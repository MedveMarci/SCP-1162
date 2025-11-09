using System;
using System.Linq;
using AdminToys;
using CommandSystem;
using LabApi.Features.Wrappers;
using UnityEngine;
using PrimitiveObjectToy = LabApi.Features.Wrappers.PrimitiveObjectToy;
using Random = UnityEngine.Random;
using TextToy = LabApi.Features.Wrappers.TextToy;

namespace SCP1162.de;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class RespawnCommand : ICommand
{
    public string Command { get; } = "scp1162respawn";
    public string[] Aliases { get; } = ["1162respawn"];
    public string Description { get; } = "Respawns every SCP-1162.";
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        foreach (var primitiveObjectToy in EventHandler.ParentToys)
            primitiveObjectToy.Destroy();
        EventHandler.ParentToys.Clear();
        
        foreach (var customRoomLocation in Plugin.Instance.Config.CustomRoomLocations)
        {
            if (Random.Range(0f, 100f) > customRoomLocation.Chance)
                continue;
            
            var room = Room.List.FirstOrDefault(room => room.Name == customRoomLocation.RoomName);
            if (room == null)
            {
                LogManager.Error($"Room {customRoomLocation.RoomName} not found for SCP-1162 spawn.");
                response = $"Room {customRoomLocation.RoomName} not found.";
                return false;
            }
            
            var mainOffset = room.Transform.TransformPoint(customRoomLocation.Offset);
            var parentToy = PrimitiveObjectToy.Create();
            
            parentToy.Type = PrimitiveType.Cube;
            parentToy.Flags = PrimitiveFlags.None;
            parentToy.GameObject.name = $"SCP1162-{room.Name}-{Guid.NewGuid()}";
            parentToy.Position = mainOffset;
            parentToy.Rotation = Quaternion.Euler(customRoomLocation.Rotation);
            
            foreach (var toyData in EventHandler.TextToys)
            {
                var textToy = TextToy.Create(parentToy.Transform);
                textToy.GameObject.name = "Scp1162Text";
                textToy.TextFormat = toyData.Text;
                textToy.Position = toyData.Offset;
                textToy.Scale = new Vector3(0.775f, 0.775f, 0.775f);
                textToy.Rotation = new Quaternion(0, 0, 1,0);
            }

            var toy = InteractableToy.Create(parentToy.Transform);
            toy.GameObject.name = EventHandler.InteractableToyData.Name;
            toy.Position = EventHandler.InteractableToyData.Offset;
            toy.Scale = EventHandler.InteractableToyData.Scale;
            LogManager.Debug($"Created SCP-1162 at {toy.GameObject.transform.position}");
            EventHandler.ParentToys.Add(parentToy);
        }
        response = "SCP-1162 spawn/despawn executed.";
        return true;
    }

}