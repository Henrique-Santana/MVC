using System;

namespace RoleTopMVC.Models
{
    public class Agendamento
    {
        
        public ulong Id {get; set;} // atributo para identificar o Agendamento.
        public Cliente cliente {get; set;}
        public Evento evento {get; set;}
        public double PrecoTotal {get;set;}
        public uint Status {get; set;} // atributo para saber se o agendamento esta aprovado, reprovado ou pendente.
        public DateTime DatadoPedido {get;set;}

        public Agendamento()
        {
            this.Id = 0;
            this.cliente = new Cliente();
            this.evento = new Evento();
            this.Status = Status;
        }
        // conferrir = criar classe opcionais com os opcionais, opcionais1 e opcionais2 onde opcionais1={Agendamento.opcional1};opcionais2={Agendamento.opcional2}.

    }
}