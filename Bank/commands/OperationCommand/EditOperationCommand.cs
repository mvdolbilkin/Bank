using Bank.facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.services;

namespace Bank.commands.OperationCommand
{
    public class EditOperationCommand(OperationFacade operationFacade) : ICommand
    {
        private readonly OperationFacade _operationFacade = operationFacade;
        public void Execute()
        {
            try
            {
                _operationFacade.ShowAllOperations();
                int operationId = InputHelper.ReadInt(
                    "Введите ID операции для редактирования: ",
                    id => _operationFacade.OperationExists(id),
                    "Ошибка: Операция с таким ID не найдена."
                );

                string newDescription = InputHelper.ReadString(
                    "Введите новое описание для операции: ",
                    _ => true,
                    ""
                );

                _operationFacade.UpdateOperation(operationId, newDescription);
                Console.WriteLine("Операция успешно обновлена.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Редактирование операции отменено.");
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
