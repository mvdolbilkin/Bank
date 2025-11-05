using Bank.factory;
using Bank.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.models;

namespace Bank.facade
{
    public class BankAccountFacade(BankFactory factory)
    {
        private readonly BankFactory _factory = factory;
        private readonly List<BankAccount> _accounts = [];

        public BankAccount CreateBankAccount(int id, string name, decimal balance)
        {
            var account = _factory.CreateBankAccount(id, name, balance);
            _accounts.Add(account);
            return account;
        }
        public void DeleteBankAccount(int id)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == id);
            if (account != null)
            {
                _accounts.Remove(account);
            }
        }
        public void ShowAllBankAccounts()
        {
            int index = 1;
            Console.WriteLine("Список счетов:");
            foreach (BankAccount account in _accounts)
            {
                Console.WriteLine($"{index}. [{account.Id}] {account.Name}\t{account.Balance:N2} руб.");
                index++;
            }
            if (_accounts.Count == 0)
            {
                Console.WriteLine("Счетов нет.");
            }
        }
        public void UpdateBankAccount(int id, string name)
        {
            var account = GetBankAccount(id);
            account.UpdateName(name);
        }
        public bool UpdateBankAccountBalance(int bankAccountId, decimal amount)
        {
            try
            {
                var bankAccount = GetBankAccount(bankAccountId);
                bankAccount.UpdateBalance(amount);
                Console.WriteLine($"Баланс счёта {bankAccount.Name} обновлён: {bankAccount.Balance:N2} руб.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool BankAccountExists(int id)
        {
            return _accounts.Any(a => a.Id == id);
        }
        public BankAccount GetBankAccount(int id)
        {
            var account = _accounts.FirstOrDefault(account => account.Id == id) ?? throw new InvalidOperationException($"Ошибка: Аккаунт с ID {id} не найден.");
            return account;
        }
    }
}
