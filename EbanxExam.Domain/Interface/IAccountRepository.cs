using EbanxExam.Domain.Models;

namespace EbanxExam.Domain.Interface
{
  public interface IAccountRepository
  {
    int? GetBalance(string accountId);
    Account GetAccount(string accountId);
    Account CreateAccount(Account account);
    Account Update(Account account);
    void Reset();
    void AddInitialData();
  }
}
