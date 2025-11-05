using Bank.facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.services;

namespace Bank.commands.BankAccountCommand
{
    public class EditBankAccountCommand(BankAccountFacade accountFacade) : ICommand
    {
        private readonly BankAccountFacade _accountFacade = accountFacade;
        public void Execute()
        {
            try
            {
                _accountFacade.ShowAllBankAccounts();
                int accountId = InputHelper.ReadInt(
                    "Введите ID счёта для редактирования: ",
                    id => _accountFacade.BankAccountExists(id),
                    "Ошибка: Счёт с таким ID не найден."
                );

                string newName = InputHelper.ReadString(
                    "Введите новое имя счёта: ",
                    name => !string.IsNullOrWhiteSpace(name),
                    "Ошибка: Имя не может быть пустым."
                );

                _accountFacade.UpdateBankAccount(accountId, newName);
                Console.WriteLine("Счёт успешно обновлён.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Редактирование счёта отменено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
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
