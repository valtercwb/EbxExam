using EbanxExam.Application.Enum;

namespace EbanxExam.Application.Transients
{
  public class Event
  {
    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public int Origin { get; set; }
    public int Amount { get; set; }
    public int Destination { get; set; }
  }
}
