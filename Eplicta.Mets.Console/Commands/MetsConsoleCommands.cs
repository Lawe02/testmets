namespace Eplicta.Mets.Console.Commands
{

public class MetsConsoleCommands : Tharga.Toolkit.Console.Commands.Base.ContainerCommandBase
{
    public MetsConsoleCommands()
        : base("mets")
    {
        RegisterCommand<CreateConsoleCommand>();
    }
}
}