using MedGrupo.Domain.ContactAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.ContactData
{
    public class SQLServerContext : DbContext, IDBContext
    {
        private readonly string ConnectionString;
        public DbSet<Contact>? Contacts { get; set; }
        public SQLServerContext(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionStrings:ContactServer"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Gender).HasConversion<string>();
            });
            
        }
        public void CreateContact(Contact value)
        {
            Contacts?.Add(value);
            SaveChanges();
        }

        public void DeleteContact(Contact value)
        {
            Contacts?.Remove(value);
            SaveChanges();
        }

        public Contact GetContact(int id)
        {
            return Contacts?.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Contact> GetContacts()
        {
            var list = Contacts?.Where(cc => cc.Active == true);
            return list == null ? new Contact[0] : list;
        }

        public void UpdateContact(Contact value)
        {
            Contacts?.Update(value);
            SaveChanges();
        }
    }
}