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
    public class CreateCategoryCommand(CategoryFacade categoryFacade) : ICommand
    {
        private readonly CategoryFacade _categoryFacade = categoryFacade;
        private int _id;
        private string? _name;
        private CategoryType _type;


        public void Execute()
        {
            try
            {
                _id = new Random().Next(1, 100);

                _name = InputHelper.ReadString(
                    "Введите имя категории: ",
                    input => !string.IsNullOrWhiteSpace(input),
                    "Ошибка: Имя категории не может быть пустым."
                );

                _type = InputHelper.ReadEnum<CategoryType>("Выберите тип категории (Income/Expense):");

                var category = _categoryFacade.CreateCategory(_id, _name, _type);
                Console.WriteLine($"Категория '{_name}' успешно создана! ID: {_id}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Создание категории отменено.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Ошибка создания категории.");
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
