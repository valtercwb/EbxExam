namespace EbanxExam.Domain.Models
{
  public abstract class Account
  {
    public long Id { get; set; }
    public decimal Balance { get; set; }
  }
}
