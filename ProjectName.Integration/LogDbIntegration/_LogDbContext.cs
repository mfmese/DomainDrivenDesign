using Microsoft.EntityFrameworkCore;

namespace Integration.LogDbIntegration
{
    public class LogDbContext: DbContext
    {
        public LogDbContext(string connectionString): base()
        {

        }

        public LogDbContext(DbContextOptions<LogDbContext> options): base(options)
        {

        }

        public virtual DbSet<ApplicationLog> ApplicationLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("connection string");
//            }

        }
    }
}
