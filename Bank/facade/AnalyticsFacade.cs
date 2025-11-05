using Bank.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.facade
{
    public class AnalyticsFacade
    {
        private readonly List<Operation> _operations;
        private readonly List<Category> _categories;

        public AnalyticsFacade(List<Operation> operations, List<Category> categories)
        {
            _operations = operations;
            _categories = categories;
        }

        public void CalculateIncomeAndExpense(DateTime startDate, DateTime endDate)
        {
            var filteredOperations = _operations.Where(op => op.Date >= startDate && op.Date <= endDate).ToList();

            decimal totalIncome = filteredOperations.Where(op => op.Type == OperationType.Income).Sum(op => op.Amount);
            decimal totalExpense = filteredOperations.Where(op => op.Type == OperationType.Expense).Sum(op => op.Amount);
            decimal difference = totalIncome - totalExpense;

            Console.WriteLine($"Аналитика за период с {startDate:d} по {endDate:d}:");
            Console.WriteLine($"Общий доход: {totalIncome:N2} руб.");
            Console.WriteLine($"Общий расход: {totalExpense:N2} руб.");
            Console.WriteLine($"Разница (доход - расход): {difference:N2} руб.");
        }

        public void GroupByCategory()
        {
            Console.WriteLine("\nГруппировка по категориям:");

            var incomeGroups = _operations.Where(op => op.Type == OperationType.Income)
                                         .GroupBy(op => op.CategoryId)
                                         .Select(g => new
                                         {
                                             CategoryName = _categories.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Без категории",
                                             Total = g.Sum(op => op.Amount)
                                         });

            Console.WriteLine("\nДоходы по категориям:");
            if (!incomeGroups.Any())
            {
                Console.WriteLine("Нет данных о доходах.");
            }
            else
            {
                foreach (var group in incomeGroups)
                {
                    Console.WriteLine($"- {group.CategoryName}: {group.Total:N2} руб.");
                }
            }
            

            var expenseGroups = _operations.Where(op => op.Type == OperationType.Expense)
                                          .GroupBy(op => op.CategoryId)
                                          .Select(g => new
                                          {
                                              CategoryName = _categories.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Без категории",
                                              Total = g.Sum(op => op.Amount)
                                          });
            
            Console.WriteLine("\nРасходы по категориям:");
            if (!expenseGroups.Any())
            {
                Console.WriteLine("Нет данных о расходах.");
            }
            else
            {
                foreach (var group in expenseGroups)
                {
                    Console.WriteLine($"- {group.CategoryName}: {group.Total:N2} руб.");
                }
            }
        }
    }
}
