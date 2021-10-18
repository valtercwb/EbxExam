using EbanxExam.Domain.Models;
using System.Collections.Generic;

namespace EbanxExam.Domain.Interface
{
  public interface IAccountRepository
  {
    decimal? GetBalance(string accountId);
    Account GetAccount(string accountId);
    Account CreateAccount(Account account);
    Account Update(Account account);
    void Reset();
    IEnumerable<Account> UpdateRange(List<Account> accountList);
  }
}
