﻿using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.EternityMode.Net;
using FargowiltasSouls.EternityMode.Net.Strategies;
using FargowiltasSouls.EternityMode.NPCMatching;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Projectiles;
using FargowiltasSouls.Projectiles.Masomode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.EternityMode.Content.Enemy.Night
{
    public class HoppinJack : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.HoppinJack);

        public int Counter;

        public override void AI(NPC npc)
        {
            base.AI(npc);

            if (npc.life < npc.lifeMax && ++Counter >= 20 && npc.velocity.X != 0)
            {
                Counter = 0;
                Projectile.NewProjectile(npc.GetSpawnSource_ForProjectile(), npc.Center.X, npc.position.Y, Main.rand.Next(-3, 4), Main.rand.Next(-4, 0), Main.rand.Next(326, 329), npc.damage / 5, 0f, Main.myPlayer);
            }
        }
    }
}
