using Bank.models;

namespace Bank.services
{
    public static class InputHelper
    {
        public static int ReadInt(string prompt, Func<int, bool>? validation = null, string errorMessage = "Ошибка: Неверный ввод.")
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.Equals(input, "отмена", StringComparison.OrdinalIgnoreCase))
                {
                    throw new OperationCanceledException("Команда отменена пользователем.");
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: Ввод не может быть пустым.");
                    continue;
                }

                if (!int.TryParse(input, out int result))
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }

                if (validation != null && !validation(result))
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }

                return result;
            }
        }

        public static decimal ReadDecimal(string prompt, Func<decimal, bool>? validation = null, string errorMessage = "Ошибка: Неверный ввод.")
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.Equals(input, "отмена", StringComparison.OrdinalIgnoreCase))
                {
                    throw new OperationCanceledException("Команда отменена пользователем.");
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: Ввод не может быть пустым.");
                    continue;
                }

                if (!decimal.TryParse(input, out decimal result))
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }

                if (validation != null && !validation(result))
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }

                return result;
            }
        }

        public static string ReadString(string prompt, Func<string, bool>? validation = null, string errorMessage = "Ошибка: Неверный ввод.")
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.Equals(input, "отмена", StringComparison.OrdinalIgnoreCase))
                {
                    throw new OperationCanceledException("Команда отменена пользователем.");
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: Ввод не может быть пустым.");
                    continue;
                }

                if (validation != null && !validation(input))
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }

                return input;
            }
        }

        public static string ReadSimpleString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.Equals(input, "отмена", StringComparison.OrdinalIgnoreCase))
                {
                    throw new OperationCanceledException("Команда отменена пользователем.");
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    input = "";
                }

                return input;
            }
        }

        public static T ReadEnum<T>(string prompt) where T : struct, Enum
        {
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.Equals(input, "отмена", StringComparison.OrdinalIgnoreCase))
                {
                    throw new OperationCanceledException("Команда отменена пользователем.");
                }

                if (Enum.TryParse<T>(input, true, out T result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine($"Неверный ввод. Пожалуйста, введите одно из следующих значений: {string.Join(", ", Enum.GetNames(typeof(T)))}");
                }
            }
            while (true);
        }
    }
}
