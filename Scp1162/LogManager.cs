using System;
using Logger = LabApi.Features.Console.Logger;

namespace SCP1162;

internal class LogManager
{
    private static bool DebugEnabled => Plugin.Instance.Config!.Debug;

    public static void Debug(string message)
    {
        if (!DebugEnabled)
            return;

        Logger.Raw($"[DEBUG] [{Plugin.Instance.Name}] {message}", ConsoleColor.Green);
    }

    public static void Info(string message, ConsoleColor color = ConsoleColor.Cyan)
    {
        Logger.Raw($"[INFO] [{Plugin.Instance.Name}] {message}", color);
    }

    public static void Warn(string message)
    {
        Logger.Warn(message);
    }

    public static void Error(string message)
    {
        Logger.Error(message);
    }
}