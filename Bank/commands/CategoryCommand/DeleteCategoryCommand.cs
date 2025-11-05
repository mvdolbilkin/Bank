using Bank.facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.services;

namespace Bank.commands.CategoryCommand
{
    public class DeleteCategoryCommand(CategoryFacade categoryFacade) : ICommand
    {
        private readonly CategoryFacade _categoryFacade = categoryFacade;

        public void Execute()
        {
            try
            {
                int categoryId = InputHelper.ReadInt(
                    "Введите ID категории: ",
                    id => _categoryFacade.CategoryExists(id),
                    "Ошибка: Категория с таким ID не найдена."
                );
                _categoryFacade.DeleteCategory(categoryId);
                Console.WriteLine($"Категория [{categoryId}] удалена!");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Удаление категории отменено.");
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
