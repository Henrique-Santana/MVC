using System;
using System.Collections.Generic;
using System.IO;
using RoleTopMVC.Models;

namespace RoleTopMVC.Repositories
{
    public class AgendamentoRepository : RepositoryBase
    {
    private const string PATH = "Database/Agendamento.csv";//PATH é uma constante, onde fica o csv, para crialo.
    public AgendamentoRepository ()
        {
            if(!File.Exists(PATH))//File Exists serve para conferir se existe o alquivo csv(PATH) , 
            {
                File.Create(PATH).Close();//Cria o csv e fecha.
            }
        }

    public bool Inserir(Agendamento agendamento)
        {
            var quantidadeLinhas = File.ReadAllLines(PATH).Length;
            agendamento.Id = (ulong) ++quantidadeLinhas;
            var linha = new string[] {PrepararRegistroCSV(agendamento)};
            File.AppendAllLines(PATH, linha);
            return true;
        }
        public List<Agendamento> ObterTodos()
            {
                var linhas = File.ReadAllLines(PATH);
                List<Agendamento> agendamentos = new List<Agendamento>();
                foreach (var linha in linhas)
                {
                    Agendamento agendamento = new Agendamento();
                    
                    agendamento.Id = ulong.Parse(ExtrairValorDoCampo("id", linha));
                    agendamento.Status = uint.Parse(ExtrairValorDoCampo("status_pedidos", linha));
                    agendamento.cliente.Nome = ExtrairValorDoCampo("cliente_nome", linha);
                    agendamento.evento.Endereco = ExtrairValorDoCampo("evento_endereco", linha);
                    agendamento.cliente.Telefone = ExtrairValorDoCampo("cliente_telefone", linha);
                    agendamento.cliente.Email = ExtrairValorDoCampo("cliente_email", linha);
                    agendamento.DatadoPedido = DateTime.Parse(ExtrairValorDoCampo("data_pedido", linha));
                    agendamento.cliente.Cpf = ExtrairValorDoCampo("cliente_cpf", linha);
                    agendamento.evento.Opcional1 = ExtrairValorDoCampo("evento_opcional1", linha);
                    agendamento.evento.Opcional2 = ExtrairValorDoCampo("evento_opcional2", linha); 
                    agendamento.evento.TipoEvento = ExtrairValorDoCampo("evento_tipoEvento", linha);
                    agendamento.evento.TipoPagamento = ExtrairValorDoCampo("evento_tipoPagamento", linha);
                    agendamento.PrecoTotal = double.Parse(ExtrairValorDoCampo("preco_total", linha));
                    agendamento.evento.Data = DateTime.Parse(ExtrairValorDoCampo("data_evento", linha));

                    agendamentos.Add(agendamento);
                }
                return agendamentos;
            }
            public List<Agendamento> ObterTodosPorCliente(string email)
            {
                var agendamentosTotais = ObterTodos();
                List<Agendamento> agendamentosCliente = new List<Agendamento>();
                foreach(var agendamento in agendamentosTotais)
                {
                    if(agendamento.cliente.Email.Equals(email))
                    {
                        agendamentosCliente.Add(agendamento);
                    }
                }
                return  agendamentosCliente;
            }

            public Agendamento ObterPor(ulong id) // método para obter o Id dos pedidos
            {
                var pedidosTotais = ObterTodos();
                foreach (var pedido in pedidosTotais)
                {
                    if(pedido.Id == id) //condição para o banco verificar se o Id do pedido do cliente, encontra-se na tabela, para depois retornar o pedido com o status de aprovado, reprovado
                    {
                        return pedido;
                    }
                }
                return null;
            }           
            public bool Atualizar (ulong id, Agendamento agendamento)
            {
                var agendamentosTotais = File.ReadAllLines(PATH); // recolhe tudo que esta na tabela de pedidos
                var agendamentoCSV = PrepararRegistroCSV(agendamento); // transforma o pedido atualizado em string para ser gravado no CSV
                var linhaPedido = -1; // porque não haverá linha -1.. serve apenas para atualizar
                var resultado = false;

                for (int i = 0; i < agendamentosTotais.Length; i++)
                {
                    var idConvertido = ulong.Parse (ExtrairValorDoCampo("id",agendamentosTotais[i]));
                    if(agendamento.Id.Equals(idConvertido))  // se o ID do pedido que foi enviado para atualizar for igual a linha com o ID igual ele vai atualizar o status
                    {
                        linhaPedido = i;
                        resultado = true;
                        break;
                    }
                }
                if (resultado)
                {
                    agendamentosTotais[linhaPedido] = agendamentoCSV;
                    File.WriteAllLines(PATH,agendamentosTotais);
                }

                return (resultado);
            }
            private string PrepararRegistroCSV(Agendamento agendamento)
            {
                Cliente cliente = agendamento.cliente;

                return $"id={agendamento.Id};status_pedidos={agendamento.Status};data_evento={agendamento.evento.Data};cliente_nome={agendamento.cliente.Nome};evento_endereco={agendamento.evento.Endereco};cliente_telefone={agendamento.cliente.Telefone};cliente_email={agendamento.cliente.Email};cliente_cpf={agendamento.cliente.Cpf};evento_opcional1={agendamento.evento.Opcional1};evento_opcional2={agendamento.evento.Opcional2};evento_tipoPagamento={agendamento.evento.TipoPagamento};evento_tipoEvento={agendamento.evento.TipoEvento};data_pedido={agendamento.DatadoPedido};preco_total={agendamento.PrecoTotal}";
            }
    }
}