using EbanxExam.Application.Enum;
using EbanxExam.Application.Interface;
using EbanxExam.Application.Transients;
using Microsoft.AspNetCore.Mvc;

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

    [Route("/reset")]
    [HttpGet]
    public ActionResult Reset()
    {
      _accountService.Reset();

      return Ok();
    }

    [Route("/balance")]
    [HttpGet]
    public ActionResult Balance(string accountId)
    {
      var balance = _accountService.Balance(accountId);

      if (balance == null)
      {
        return NotFound();
      }

      return Ok(new { balance });
    }

    [Route("/event")]
    [HttpPost]
    public ActionResult Event(TransactionType type, string origin, string destination, decimal amount)
    {
      var resposta = new Resposta();

      switch (type)
      {
        case TransactionType.deposit:
          resposta.destination = _accountService.Deposit(type, origin, destination, amount);
          if (resposta.destination == null)
          {
            return NotFound();
          }

          return Created("event", new { resposta.destination });
        case TransactionType.withdraw:
          resposta.origin = _accountService.Withdraw(type, origin, destination, amount);
          if (resposta.origin == null)
          {
            return NotFound();
          }

          return Created("event", new { resposta.origin });
        case TransactionType.transfer:
          resposta.Transfer = _accountService.Transfer(type, origin, destination, amount);
          if (resposta.Transfer.Destination == null)
          {
            return NotFound();
          }

          return Created("event", new { resposta.Transfer.Destination, resposta.Transfer.Origin });
        default:
          return NoContent();

      }
    }
  }
}
