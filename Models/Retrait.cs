using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Models
{
    public class Retrait
    {
        [Key]
        public int Id { get; set; }
        public string? motif_retrait { get; set; }
        public DateTime date_retrait { get; set; }
        public int montant_retrait { get; set; }

        [ForeignKey("Compte")]
        public virtual int CompteId { get; set; }
        public virtual Compte? compte { get; set; }

    }
}
