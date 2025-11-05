using Bank.facade;
using Bank.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.services;

namespace Bank.commands.BankAccountCommand
{
    public class DeleteBankAccountCommand(BankAccountFacade accountFacade) : ICommand
    {
        private readonly BankAccountFacade _accountFacade = accountFacade;
        private BankAccount? _account;

        public void Execute()
        {
            try
            {
                int accountId = InputHelper.ReadInt(
                    "Введите ID счёта (или 'отмена' для отмены команды): ",
                    id => _accountFacade.BankAccountExists(id),
                    "Ошибка: Счёт с таким ID не найден."
                );
                _account = _accountFacade.GetBankAccount(accountId);
                _accountFacade.DeleteBankAccount(accountId);
                Console.WriteLine($"Счёт {accountId} удалён!");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Команда удаления счёта отменена.");
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
