using System;

namespace EbanxExam.Application.Transients
{
  public class Resposta
  {
    public Destination destination { get; set; }
    public Origin origin { get; set; }
    public Transfer Transfer { get; set; }
  }
}
