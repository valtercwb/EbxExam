
using EbanxExam.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace AspnetCore_EFCoreInMemory.Models
{
  public class ApiContext : DbContext
  {
    public ApiContext(DbContextOptions<ApiContext> options)
      : base(options)
    { }

    public DbSet<Account> Account { get; set; }
  }
}