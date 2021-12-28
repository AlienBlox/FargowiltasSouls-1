﻿using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.EternityMode.Net;
using FargowiltasSouls.EternityMode.Net.Strategies;
using FargowiltasSouls.EternityMode.NPCMatching;
using FargowiltasSouls.Items.Accessories.Masomode;
using FargowiltasSouls.Projectiles.Masomode;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.EternityMode.Content.Miniboss
{
    public class RainbowSlime : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.RainbowSlime);

        public int SpawnSyncTimer;

        public bool SpawnedByOtherSlime;
        public bool DoStompAttack;

        public override Dictionary<Ref<object>, CompoundStrategy> GetNetInfo() =>
            new Dictionary<Ref<object>, CompoundStrategy> {
                { new Ref<object>(SpawnSyncTimer), IntStrategies.CompoundStrategy },

                { new Ref<object>(SpawnedByOtherSlime), BoolStrategies.CompoundStrategy },
                { new Ref<object>(DoStompAttack), BoolStrategies.CompoundStrategy },
            };

        public override void AI(NPC npc)
        {
            base.AI(npc);

            if (++SpawnSyncTimer == 15) //delayed for mp leeway
            {
                npc.netUpdate = true;
                NetSync(npc);
            }
            else if (SpawnSyncTimer == 30)
            {
                if (!SpawnedByOtherSlime) //slime become big
                {
                    npc.lifeMax *= 5;
                    npc.life = npc.lifeMax;
                    npc.HealEffect(npc.lifeMax);

                    npc.Center = npc.Bottom;
                    npc.scale *= 3;
                    npc.width *= 3;
                    npc.height *= 3;
                    npc.Bottom = npc.Center;
                }

                npc.netUpdate = true;
            }

            if (DoStompAttack) //shoot spikes whenever jumping
            {
                if (npc.velocity.Y == 0f) //start attack
                {
                    DoStompAttack = false;
                    if (npc.HasPlayerTarget && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        const float gravity = 0.15f;
                        const float time = 120f;
                        Vector2 distance = Main.player[npc.target].Center - npc.Center;
                        distance += Main.player[npc.target].velocity * 30f;
                        distance.X = distance.X / time;
                        distance.Y = distance.Y / time - 0.5f * gravity * time;
                        float ai0 = SpawnedByOtherSlime ? 1 : 0;
                        int max = SpawnedByOtherSlime ? 3 : 25;
                        float spread = SpawnedByOtherSlime ? 0.5f : 1.5f;
                        for (int i = 0; i < max; i++)
                        {
                            Projectile.NewProjectile(npc.Center, distance + spread * Main.rand.NextVector2Circular(-1f, 1f),
                                ModContent.ProjectileType<RainbowSlimeSpike>(), npc.damage / 8, 0f, Main.myPlayer, ai0);
                        }
                    }
                }
            }
            else if (npc.velocity.Y > 0)
            {
                DoStompAttack = true;
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            base.OnHitPlayer(npc, target, damage, crit);

            target.AddBuff(BuffID.Slimed, 120);
            target.AddBuff(ModContent.BuffType<FlamesoftheUniverse>(), 240);
        }

        public override bool CheckDead(NPC npc)
        {
            if (!SpawnedByOtherSlime)
            {
                npc.active = false;
                Main.PlaySound(npc.DeathSound);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int slimeIndex = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.RainbowSlime);
                        if (slimeIndex != Main.maxNPCs)
                        {
                            NPC slime = Main.npc[slimeIndex];

                            slime.position = slime.Center;
                            slime.width = (int)(slime.width / slime.scale);
                            slime.height = (int)(slime.height / slime.scale);
                            slime.scale = 1f;
                            slime.Center = slime.position;

                            slime.lifeMax /= 5;
                            slime.life = slime.lifeMax;

                            slime.GetEModeNPCMod<RainbowSlime>().SpawnedByOtherSlime = true;
                            slime.velocity = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 1));

                            if (Main.netMode == NetmodeID.Server)
                                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, slimeIndex);
                        }
                    }
                }
                for (int i = 0; i < 20; i++)
                {
                    int num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, DustID.RainbowMk2, -npc.velocity.X * 0.2f, -npc.velocity.Y * 0.2f, 100, default(Color), 5f);
                    Main.dust[num469].noGravity = true;
                    Main.dust[num469].velocity *= 2f;
                    num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, DustID.RainbowMk2, -npc.velocity.X * 0.2f, -npc.velocity.Y * 0.2f, 100, default(Color), 2f);
                    Main.dust[num469].velocity *= 2f;
                }
                return false;
            }
            else
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int[] slimes = { NPCID.Crimslime, NPCID.Pinky, NPCID.Gastropod, NPCID.CorruptSlime };

                    for (int i = 0; i < slimes.Length; i++)
                    {
                        if (Main.rand.Next(3) != 0)
                            continue;

                        int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);
                        Main.npc[spawn].SetDefaults(slimes[i]);
                        Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                        Main.npc[spawn].velocity.Y = npc.velocity.Y;

                        NPC spawn2 = Main.npc[spawn];
                        spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                        NPC spawn3 = Main.npc[spawn];
                        spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                        Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);

                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, spawn);
                    }
                }
            }

            return base.CheckDead(npc);
        }

        public override void NPCLoot(NPC npc)
        {
            base.NPCLoot(npc);

            if (SpawnedByOtherSlime && Main.rand.NextBool(10))
                Item.NewItem(npc.Hitbox, ModContent.ItemType<ConcentratedRainbowMatter>());
        }
    }
}
