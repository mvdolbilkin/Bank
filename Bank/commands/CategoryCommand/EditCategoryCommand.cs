using Bank.facade;
using Bank.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.services;

namespace Bank.commands.CategoryCommand
{
    public class EditCategoryCommand(CategoryFacade categoryFacade) : ICommand
    {
        private readonly CategoryFacade _categoryFacade = categoryFacade;


        public void Execute()
        {
            try
            {
                _categoryFacade.ShowAllCategories();
                int categoryId = InputHelper.ReadInt(
                    "Введите ID категории для редактирования: ",
                    id => _categoryFacade.CategoryExists(id),
                    "Ошибка: Категория с таким ID не найдена."
                );

                string newName = InputHelper.ReadString(
                    "Введите новое имя для категории: ",
                    name => !string.IsNullOrWhiteSpace(name),
                    "Ошибка: Имя не может быть пустым."
                );

                CategoryType newType = InputHelper.ReadEnum<CategoryType>("Введите новый тип категории (Income/Expense): ");

                _categoryFacade.UpdateCategory(categoryId, newName, newType);
                Console.WriteLine("Категория успешно обновлена.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Редактирование категории отменено.");
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
