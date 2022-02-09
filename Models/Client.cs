using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string? nom_client { get; set; }
        [StringLength(100)]
        public string? prenom_client { get; set; }

        public DateTime date_naissance { get; set; }
        [StringLength(100)]
        public string? adresse { get; set; }
        [StringLength(100)]
        public string? telephone { get; set; }
    }
}
