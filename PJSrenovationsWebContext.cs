using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PJSrenovation_Project_Manager.Models;

namespace PJSrenovation_Project_Manager
{
    public class PJSrenovationsWebContext : DbContext
    {
        public PJSrenovationsWebContext(DbContextOptions<PJSrenovationsWebContext>
            options)
            : base(options)
        {
        }
        public PJSrenovationsWebContext() { }

        // Dbset objects for collocting data set for dbase
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Painter> Painters { get; set; }
        public virtual DbSet<PaintingWork> PaintingWorks { get; set; }
        public virtual DbSet<PaintingJob> PaintingJobs { get; set; }

        // ************************Sub contract Entities*************************
       // public virtual DbSet<SubContractor> SubContractors { get; set; }
        //public virtual DbSet<PlumbingWork> PlumbingWorks { get; set; }
        //public virtual DbSet<ElectricalWork> ElectricalWorks{get; set;}
        //public virtual DbSet<CarpentryWork> CarpentryWorks{get; set;}
        //public virtual DbSet<WallPaperWork> WallPaperWorks{get; set;}
        //public virtual DbSet<Plastering> Plasterings { get; set; }
        //public virtual DbSet<Tiling> Tilings{get; set;}
        //public virtual DbSet<SubContractWork> SubContractWorks { get; set; }




        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Database=PJSrenovationsWebContext;Trusted_Connection=True;MultipleActiveResultSets=true");

        //*****************this is to override the default Table names i EF.  ***********


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Painter>().ToTable("Painter");
            modelBuilder.Entity<PaintingWork>().ToTable("PaintingWork");
            modelBuilder.Entity<PaintingJob>().ToTable("PaintingJob");

            //*********************Sub contract Tables**************************
            //modelBuilder.Entity<SubContractor>().ToTable("SubContractor");
            //modelBuilder.Entity<PlumbingWork>().ToTable("Plumbing");
            //modelBuilder.Entity<ElectricalWork>().ToTable("Electrical");
            //modelBuilder.Entity<Tiling>().ToTable("Tiling");
            //modelBuilder.Entity<Plastering>().ToTable("Plastering");
            //modelBuilder.Entity<WallPaperWork>().ToTable("WallPaper");
            //modelBuilder.Entity<CarpentryWork>().ToTable("Carpentry");
           // modelBuilder.Entity<SubContractWork>().ToTable("SubContractWork");





            modelBuilder.Entity<Client>().HasMany(c => c.Properties).WithOne(p => p.Client);
           
            

        }
        
       

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Database=PJSrenovationsWebContext;Trusted_Connection=True;MultipleActiveResultSets=true");

        //*****************this is to override the default Table names i EF.  ***********


        //public DbSet<PaintingDecoratingJob> PaintingDecoratingJob { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Database=PJSrenovationsWebContext;Trusted_Connection=True;MultipleActiveResultSets=true");

        //*****************this is to override the default Table names i EF.  ***********



        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Database=PJSrenovationsWebContext;Trusted_Connection=True;MultipleActiveResultSets=true");

        //*****************this is to override the default Table names i EF.  ***********


        

    }
}
