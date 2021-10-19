using EbanxExam.Application.Enum;
using EbanxExam.Application.Transients;
using System.Threading.Tasks;

namespace EbanxExam.Application.Interface
{
  public interface IAccountService
  {
    void Reset();
    int? Balance(string accountId);
    Transfer Transfer(TransactionType type, string origin, string destination, int amount);
    Destination Deposit(TransactionType type, string origin, string destination, int amount);
    Origin Withdraw(TransactionType type, string origin, string destination, int amount);
    void AddInitialData();
  }
}
