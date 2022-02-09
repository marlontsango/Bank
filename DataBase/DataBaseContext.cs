
using Bank.Models;
using Microsoft.EntityFrameworkCore;
using Type = Bank.Models.Type;

namespace Bank.DataBase
{
    public class DataBaseContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=banque;user=root;password=''",
                                    new MySqlServerVersion(new Version(8, 0, 19)));
        }
        public DbSet<Client>? client { get; set; }
        public DbSet<Versement>? versement { get; set; }
        public DbSet<Virement>? virement { get; set; }
        public DbSet<Retrait>? retrait { get; set; }
        public DbSet<Type>? type { get; set; }
        public DbSet<Compte>? compte { get; set; }


    }
    
}
