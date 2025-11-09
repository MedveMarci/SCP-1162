using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace SCP1162;

public class Plugin : Plugin<Config>
{
    public static Plugin Instance;
    public override string Description => "A mysterious hole that gives random items.";
    public override string Name => "SCP-1162";
    public override string Author => "MedveMarci";
    public override Version Version { get; } = new(1, 1, 0);
    public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);
    private EventHandler EventHandler { get; } = new();
    public string githubRepo = "MedveMarci/SCP-1162";
    public override void Enable()
    {
        Instance = this;
        CustomHandlersManager.RegisterEventsHandler(EventHandler);
    }


    public override void Disable()
    {
        CustomHandlersManager.RegisterEventsHandler(EventHandler);
        Instance = this;
    }
}