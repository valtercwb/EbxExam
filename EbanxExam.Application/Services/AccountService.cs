using AutoMapper;
using EbanxExam.Application.Enum;
using EbanxExam.Application.Interface;
using EbanxExam.Application.Transients;
using EbanxExam.Domain.Interface;
using System.Transactions;

namespace EbanxExam.Application
{
  public class AccountService : IAccountService
  {
    readonly IMapper _mapper;
    readonly IAccountRepository _accountRepository;

    public AccountService(IMapper mapper, IAccountRepository accountRepository)
    {
      _mapper = mapper;
      _accountRepository = accountRepository;
    }

    void IAccountService.Reset()
    {
      _accountRepository.Reset();
    }

    int? IAccountService.Balance(string accountId)
    {
      return _accountRepository.GetBalance(accountId);
    }

    Transfer IAccountService.Transfer(TransactionType type, string origin, string destination, int amount)
    {
      _accountRepository.AddInitialData();

      var originAccount = _accountRepository.GetAccount(origin);
      var destinationAccount = _accountRepository.GetAccount(destination);
      var transfer = new Transfer();

      if (originAccount != null && destinationAccount != null)
      {
        originAccount.Balance -= amount;
        destinationAccount.Balance += amount;

        using var scope = new TransactionScope();
        try
        {
          var originAccountResponse = _accountRepository.Update(originAccount);
          var destinationAccountResponse = _accountRepository.Update(destinationAccount);
          scope.Complete();

          transfer.Destination = _mapper.Map<Destination>(destinationAccountResponse);
          transfer.Origin = _mapper.Map<Origin>(originAccountResponse);
          return transfer;
        }
        catch (System.Exception)
        {
          scope.Dispose();
        }
      }

      return null;
    }

    Destination IAccountService.Deposit(TransactionType type, string origin, string destination, int amount)
    {
      if (!string.IsNullOrEmpty(destination))
      {
        var accountDestination = _accountRepository.GetAccount(destination);

        if (accountDestination == null && destination != default)
        {
          return _mapper.Map<Destination>(_accountRepository.CreateAccount(new Domain.Models.Account { Id = destination, Balance = amount }));
        }
        else
        {
          accountDestination.Balance += amount;
          return _mapper.Map<Destination>(_accountRepository.Update(accountDestination));
        }
      }

      return null;
    }

    Origin IAccountService.Withdraw(TransactionType type, string origin, string destination, int amount)
    {
      var accountOrigin = _accountRepository.GetAccount(origin);

      if (accountOrigin == null)
      {
        return null;
      }

      if (accountOrigin.Balance > 0)
      {
        accountOrigin.Balance -= amount;

        return _mapper.Map<Origin>(_accountRepository.Update(accountOrigin));
      }

      return null;

    }

    public void AddInitialData()
    {
      _accountRepository.AddInitialData();
    }
  }
}
