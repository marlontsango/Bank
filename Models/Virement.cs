using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Models
{
    public class Virement
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateVirement { get; set; }
        public int montant_virement { get; set; }
        public string? motif_virement { get; set; }

        [ForeignKey("Compte")]
        public virtual int CompteId { get; set; }
        public virtual Compte? compte { get; set; }


    }
}
