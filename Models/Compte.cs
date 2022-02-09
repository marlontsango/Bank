using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Models
{
    public class Compte
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string? intitule_compte { get; set; }
     
        public DateTime date_ouverture { get; set; }
        public int solde { get; set; }
        [StringLength(100)]
        public string? numero_compte { get; set; }

        [ForeignKey("Client")]
        public virtual int ClientId { get; set; }
        public virtual Client? client { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public virtual Type? type { get; set; }
    }
}
