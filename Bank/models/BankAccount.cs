using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.models
{
    public class BankAccount
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }
        public BankAccount(int id, string name, decimal balance)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Имя не может быть пустым.");
            }
            if (balance < 0)
            {
                throw new ArgumentException("Баланс не может быть отрицательным.");
            }
            Id = id;
            Name = name;
            Balance = balance;
        }
        public void UpdateBalance(decimal amount)
        {
            if (Balance + amount < 0)
            {
                throw new InvalidOperationException("Баланс не может стать отрицательным.");
            }
            Balance += amount;
        }
        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Имя не может быть пустым.");
            }
            Name = name;
        }
    }
}
