using System;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Eplicta.Mets.Console.Commands;
using Tharga.Toolkit.Console;
using Tharga.Toolkit.Console.Commands;
using Tharga.Toolkit.Console.Consoles;
using Tharga.Toolkit.Console.Helpers;
using Tharga.Toolkit.Console.Interfaces;

namespace Eplicta.Mets.Console
{
     internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var container = GetContainer();

        using var console = new ClientConsole();
        var command = new RootCommand(console, new CommandResolver(type => (ICommand)container.Resolve(type)));
        command.RegisterCommand<MetsConsoleCommands>();
        var engine = new CommandEngine(command);
        engine.Start(args);
    }

    private static WindsorContainer GetContainer()
    {
        var container = new WindsorContainer();
        container.Register(Classes.FromAssemblyInThisApplication(Assembly.GetAssembly(typeof(Program))).IncludeNonPublicTypes().BasedOn<ICommand>().Configure(x => System.Diagnostics.Debug.WriteLine($"Registered in IOC: {x.Implementation.Name}")).Configure(x => x.LifeStyle.Is(LifestyleType.Transient)));
        return container;
    }
}
}

