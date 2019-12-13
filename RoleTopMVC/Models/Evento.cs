using System;

namespace RoleTopMVC.Models
{
    public class Evento
    {
        public string Opcional1 {get; set;}
        public string Opcional2 {get; set;}
        public string Endereco {get; set;}
        public string TipoPagamento {get; set;}
        public string TipoEvento {get; set;}
        public DateTime Data {get; set;}

        public Evento ()
        {
            this.Opcional1 = Opcional1;
            this.Opcional2 = Opcional2;
            this.Endereco = Endereco;
            this.TipoPagamento = TipoPagamento;
            this.TipoEvento = TipoEvento;
            this.Data = Data;
        }
    }
}