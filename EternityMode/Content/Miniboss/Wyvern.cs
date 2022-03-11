﻿using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.EternityMode.Net;
using FargowiltasSouls.EternityMode.Net.Strategies;
using FargowiltasSouls.EternityMode.NPCMatching;
using FargowiltasSouls.Items.Accessories.Masomode;
using FargowiltasSouls.Projectiles.Masomode;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.EternityMode.Content.Miniboss
{
    public class Wyvern : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.WyvernHead);

        public int AttackTimer;

        public override Dictionary<Ref<object>, CompoundStrategy> GetNetInfo() =>
            new Dictionary<Ref<object>, CompoundStrategy> {
                { new Ref<object>(AttackTimer), IntStrategies.CompoundStrategy },
            };

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            AttackTimer = Main.rand.Next(180);
        }

        public override void OnSpawn(NPC npc)
        {
            base.OnSpawn(npc);

            if (Main.hardMode && Main.rand.NextBool(4))
                NPCs.EModeGlobalNPC.Horde(npc, 2);
        }

        public override void AI(NPC npc)
        {
            base.AI(npc);

            if (++AttackTimer > 240)
            {
                AttackTimer = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient && npc.velocity != Vector2.Zero)
                {
                    const int max = 12;
                    Vector2 vel = Vector2.Normalize(npc.velocity) * 1.5f;
                    for (int i = 0; i < max; i++)
                    {
                        Projectile.NewProjectile(npc.GetSpawnSource_ForProjectile(), npc.Center, vel.RotatedBy(2f * MathHelper.Pi / max * i),
                            ModContent.ProjectileType<LightBall>(), npc.damage / 5, 0f, Main.myPlayer, 0f, .01f * npc.direction);
                    }
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);

            EModeUtils.EModeDrop(npcLoot, ItemDropRule.Common(ItemID.FloatingIslandFishingCrate));
            EModeUtils.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<WyvernFeather>(), 5));
            EModeUtils.EModeDrop(npcLoot, ItemDropRule.Common(ItemID.CloudinaBottle, 20));
        }
    }

    public class WyvernSegment : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(
            NPCID.WyvernBody,
            NPCID.WyvernBody2,
            NPCID.WyvernBody3,
            NPCID.WyvernHead,
            NPCID.WyvernLegs,
            NPCID.WyvernTail
        );

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            if (Main.hardMode)
                npc.lifeMax *= 2;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            base.OnHitPlayer(npc, target, damage, crit);

            target.AddBuff(ModContent.BuffType<Crippled>(), 240);
            target.AddBuff(ModContent.BuffType<ClippedWings>(), 240);
        }
    }
}
