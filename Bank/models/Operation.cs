using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.models
{
    public class Operation
    {
        public int Id { get; private set; }
        public OperationType Type { get; private set; }
        public int AccountId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public int CategoryId { get; private set; }
        public Operation(int id, OperationType type, int bankAccountId, decimal amount, DateTime date, string description, int categoryId)
        {
            Id = id;
            Type = type;
            AccountId = bankAccountId;
            Amount = amount;
            Date = date;
            Description = description;
            CategoryId = categoryId;
        }
        public void UpdateDescription(string description)
        {
            Description = description;
        }
    }
    public enum OperationType
    {
        Income,
        Expense
    }
}
