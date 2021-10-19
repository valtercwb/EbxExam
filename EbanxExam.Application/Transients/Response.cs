namespace EbanxExam.Application.Transients
{
  public class Response
  {
    public Destination Destination { get; set; }
    public Origin Origin { get; set; }
    public Transfer Transfer { get; set; }

    public Response()
    {
      Transfer = new Transfer();
    }
  }
}
