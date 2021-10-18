using EbanxExam.Application.Enum;
using EbanxExam.Application.Transients;
using System;

namespace EbanxExam.Application.Interface
{
  public interface IAccountService
  {
    decimal GetBalance(long accountId);

    Origin Withdraw(TransactionType type, string origin, decimal amount);
    Tuple<Origin, Destination> Transfer(TransactionType type, string origin, decimal amount = 10);
  }
}
