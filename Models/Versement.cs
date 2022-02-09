using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Models
{
    public class Versement
    {
        [Key]
        public int Id { get; set; }
        public string? motif { get; set; }
        public DateTime date_versement { get; set; }
        public int montant_versement { get; set; }

        [ForeignKey("Compte")]
        public virtual int CompteId { get; set; }
        public virtual Compte? compte { get; set; }
  

    }
}
