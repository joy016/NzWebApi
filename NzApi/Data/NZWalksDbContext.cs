using Microsoft.EntityFrameworkCore;
using NzApi.Models.Domain;

namespace NzApi.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { 
            
        }



        // these 3 properties will create tables once migration is run
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Regions> Regions { get; set; }

        public DbSet<Walks> Walks { get; set; }


    }
}
