﻿using FargowiltasSouls.EternityMode.Net;
using FargowiltasSouls.EternityMode.Net.Strategies;
using FargowiltasSouls.EternityMode.NPCMatching;
using FargowiltasSouls.ItemDropRules.Conditions;
using FargowiltasSouls.Items.Accessories.Masomode;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Projectiles.Masomode;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.EternityMode.Content.Enemy
{
    public class FireImp : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.FireImp);

        public int TeleportThreshold = 180;

        public int TeleportTimer;

        public bool DoTeleport;

        public override Dictionary<Ref<object>, CompoundStrategy> GetNetInfo() =>
            new Dictionary<Ref<object>, CompoundStrategy> {
                { new Ref<object>(TeleportTimer), IntStrategies.CompoundStrategy },

                { new Ref<object>(DoTeleport), BoolStrategies.CompoundStrategy },
            };

        public override void AI(NPC npc)
        {
            base.AI(npc);

            int teleportThreshold = npc.type == NPCID.Tim || npc.type == NPCID.RuneWizard ? 90 : 180;

            if (DoTeleport)
            {
                if (++TeleportTimer > teleportThreshold)
                {
                    TeleportTimer = 0;
                    DoTeleport = false;

                    if (Main.netMode != NetmodeID.MultiplayerClient && npc.HasPlayerTarget)
                    {
                        npc.ai[0] = 1f;
                        int num1 = (int)Main.player[npc.target].position.X / 16;
                        int num2 = (int)Main.player[npc.target].position.Y / 16;
                        int num3 = (int)npc.position.X / 16;
                        int num4 = (int)npc.position.Y / 16;
                        int num5 = 20;
                        int num6 = 0;
                        bool flag1 = false;
                        if ((double)Math.Abs(npc.position.X - Main.player[npc.target].position.X) + (double)Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000.0)
                        {
                            num6 = 100;
                            flag1 = true;
                        }
                        while (!flag1 && num6 < 100)
                        {
                            ++num6;
                            int index1 = Main.rand.Next(num1 - num5, num1 + num5);
                            for (int index2 = Main.rand.Next(num2 - num5, num2 + num5); index2 < num2 + num5; ++index2)
                            {
                                if ((index2 < num2 - 4 || index2 > num2 + 4 || (index1 < num1 - 4 || index1 > num1 + 4)) && (index2 < num4 - 1 || index2 > num4 + 1 || (index1 < num3 - 1 || index1 > num3 + 1)) && Main.tile[index1, index2].IsActiveUnactuated)
                                {
                                    bool flag2 = true;
                                    if (npc.HasValidTarget && Main.player[npc.target].ZoneDungeon && (npc.type == NPCID.DarkCaster || npc.type >= NPCID.RaggedCaster && npc.type <= NPCID.DiabolistWhite) && !Main.wallDungeon[(int)Main.tile[index1, index2 - 1].wall])
                                        flag2 = false;
                                    if (Main.tile[index1, index2 - 1].LiquidType == LiquidID.Lava && Main.tile[index1, index2 - 1].LiquidAmount > 0)
                                        flag2 = false;
                                    if (flag2 && Main.tileSolid[(int)Main.tile[index1, index2].type] && !Collision.SolidTiles(index1 - 1, index1 + 1, index2 - 4, index2 - 1))
                                    {
                                        npc.ai[1] = 20f;
                                        npc.ai[2] = (float)index1;
                                        npc.ai[3] = (float)index2;
                                        flag1 = true;
                                        break;
                                    }
                                }
                            }
                        }

                        npc.netUpdate = true;
                        NetSync(npc);
                    }
                }
            }

            if (npc.ai[0] == 0 && npc.ai[1] == 20 && npc.ai[2] > 0 && npc.ai[3] > 0)
            {
                TeleportTimer = 0;
                DoTeleport = false;
            }
        }

        public override void ModifyHitByAnything(NPC npc, Player player, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitByAnything(npc, player, ref damage, ref knockback, ref crit);
            
            DoTeleport = true;
            NetSync(npc, false);
        }
    }

    public class Tim : FireImp
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.Tim);

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            npc.buffImmune[BuffID.OnFire] = true;
            npc.lavaImmune = true;
            npc.lifeMax *= 4;
            npc.damage /= 2;
        }

        public override void AI(NPC npc)
        {
            base.AI(npc);

            EModeGlobalNPC.Aura(npc, 450, BuffID.Silenced, true, 15);
            EModeGlobalNPC.Aura(npc, 150, BuffID.Cursed, false, 20);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);

            npcLoot.Add(ItemDropRule.ByCondition(new EModeDropCondition(), ModContent.ItemType<TimsConcoction>(), 5));
        }
    }

    public class DungeonTeleporters : FireImp
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(
            NPCID.DiabolistRed,
            NPCID.DiabolistWhite,
            NPCID.Necromancer,
            NPCID.NecromancerArmored,
            NPCID.RaggedCaster,
            NPCID.RaggedCasterOpenCoat
        );

        public override void AI(NPC npc)
        {
            if (npc.HasValidTarget && !Main.player[npc.target].ZoneDungeon && !DoTeleport)
            {
                DoTeleport = true;
                TeleportTimer = TeleportThreshold - 420; //occasionally teleport outside dungeon
            }

            base.AI(npc);
        }
    }

    public class DarkCaster : DungeonTeleporters
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.DarkCaster);

        public int AttackTimer;

        public override void AI(NPC npc)
        {
            base.AI(npc);

            if (++AttackTimer > 300)
            {
                AttackTimer = 0;
                for (int i = 0; i < 5; i++) //spray water bolts
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item21, npc.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(npc.GetProjectileSpawnSource(), npc.Center, Main.rand.NextVector2CircularEdge(-4.5f, 4.5f), ModContent.ProjectileType<WaterBoltHostile>(), npc.damage / 4, 0f, Main.myPlayer);
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);

            npcLoot.Add(ItemDropRule.ByCondition(new EModeDropCondition(), ItemID.WaterBolt, 50));
        }
    }
}
