using System.Collections.Generic;
using RoleTopMVC.Models;

namespace RoleTopMVC.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public List<Agendamento> Agendamento {get; set;}
        public uint EventosAprovados {get; set;}
        public uint EventosReprovados {get; set;}
        public uint EventosPendentes {get; set;}

        public DashboardViewModel()
        {
            this.Agendamento = new List<Agendamento>();
            
        }
    }
}