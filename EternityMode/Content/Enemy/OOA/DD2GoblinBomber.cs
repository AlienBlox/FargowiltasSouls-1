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
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.EternityMode.Content.Enemy.OOA
{
    public class DD2GoblinBomber : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(
            NPCID.DD2GoblinBomberT1,
            NPCID.DD2GoblinBomberT2,
            NPCID.DD2GoblinBomberT3
        );

        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(npc.GetSpawnSource_ForProjectile(), npc.Center,
                        new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-9f, -6f)),
                        ProjectileID.DD2GoblinBomb, npc.damage / 4, 0, Main.myPlayer);
                }
            }
        }
    }
}
