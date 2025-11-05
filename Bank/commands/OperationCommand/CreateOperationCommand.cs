using Bank.facade;
using Bank.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.services;

namespace Bank.commands.OperationCommand
{
    public class CreateOperationCommand(OperationFacade operationFacade, BankAccountFacade accountFacade, CategoryFacade categoryFacade) : ICommand
    {
        private readonly OperationFacade _operationFacade = operationFacade;
        private readonly BankAccountFacade _accountFacade = accountFacade;
        private readonly CategoryFacade _categoryFacade = categoryFacade;
        private int _id;
        private OperationType _type;
        private int _bankAccountId;
        private decimal _amount;
        private DateTime _date;
        private string? _description;
        private int _categoryId;

        public void Execute()
        {
            try
            {
                _id = new Random().Next(1000, 9999);

                _type = InputHelper.ReadEnum<OperationType>("Выберите тип операции (Income/Expense):");

                _bankAccountId = InputHelper.ReadInt(
                    "Введите ID счёта: ",
                    id => _accountFacade.BankAccountExists(id),
                    "Ошибка: Счёт с таким ID не найден."
                );

                _amount = InputHelper.ReadDecimal(
                    "Введите сумму операции: ",
                    amount => {
                        if (_type == OperationType.Expense)
                        {
                            return _accountFacade.GetBankAccount(_bankAccountId).Balance >= amount;
                        }
                        return true;
                    },
                    "Ошибка: Недостаточно средств."
                );

                _date = DateTime.Now;

                _description = InputHelper.ReadSimpleString("Введите описание операции: ");

                _categoryId = InputHelper.ReadInt(
                    "Введите ID категории: ",
                    id => _categoryFacade.CategoryExists(id),
                    "Ошибка: Категория с таким ID не найдена."
                );

                var category = _categoryFacade.GetCategory(_categoryId);
                if (category.Type.ToString() != _type.ToString())
                {
                    string opTypeStr = _type == OperationType.Income ? "Доход" : "Расход";
                    string catTypeStr = category.Type == CategoryType.Income ? "Доход" : "Расход";
                    Console.WriteLine($"Ошибка: Тип операции '{opTypeStr}' не соответствует типу категории '{category.Name}' ('{catTypeStr}').");
                    return;
                }

                _operationFacade.CreateOperation(_id, _amount, _date, _description, _bankAccountId, _categoryId, _type);
                Console.WriteLine($"Операция '{_id}' успешно создана!");

            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Создание операции отменено.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Ошибка создания операции.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
