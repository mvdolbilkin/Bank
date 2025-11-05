using Bank.factory;
using Bank.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.facade
{
    public class CategoryFacade(BankFactory factory)
    {
        private readonly BankFactory _factory = factory;
        private readonly List<Category> _categories = [];

        public List<Category> Categories => _categories;

        public Category CreateCategory(int id, string name, CategoryType type)
        {
            var category = _factory.CreateCategory(id, name, type);
            _categories.Add(category);
            return category;
        }

        public Category GetCategory(int id)
        {
            var category = _categories.FirstOrDefault(category => category.Id == id) ?? throw new InvalidOperationException($"Ошибка: Категория с ID {id} не найдена.");
            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }

        public bool CategoryExists(int id)
        {
            return _categories.Any(c => c.Id == id);
        }

        public void ShowAllCategories()
        {
            int index = 1;
            Console.WriteLine("Список категорий:");
            foreach (Category category in _categories)
            {
                Console.WriteLine($"{index}. [{category.Id}] {category.Name}\t{category.Type}");
                index++;
            }
            if (_categories.Count == 0)
            {
                Console.WriteLine("Категорий нет.");
            }
        }
        public void UpdateCategory(int id, string name, CategoryType type)
        {
            var category = GetCategory(id);
            category.Update(name, type);
        }
    }
}
