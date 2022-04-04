﻿using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.Items.Misc;

namespace FargowiltasSouls.Items.BossBags
{
    public class DeviBag : BossBag
    {
        protected override bool IsPreHMBag => true;

        public override int BossBagNPC => ModContent.NPCType<NPCs.DeviBoss.DeviBoss>();

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(player.GetItemSource_OpenItem(Item.type), ModContent.ItemType<DeviatingEnergy>(), Main.rand.Next(16) + 15);
        }
    }
}