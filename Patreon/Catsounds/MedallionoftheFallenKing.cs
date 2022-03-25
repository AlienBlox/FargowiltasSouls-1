﻿using FargowiltasSouls.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Patreon.Catsounds
{
    public class MedallionoftheFallenKing : PatreonModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Medallion of the Fallen King");
            Tooltip.SetDefault(
@"Spawns a King Slime Minion that scales with summon damage");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 1;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<KingSlimeMinionBuff>(), 2);
        }
    }
}