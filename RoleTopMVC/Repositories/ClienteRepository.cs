using System.IO;
using RoleTopMVC.Models;

namespace RoleTopMVC.Repositories
{
    public class ClienteRepository
    {
        private const string PATH = "Database/Cliente.csv";//PATH é uma constante, onde fica o csv, para crialo.
        public ClienteRepository()
        {
            if(!File.Exists(PATH))//File Exists serve para conferir se existe o arquivo csv(PATH). 
            {
                File.Create(PATH).Close();//Cria e Fecha.
            }
        }

        public bool Inserir(Cliente cliente)
        {
            var linha = new string[] {PrepararRegistroCSV(cliente)};
            File.AppendAllLines(PATH, linha);//método Append irá inserir os dados abaixo do outro..
            
            return true;
        }

        public Cliente ObterPor(string email)
        {
            var linhas = File.ReadAllLines(PATH);
            foreach (var linha in linhas)
            {
                if(ExtrairValorDoCampo("email", linha).Equals(email)) // extrair o valor do campo email de cada linha... se o que extraiu for igual o que esta no campo email executa
                {
                    Cliente c = new Cliente();
                    c.Nome = ExtrairValorDoCampo("nome", linha);
                    c.Email = ExtrairValorDoCampo("email", linha);
                    c.Senha = ExtrairValorDoCampo("senha", linha);
                    c.Telefone = ExtrairValorDoCampo("telefone", linha);
                    c.Cpf = ExtrairValorDoCampo("cpf", linha);
                    c.TipoUsuario = uint.Parse(ExtrairValorDoCampo("tipo_usuario", linha));
                    //c.Endereço = ExtrairValorDoCampo("endereço", linha);
                    //c.DataNascimento = DateTime.Parse(ExtrairValorDoCampo("dataNascimento", linha));
                    return c;
                }
            }
            return null;
        }
        private string PrepararRegistroCSV(Cliente cliente)
        {//"endereço={cliente.Endereço};
            return $"tipo_usuario={cliente.TipoUsuario};nome={cliente.Nome};email={cliente.Email};senha={cliente.Senha};telefone={cliente.Telefone};cpf={cliente.Cpf}";
        }
        public string ExtrairValorDoCampo(string nomeCampo, string linha)
        {
            var chave = nomeCampo;
            var indiceChave = linha.IndexOf(chave);//Indexof encontra a posição da chave que foi indicada, no caso "email"
            var indiceTerminal = linha.IndexOf(";", indiceChave);//Indexof encontra a posição da chave que foi indicada, no caso "email"
            var valor = "";
            if(indiceTerminal != -1)
            {
                valor = linha.Substring(indiceChave, indiceTerminal - indiceChave); //ignora a chave e pega o valor de string depois dela
            }
            else
            {
                valor = linha.Substring(indiceChave);
            }
            System.Console.WriteLine($"Campo{nomeCampo} tem valor {valor}");
            return valor.Replace(nomeCampo + "=", "");// apaga o "email=" e substitui por nada
        }
    }
}