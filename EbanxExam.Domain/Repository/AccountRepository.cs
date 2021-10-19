using AspnetCore_EFCoreInMemory.Models;
using EbanxExam.Domain.Interface;
using EbanxExam.Domain.Models;
using System.Linq;

namespace EbanxExam.Domain.Repository
{
  public class AccountRepository : IAccountRepository
  {
    readonly ApiContext _apiContext;

    public AccountRepository(ApiContext apiContext)
    {
      _apiContext = apiContext;
    }

    Account IAccountRepository.CreateAccount(Account account)
    {
      _apiContext.Account.Add(account);
      _apiContext.SaveChanges();
      return account;
    }

    Account IAccountRepository.Update(Account account)
    {
      _apiContext.Account.Update(account);
      _apiContext.SaveChanges();
      return account;
    }

    public void Reset()
    {
      _apiContext.Database.EnsureDeleted();
    }

    Account IAccountRepository.GetAccount(string accountId)
    {
      return _apiContext.Account.Where(x => x.Id == accountId).FirstOrDefault();
    }

    int? IAccountRepository.GetBalance(string accountId)
    {
      return _apiContext.Account.Where(x => x.Id == accountId).Select(x => x.Balance).FirstOrDefault();
    }

    void IAccountRepository.AddInitialData()
    {
      if (!_apiContext.Account.Any(x => x.Id.Equals("300")))
      {
        var account = new Account
        {
          Id = "300",
          Balance = 0
        };

        _apiContext.Account.Add(account);
        _apiContext.SaveChanges();
      }
    }
  }
}
