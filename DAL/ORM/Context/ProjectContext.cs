using DAL.ORM.Entity;
using DAL.ORM.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ORM.Context
{
    public class ProjectContext:DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString = @"Server=DESKTOP-ISA\SQLEXPRESS;Database=ContactList;Integrated Security=true";
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContactMap());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

            foreach (var item in addedEntries)
            {
                Contact entity = item.Entity as Contact;
                if (item != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.AddedDate = DateTime.Now;
                        entity.Status = Entity.Enum.Status.Active;
                    }
                    
                }
            }
            return base.SaveChanges();
        }

       
    }
}
