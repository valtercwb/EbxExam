using EbanxExam.Application.Enum;
using EbanxExam.Application.Interface;
using EbanxExam.Application.Transients;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EbanxExam.Controllers
{
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
      _accountService = accountService;
    }

    /// <summary>
    /// Drops the database
    /// </summary>
    /// <returns></returns>
    [Route("/reset"), Produces("text/plain")]
    [HttpPost]
    public IActionResult Reset()
    {
      _accountService.Reset();

      return Ok(HttpStatusCode.OK.ToString());
    }

    /// <summary>
    /// Returns the balance by the user account identity id
    /// </summary>
    /// <param name="account_id"></param>
    /// <returns></returns>
    [Route("/balance")]
    [HttpGet]
    public ActionResult Balance(string account_id)
    {
      var balance = _accountService.Balance(account_id);

      if (balance == null)
      {
        return NotFound(0);
      }

      return Ok(balance);
    }

    /// <summary>
    /// This endpoint accept three kinds of operations
    /// which are defined by the type parameter: deposit, withdraw or transfer
    /// </summary>
    /// <param name="eventRequest"></param>
    /// <returns></returns>
    [Route("/event")]
    [HttpPost]
    public ActionResult<EventRequest> Event(EventRequest eventRequest)
    {
      var response = new Response();

      switch (eventRequest.Type)
      {
        case TransactionType.deposit:
          if (string.IsNullOrEmpty(eventRequest.Destination))
          {
            return BadRequest();
          }

          response.Destination = _accountService.Deposit(eventRequest.Type, eventRequest.Origin, eventRequest.Destination, eventRequest.Amount);

          if (response.Destination == null)
          {
            return NotFound(0);
          }

          return Created("event", new { response.Destination });

        case TransactionType.withdraw:
          if (string.IsNullOrEmpty(eventRequest.Origin))
          {
            return BadRequest();
          }

          response.Origin = _accountService.Withdraw(eventRequest.Type, eventRequest.Origin, eventRequest.Destination, eventRequest.Amount);

          if (response.Origin == null)
          {
            return NotFound(0);
          }

          return Created("event", new { response.Origin });

        case TransactionType.transfer:

          if (string.IsNullOrEmpty(eventRequest.Origin) || string.IsNullOrEmpty(eventRequest.Destination))
          {
            return BadRequest();
          }

          response.Transfer = _accountService.Transfer(eventRequest.Type, eventRequest.Origin, eventRequest.Destination, eventRequest.Amount);

          if (response.Transfer == null || response.Transfer.Destination == null)
          {
            return NotFound(0);
          }

          return Created("event", new { response.Transfer.Origin, response.Transfer.Destination });

        default:
          return BadRequest();
      }
    }
  }
}
