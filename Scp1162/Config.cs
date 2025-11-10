using System.Collections.Generic;
using System.ComponentModel;
using InventorySystem.Items.Usables.Scp330;
using MapGeneration;
using UnityEngine;
using YamlDotNet.Serialization;

namespace SCP1162;

public class Config
{
    [Description("Enable debug messages")] public bool Debug { get; set; } = false;

    [YamlIgnore]
    [Description("Should SCP-1162 use pickup interaction?")]
    public bool UsePickup { get; set; } = false;

    [Description("Can Scp-3114 use SCP-1162?")]
    public bool CanScp3114Use { get; set; } = false;

    [Description("Should Scp-1162 give the weapon with ammo (if the item given is a weapon)")]
    public bool GiveWeaponWithAmmo { get; set; } = false;

    [Description(
        "How much damage should players get when using Scp-1162 without holding an item in hand (set to 0 to disable)")]
    public int HealthMinus { get; set; } = 25;

    [Description("The chance that the item disappears in % (set to 0 to disable)")]
    public float PercentDisappearing { get; set; } = 30;

    [Description("What items should Scp-1162 be able to give")]
    public List<ItemType> ItemsToGive { get; set; } =
    [
        ItemType.KeycardJanitor,
        ItemType.KeycardZoneManager,
        ItemType.KeycardScientist,
        ItemType.KeycardContainmentEngineer,
        ItemType.KeycardResearchCoordinator,
        ItemType.KeycardMTFPrivate,
        ItemType.KeycardMTFOperative,
        ItemType.KeycardMTFCaptain,
        ItemType.KeycardFacilityManager,
        ItemType.KeycardChaosInsurgency,
        ItemType.KeycardO5,
        ItemType.GunCOM15,
        ItemType.GunCOM18,
        ItemType.Painkillers,
        ItemType.Medkit,
        ItemType.Adrenaline,
        ItemType.SCP500,
        ItemType.SCP207,
        ItemType.AntiSCP207,
        ItemType.GrenadeHE,
        ItemType.GrenadeFlash,
        ItemType.Coin,
        ItemType.Flashlight,
        ItemType.Radio
    ];

    [Description("The chance that the item will be a random candy in % (set to 0 to disable)")]
    public float PercentCandy { get; set; } = 10;

    [Description("What candies should Scp-1162 be able to give")]
    public List<CandyKindID> CandiesToGive { get; set; } =
    [
        CandyKindID.Rainbow,
        CandyKindID.Yellow,
        CandyKindID.Purple,
        CandyKindID.Red,
        CandyKindID.Green,
        CandyKindID.Blue,
        CandyKindID.Pink,
        CandyKindID.Orange,
        CandyKindID.White,
        CandyKindID.Gray,
        CandyKindID.Black,
        CandyKindID.Brown,
        CandyKindID.Evil
    ];

    [Description("Message sent when interacting with Scp-1162 without holding an item in hand")]
    public string DamageHint { get; set; } = "<color=red>Dont stick your Hand in unknown holes ( ͡° ͜ʖ ͡° )</color>";

    [Description("Message sent when interacting with Scp-1162")]
    public string InteractionHint { get; set; } =
        "<i>You put an item into </i><color=yellow>SCP-1162</color><i> and got Another!</i>";

    [Description("Message sent when the item disappears")]
    public string LostItemHint { get; set; } = "<color=red>You lost your item</color>";

    [Description("This option is for a custom SCP-1162 location")]
    public List<CustomRoomLocationData> CustomRoomLocations { get; set; } =
    [
        new()
        {
            RoomName = RoomName.Lcz173,
            Offset = new Vector3(16f, 12.7f, 3.7f),
            Rotation = new Vector3(0f, 0f, 0f),
            Chance = 100f
        }
    ];

    public class CustomRoomLocationData
    {
        public RoomName RoomName { get; set; }
        public Vector3 Offset { get; set; }
        public Vector3 Rotation { get; set; }
        public float Chance { get; set; }
    }
}