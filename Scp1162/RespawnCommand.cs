using System;
using CommandSystem;

namespace SCP1162.de;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class Scp1162RespawnCommand : ICommand
{
    public string Command => "scp1162respawn";
    public string[] Aliases { get; } = ["1162respawn"];
    public string Description => "Respawns every SCP-1162.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        EventHandler.DestroyScp1162();
        EventHandler.SpawnScp1162();
        response = "SCP-1162 spawn/despawn executed.";
        return true;
    }
}