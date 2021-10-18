using EbanxExam.Application.Enum;
using EbanxExam.Application.Transients;

namespace EbanxExam.Application.Interface
{
  public interface IAccountService
  {
    void Reset();
    decimal? Balance(string accountId);
    Transfer Transfer(TransactionType type, string origin, string destination, decimal amount);
    Destination Deposit(TransactionType type, string origin, string destination, decimal amount);
    Origin Withdraw(TransactionType type, string origin, string destination, decimal amount);
  }
}
