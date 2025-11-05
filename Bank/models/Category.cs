using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.models
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public CategoryType Type { get; private set; }
        public Category(int id, string name, CategoryType type)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Имя категории не может быть пустым.");
            }
            Id = id;
            Name = name;
            Type = type;
        }
        public void Update(string name, CategoryType type)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Имя категории не может быть пустым.");
            }
            Name = name;
            Type = type;
        }
    }
    public enum CategoryType
    {
        Income,
        Expense
    }
}
