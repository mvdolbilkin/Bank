using Bank.factory;
using Bank.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.services;

namespace Bank.facade
{
    public class OperationFacade(BankFactory factory, BankAccountFacade accountFacade)
    {
        private readonly BankFactory _factory = factory;
        private readonly BankAccountFacade _accountFacade = accountFacade;
        private readonly List<Operation> _operations = [];

        public List<Operation> Operations => _operations;

        public Operation CreateOperation(int id, decimal amount, DateTime date, string description,
                                     int accountId, int categoryId, OperationType type)
        {
            var operationAmount = type == OperationType.Expense ? -amount : amount;
            var operation = _factory.CreateOperation(id, amount, date, description, accountId, categoryId, type);
            if (_accountFacade.UpdateBankAccountBalance(accountId, operationAmount))
            {
                _operations.Add(operation);
            }
            return operation;
        }

        public void DeleteOperation(int id)
        {
            var operation = _operations.FirstOrDefault(op => op.Id == id);
            if (operation != null)
            {
                var amountToReverse = operation.Type == OperationType.Expense ? operation.Amount : -operation.Amount;
                _accountFacade.UpdateBankAccountBalance(operation.AccountId, amountToReverse);
                _operations.Remove(operation);
            }
        }
        public void UpdateOperation(int id, string description)
        {
            var operation = GetOperation(id);
            operation.UpdateDescription(description);
        }

        public bool OperationExists(int id)
        {
            return _operations.Any(o => o.Id == id);
        }

        public void ShowAllOperations()
        {
            int index = 1;
            Console.WriteLine("Список операций:");
            foreach (Operation operation in _operations)
            {
                Console.WriteLine($"{index}. {operation.Date}. [{operation.Id}] {operation.Type} со счёта ID {operation.AccountId} : {operation.Amount:N2} руб.");
                index++;
            }
            if (_operations.Count == 0)
            {
                Console.WriteLine("Операций нет.");
            }
        }

        public Operation GetOperation(int id)
        {
            var operation = _operations.FirstOrDefault(operation => operation.Id == id) ?? throw new InvalidOperationException($"Ошибка: Операция с ID {id} не найдена.");
            return operation;
        }
        public void ShowOperationsForAccountId()
        {
            try
            {
                int accountId = InputHelper.ReadInt(
                    "Введите ID счёта (или 'отмена' для отмены команды): ",
                    id => _accountFacade.BankAccountExists(id),
                    "Ошибка: Счёт с таким ID не найден."
                );

                Console.WriteLine($"\nСписок операций для счёта ID {accountId}:");
                foreach (Operation operation in _operations)
                {
                    if (operation.AccountId == accountId)
                        Console.WriteLine($"{operation.Date}. [{operation.Id}] {operation.Type} со счёта ID {operation.AccountId}: {operation.Amount:N2} руб.\t{operation.CategoryId}");
                }
                if (_operations.Count == 0)
                {
                    Console.WriteLine("Операций нет.");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Команда отменена.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
