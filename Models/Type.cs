using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }
        public string? nom_type { get; set; }
        public int solde_min { get; set; }
        public int montant_max { get; set; }
    }
}
