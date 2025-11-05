using Bank.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.factory
{
    public class BankFactory
    {
        public BankAccount CreateBankAccount(int id, string name, decimal balance)
        {
            return new BankAccount(id, name, balance);
        }

        public Category CreateCategory(int id, string name, CategoryType type)
        {
            return new Category(id, name, type);
        }

        public Operation CreateOperation(int id, decimal amount, DateTime date, string description,
                                          int accountId, int categoryId, OperationType type)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма должна быть положительной.");
            }
            return new Operation(id, type, accountId, amount, date, description, categoryId);
        }
    }
}
