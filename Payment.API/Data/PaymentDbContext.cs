using Microsoft.EntityFrameworkCore;
using Payment.API.Models;

namespace Payment.API.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}
