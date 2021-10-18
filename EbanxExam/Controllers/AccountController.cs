using Microsoft.AspNetCore.Mvc;

namespace EbanxExam.Controllers
{
  [ApiController]
  public class AccountController : ControllerBase
  {

    [Route("/reset")]
    [HttpGet]
    public ActionResult Reset()
    {
      return Ok();
    }

    [Route("/balance")]
    [HttpGet]
    public ActionResult Balance(long accountId)
    {
      return Ok();
    }


  }
}
