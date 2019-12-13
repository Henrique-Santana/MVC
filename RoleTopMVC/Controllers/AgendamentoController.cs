using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Enums;
using RoleTopMVC.Models;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class AgendamentoController : AbstractController
    {
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        ClienteRepository clienteRepository = new ClienteRepository();
        Agendamento agendamento = new Agendamento();
        public IActionResult Index()
        {
            var usuarioLogado = ObterUsuarioSession();

            AgendamentoViewModel avm = new AgendamentoViewModel();

            if (!string.IsNullOrEmpty(usuarioLogado))
            {
                var clienteLogado = clienteRepository.ObterPor(usuarioLogado);

                avm.UsuarioEmail = usuarioLogado;
                avm.Cliente = clienteLogado;
                
                return View(avm);
            }
            return View("Error", new RespostaViewModel(){
                Mensagem = "É necessário estar logado para realizar um agendamento, realize um cadastro caso não tenha uma conta",
                
            });
        }
        public IActionResult Registrar(IFormCollection form)

        {
            Evento evento = new Evento(){
                
                Opcional1= form["opcional1"],
                Opcional2 = form ["opcional2"],
                Endereco = form ["endereco"],
                TipoPagamento = form ["tipoPagamento"],
                TipoEvento = form ["tipoEvento"],
                Data = DateTime.Parse(form ["data"])
                };

            Cliente cliente = new Cliente()
            {
                Nome = form["nome"],
                Cpf = form["cpf"],
                Telefone = form["telefone"],
                Email = form["email"]
            };
            

            agendamento.cliente = cliente;
            agendamento.DatadoPedido = DateTime.Now;
            agendamento.evento = evento;
            
            if(agendamentoRepository.Inserir(agendamento))
            {
            return View("Sucesso", new RespostaViewModel()
            {
                Mensagem = "Aguarde a aprovação dos nossos administradores",
                NomeView = "Sucesso",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
            }
            else
            {
            return View("Error", new RespostaViewModel()
            {
                Mensagem = "Houve um erro ao processar seu agendamento. tente novamente",
                NomeView = "Erro",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
            }
        }
        public IActionResult Aprovar(ulong id)
    {
        Agendamento agendamento = agendamentoRepository.ObterPor(id);
        agendamento.Status = (uint) StatusPedidos.APROVADO;

        if(agendamentoRepository.Atualizar(id,agendamento))
        {
            return RedirectToAction("Dashboard", "Administrador");
        }
        else
        {
            return View("Error", new RespostaViewModel()
            {
                Mensagem = "Houve um erro ao aprovar pedido",
                NomeView = "Dashboard",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }
    }
    public IActionResult Reprovar(ulong id)
    {
        Agendamento agendamento = agendamentoRepository.ObterPor(id);
        agendamento.Status = (uint) StatusPedidos.REPROVADO;

        if(agendamentoRepository.Atualizar(id, agendamento))
        {
            return RedirectToAction("Dashboard", "Administrador");
        }
        else
        {
            return View("Error", new RespostaViewModel()
            {
                Mensagem = "Houve um erro ao Reprovar pedido",
                NomeView = "Dashboard",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }
    }
    }
}