using Bank.commands.BankAccountCommand;
using Bank.commands.CategoryCommand;
using Bank.commands.OperationCommand;
using Bank.facade;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Bank.commands
{
    public class CommandProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        public BankAccountFacade BankAccountFacade { get; }
        public CategoryFacade CategoryFacade { get; }
        public OperationFacade OperationFacade { get; }

        public CommandProcessor(IServiceProvider serviceProvider, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
        {
            _serviceProvider = serviceProvider;
            BankAccountFacade = bankAccountFacade;
            CategoryFacade = categoryFacade;
            OperationFacade = operationFacade;
        }

        public void ProcessCommand<T>() where T : ICommand
        {
            var command = _serviceProvider.GetRequiredService<T>();
            var timedCommand = new TimingCommandDecorator(command);
            timedCommand.Execute();
        }
    }

    public class TimingCommandDecorator(ICommand wrappedCommand) : ICommand
    {
        private readonly ICommand _wrappedCommand = wrappedCommand;

        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            _wrappedCommand.Execute();
            stopwatch.Stop();
            Console.WriteLine($"[ИНФО] Команда '{_wrappedCommand.GetType().Name}' выполнена за {stopwatch.ElapsedMilliseconds} мс.");
        }

        public void Redo()
        {
            _wrappedCommand.Redo();
        }

        public void Undo()
        {
            _wrappedCommand.Undo();
        }
    }
}
