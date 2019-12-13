using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Models;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class CadastroController : AbstractController
    {
        ClienteRepository clienteRepositorio = new ClienteRepository();
        public IActionResult Index()
        {
            return View(new BaseViewModel()
            {
                NomeView = "Cadastro",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }
        public IActionResult CadastrarCliente(IFormCollection form)
        {
            //
            ViewData["Action"] = "Cadastro";
            try
            {
                //objeto criado e construtor criado para podemos criar um cliente.
                Cliente cliente = new Cliente(form["nome"],form["telefone"],form["senha"], form["email"],form["cpf"]);

                clienteRepositorio.Inserir(cliente);
                
                return View("Sucesso",new RespostaViewModel());
                //Retorna uma View Erro que na vdd é um File em View/Shared, que retorna uma msg de sucesso.
            }
            catch (Exception e)
            {
                //Retorna uma View Erro que na vdd é um File em View/Shared, que retorna uma msg de erro.
                return View ("Error",new RespostaViewModel());
            }
        }
    }
}