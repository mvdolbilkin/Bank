using Bank.commands;
using Bank.commands.BankAccountCommand;
using Bank.commands.CategoryCommand;
using Bank.commands.OperationCommand;
using Bank.facade;
using Bank.factory;
using Bank.services;
using Microsoft.Extensions.DependencyInjection;
using System;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();

        var commandProcessor = serviceProvider.GetRequiredService<CommandProcessor>();
        var bankAccountFacade = serviceProvider.GetRequiredService<BankAccountFacade>();
        var categoryFacade = serviceProvider.GetRequiredService<CategoryFacade>();
        var operationFacade = serviceProvider.GetRequiredService<OperationFacade>();
        var analyticsFacade = serviceProvider.GetRequiredService<AnalyticsFacade>();


        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Управление счетами");
            Console.WriteLine("2. Управление категориями");
            Console.WriteLine("3. Управление операциями");
            Console.WriteLine("4. Показать аналитику");
            Console.WriteLine("0. Выход");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ManageBankAccounts(commandProcessor);
                    break;
                case "2":
                    ManageCategories(commandProcessor);
                    break;
                case "3":
                    ManageOperations(commandProcessor);
                    break;
                case "4":
                    ShowAnalytics(analyticsFacade);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<BankFactory>();
        services.AddSingleton<BankAccountFacade>();
        services.AddSingleton<CategoryFacade>();
        services.AddSingleton<OperationFacade>();
        
        services.AddSingleton<AnalyticsFacade>(provider => 
            new AnalyticsFacade(
                provider.GetRequiredService<OperationFacade>().Operations,
                provider.GetRequiredService<CategoryFacade>().Categories
            )
        );

        services.AddTransient<CreateBankAccountCommand>();
        services.AddTransient<DeleteBankAccountCommand>();
        services.AddTransient<EditBankAccountCommand>();
        services.AddTransient<CreateCategoryCommand>();
        services.AddTransient<DeleteCategoryCommand>();
        services.AddTransient<EditCategoryCommand>();
        services.AddTransient<CreateOperationCommand>();
        services.AddTransient<DeleteOperationCommand>();
        services.AddTransient<EditOperationCommand>();

        services.AddSingleton<CommandProcessor>();
    }

    private static void ManageBankAccounts(CommandProcessor commandProcessor)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Управление счетами ---");
            Console.WriteLine("1. Создать счет");
            Console.WriteLine("2. Удалить счет");
            Console.WriteLine("3. Редактировать счет");
            Console.WriteLine("4. Показать все счета");
            Console.WriteLine("0. Назад");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    commandProcessor.ProcessCommand<CreateBankAccountCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "2":
                    Console.Clear();
                    commandProcessor.ProcessCommand<DeleteBankAccountCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "3":
                    Console.Clear();
                    commandProcessor.ProcessCommand<EditBankAccountCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "4":
                    Console.Clear();
                    commandProcessor.BankAccountFacade.ShowAllBankAccounts();
                    PressAnyKeyToContinue();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

     private static void ManageCategories(CommandProcessor commandProcessor)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Управление категориями ---");
            Console.WriteLine("1. Создать категорию");
            Console.WriteLine("2. Удалить категорию");
            Console.WriteLine("3. Редактировать категорию");
            Console.WriteLine("4. Показать все категории");
            Console.WriteLine("0. Назад");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    commandProcessor.ProcessCommand<CreateCategoryCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "2":
                    Console.Clear();
                    commandProcessor.ProcessCommand<DeleteCategoryCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "3":
                    Console.Clear();
                    commandProcessor.ProcessCommand<EditCategoryCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "4":
                    Console.Clear();
                    commandProcessor.CategoryFacade.ShowAllCategories();
                    PressAnyKeyToContinue();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

    private static void ManageOperations(CommandProcessor commandProcessor)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Управление операциями ---");
            Console.WriteLine("1. Создать операцию");
            Console.WriteLine("2. Удалить операцию");
            Console.WriteLine("3. Редактировать описание операции");
            Console.WriteLine("4. Показать все операции");
            Console.WriteLine("5. Показать операции по счету");
            Console.WriteLine("0. Назад");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    commandProcessor.ProcessCommand<CreateOperationCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "2":
                    Console.Clear();
                    commandProcessor.ProcessCommand<DeleteOperationCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "3":
                    Console.Clear();
                    commandProcessor.ProcessCommand<EditOperationCommand>();
                    PressAnyKeyToContinue();
                    break;
                case "4":
                    Console.Clear();
                    commandProcessor.OperationFacade.ShowAllOperations();
                    PressAnyKeyToContinue();
                    break;
                case "5":
                    Console.Clear();
                    commandProcessor.OperationFacade.ShowOperationsForAccountId();
                    PressAnyKeyToContinue();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

    private static void ShowAnalytics(AnalyticsFacade analyticsFacade)
    {
         while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Аналитика ---");
            Console.WriteLine("1. Доходы и расходы за период");
            Console.WriteLine("2. Группировка по категориям");
            Console.WriteLine("0. Назад");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("Введите начальную дату (дд.мм.гггг):");
                        DateTime startDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Введите конечную дату (дд.мм.гггг):");
                        DateTime endDate = DateTime.Parse(Console.ReadLine());
                        analyticsFacade.CalculateIncomeAndExpense(startDate, endDate);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: Неверный формат даты.");
                    }
                    PressAnyKeyToContinue();
                    break;
                case "2":
                    Console.Clear();
                    analyticsFacade.GroupByCategory();
                    PressAnyKeyToContinue();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

    private static void PressAnyKeyToContinue()
    {
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}
