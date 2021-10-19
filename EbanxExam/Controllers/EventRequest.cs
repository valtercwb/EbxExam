namespace EbanxExam.Controllers
{
  public class EventRequest
  {
    public EventRequest()
    {
    }

    public Application.Enum.TransactionType Type { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public int Amount { get; set; }
  }
}