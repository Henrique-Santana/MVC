using System;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Enums;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class AdministradorController : AbstractController
    {
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        [HttpGet]
        public IActionResult Dashboard()
        {
            try
            {
            var tipoUsuarioSessao = uint.Parse(ObterUsuarioTipoSession());
            if(tipoUsuarioSessao.Equals((uint)TiposUsuario.ADMINISTRADOR))
            {
            var agendamento = agendamentoRepository.ObterTodos();
            DashboardViewModel dashboardViewModel = new DashboardViewModel();

            foreach (var evento in agendamento)
            {
                switch(evento.Status)
                {
                    case (uint) StatusPedidos.REPROVADO:
                    dashboardViewModel.EventosReprovados++;
                    break;
                    case (uint) StatusPedidos.APROVADO:
                        dashboardViewModel.EventosAprovados++;
                    break;
                    default:
                        dashboardViewModel.EventosPendentes++;
                        dashboardViewModel.Agendamento.Add(evento);
                    break;
                }
            }
            dashboardViewModel.NomeView = "Dashboard";
            dashboardViewModel.UsuarioEmail = ObterUsuarioSession();

            return View(dashboardViewModel); 
            }
            
            return View("Erro", new RespostaViewModel()
            {
                NomeView = "Dashboard",
                Mensagem = "Acesso restrito"
            });
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
                return View("Error", new RespostaViewModel()
            {
                NomeView = "Dashboard",
                Mensagem = "Tempo Expirado"
            });
            }
        }
    }
}