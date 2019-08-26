using System.ComponentModel.DataAnnotations.Schema;

namespace AspCoreGraficos.Models
{
    public class Moeda
    {
        public int MoedaId { get; set; }

        public string Nome { get; set; }

        public int Quantidade { get; set; }

        [NotMapped]
        public bool Checkboxmarcado { get; set; }
    }
}