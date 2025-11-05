using Bank.facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.services;

namespace Bank.commands.OperationCommand
{
    public class DeleteOperationCommand(OperationFacade operationFacade) : ICommand
    {
        private readonly OperationFacade _operationFacade = operationFacade;

        public void Execute()
        {
            try
            {
                int operationId = InputHelper.ReadInt(
                    "Введите ID операции: ",
                    id => _operationFacade.OperationExists(id),
                    "Ошибка: Операция с таким ID не найдена."
                );
                _operationFacade.DeleteOperation(operationId);
                Console.WriteLine($"Операция {operationId} удалена!");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Удаление операции отменено.");
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
