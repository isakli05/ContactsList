using DAL.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ORM.Map
{
    public class ContactMap:EntityTypeConfiguration<Contact>

    {
        public ContactMap()
        {
            ToTable("dbo.Contacts");
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AddedDate).HasColumnType("datetime2");
            Property(x => x.ImagePath).IsOptional();
        }
    }
}
