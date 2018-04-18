using Incognito.Models;
using Microsoft.EntityFrameworkCore;

namespace Incognito.Data
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options)
            : base (options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<ReportMessage> ReportMessages { get; set; }
    }
}
