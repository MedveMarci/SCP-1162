using System;
using CustomPlayerEffects;
using InventorySystem.Items;
using InventorySystem.Items.Usables.Scp330;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using Random = UnityEngine.Random;

namespace SCP1162;

public class EventHandler : CustomEventsHandler
{
    private static void GiveItem(Player player)
    {
        try
        {
            if (Plugin.Instance.Config == null) return;
            var items = Plugin.Instance.Config.ItemsToGive;

            player.SendHint(Plugin.Instance.Config.InteractionHint, 20);
            if (player.CurrentItem != null) player.RemoveItem(player.CurrentItem);
            player.CurrentItem = null;

            Timing.CallDelayed(0.1f, () =>
            {
                if (Random.Range(0f, 100f) < Plugin.Instance.Config.PercentCandy)
                {
                    player.GiveCandy(CandyKindID.Pink, ItemAddReason.PickedUp);
                }
                else
                {
                    var randomItem = items.RandomItem();
                    var item = player.AddItem(randomItem);
                    player.CurrentItem = item;
                }
            });
        }
        catch (Exception e)
        {
            LogManager.Error($"Error while giving item from SCP-1162: {e.Message}");
        }
    }

    public override void OnPlayerInteractedToy(PlayerInteractedToyEventArgs ev)
    {
        try
        {
            LogManager.Debug($"Player {ev.Player.Nickname} interacted with {ev.Interactable.GameObject.name}");
            if (ev.Interactable.GameObject.name != "SCP-1162") return;
            if (Plugin.Instance.Config == null) return;
            var percentDisappearing = Plugin.Instance.Config.PercentDisappearing;
            if (ev.Player.CurrentItem != null)
            {
                if (ev.Player.CurrentItem.Type == ItemType.Snowball)
                {
                    ev.Player.SendHint("<color=red>Hógolyóval nem használatod az SCP-1162!</color>");
                    return;
                }

                if (percentDisappearing == 0 || Random.Range(0f, 100f) > percentDisappearing)
                {
                    GiveItem(ev.Player);
                }
                else
                {
                    if (ev.Player.CurrentItem != null) ev.Player.RemoveItem(ev.Player.CurrentItem);
                    ev.Player.SendHint(Plugin.Instance.Config.LostItemHint);
                }
            }
            else
            {
                if (Plugin.Instance.Config.HealthMinus == 0) return;
                ev.Player.Damage(Plugin.Instance.Config.HealthMinus, "SCP-1162");
                ev.Player.EnableEffect<Burned>(3);
                ev.Player.SendHint(Plugin.Instance.Config.DamageHint);
            }
        }
        catch (Exception e)
        {
            LogManager.Error($"Error while interacting with SCP-1162: {e.Message}");
        }

        base.OnPlayerInteractedToy(ev);
    }

    public override void OnServerWaitingForPlayers()
    {
        _ = VersionManager.CheckForUpdatesAsync(Plugin.Instance.Version);
        base.OnServerWaitingForPlayers();
    }
}