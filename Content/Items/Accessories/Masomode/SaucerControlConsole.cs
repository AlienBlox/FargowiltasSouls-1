﻿using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
    public class SaucerControlConsole : SoulsItem
    {
        public override bool Eternity => true;

        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 6);
        }

        public override void UpdateInventory(Player player)
        {
            player.FargoSouls().CanAmmoCycle = true;
        }

        public override void UpdateVanity(Player player)
        {
            player.FargoSouls().CanAmmoCycle = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.FargoSouls().CanAmmoCycle = true;

            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.AddEffect<UfoMinionEffect>(Item);

        }
    }
    public class UfoMinionEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<HeartHeader>();
        public override int ToggleItemType => ModContent.ItemType<SaucerControlConsole>();
        public override bool MinionEffect => true;
        public override void PostUpdateEquips(Player player)
        {
            if (!player.HasBuff<SouloftheMasochistBuff>())
                player.AddBuff(ModContent.BuffType<SaucerMinionBuff>(), 2);
        }

    }
}