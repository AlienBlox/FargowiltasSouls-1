﻿using Fargowiltas.Items.Summons;
using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.EternityMode.Net;
using FargowiltasSouls.EternityMode.Net.Strategies;
using FargowiltasSouls.EternityMode.NPCMatching;
using FargowiltasSouls.Items.Accessories.Masomode;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Projectiles.Masomode;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.EternityMode.Content.Boss.PHM
{
    public class EaterofWorlds : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail);

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            npc.buffImmune[BuffID.CursedInferno] = true;
        }

        public override void NPCLoot(NPC npc)
        {
            base.NPCLoot(npc);

            bool dropItems = true;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && i != npc.whoAmI && (Main.npc[i].type == NPCID.EaterofWorldsHead || Main.npc[i].type == NPCID.EaterofWorldsBody || Main.npc[i].type == NPCID.EaterofWorldsTail))
                {
                    dropItems = false;
                    break;
                }
            }
            if (dropItems)
            {
                npc.DropItemInstanced(npc.position, npc.Size, ItemID.CorruptFishingCrate, 5);
                npc.DropItemInstanced(npc.position, npc.Size, ModContent.ItemType<CorruptHeart>());

                //to make up for no loot until dead
                Item.NewItem(npc.Hitbox, ItemID.ShadowScale, 60);
                Item.NewItem(npc.Hitbox, ItemID.DemoniteOre, 200);
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            base.OnHitPlayer(npc, target, damage, crit);

            target.AddBuff(BuffID.CursedInferno, 180);
            target.AddBuff(ModContent.BuffType<Rotting>(), 600);
        }

        public override void LoadSprites(NPC npc, bool recolor)
        {
            base.LoadSprites(npc, recolor);

            LoadNPCSprite(recolor, npc.type);
        }
    }

    public class EaterofWorldsHead : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.EaterofWorldsHead);

        public int FlamethrowerCDOrUTurnStoredTargetX;
        public int UTurnTotalSpacingDistance;
        public int UTurnIndividualSpacingPosition;
        public static int UTurnAITimer;
        public static int CursedFlameTimer;

        public bool UTurn;

        public bool DroppedSummon;

        public int NoSelfDestructTimer = 15;

        public override Dictionary<Ref<object>, CompoundStrategy> GetNetInfo() =>
            new Dictionary<Ref<object>, CompoundStrategy> {
                { new Ref<object>(FlamethrowerCDOrUTurnStoredTargetX), IntStrategies.CompoundStrategy },
                { new Ref<object>(UTurnTotalSpacingDistance), IntStrategies.CompoundStrategy },
                { new Ref<object>(UTurnIndividualSpacingPosition), IntStrategies.CompoundStrategy },
                { new Ref<object>(UTurnAITimer), IntStrategies.CompoundStrategy },
                { new Ref<object>(CursedFlameTimer), IntStrategies.CompoundStrategy },

                { new Ref<object>(UTurn), BoolStrategies.CompoundStrategy },
            };

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            npc.defense += 10;
            npc.damage = (int)(npc.damage * 4.0 / 3.0);
        }

        public override bool PreAI(NPC npc)
        {
            EModeGlobalNPC.eaterBoss = npc.whoAmI;
            EModeGlobalNPC.boss = npc.whoAmI;

            if (FargoSoulsWorld.SwarmActive)
                return true;

            if (!npc.HasValidTarget || npc.Distance(Main.player[npc.target].Center) > 3000)
            {
                npc.velocity.Y += 0.25f;
                if (npc.timeLeft > 120)
                    npc.timeLeft = 120;
            }

            //if (eaterResist > 0 && npc.whoAmI == NPC.FindFirstNPC(npc.type)) eaterResist--;

            int firstEater = NPC.FindFirstNPC(npc.type);

            if (npc.whoAmI == firstEater)
                UTurnAITimer++;

            if (Main.netMode != NetmodeID.MultiplayerClient && npc.whoAmI == firstEater && ++CursedFlameTimer > 300) //only let one eater increment this
            {
                bool shoot = true;
                if (!FargoSoulsWorld.MasochistModeReal)
                {
                    for (int i = 0; i < Main.maxNPCs; i++) //cancel if anyone is doing the u-turn
                    {
                        if (Main.npc[i].active && Main.npc[i].type == npc.type && Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().UTurn)
                        {
                            shoot = false;
                            CursedFlameTimer -= 30;
                        }
                    }
                }

                if (shoot)
                {
                    CursedFlameTimer = 0;

                    int counter = 0;
                    int delay = 0;
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        if (Main.npc[i].active)
                        {
                            /*if (Main.npc[i].type == npc.type && !Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().masobool0)
                            {
                                Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().counter2 = 0; //stop others from triggering it
                            }
                            else */
                            if (Main.npc[i].type == NPCID.EaterofWorldsHead || Main.npc[i].type == NPCID.EaterofWorldsBody || Main.npc[i].type == NPCID.EaterofWorldsTail)
                            {
                                if (++counter > (FargoSoulsWorld.MasochistModeReal ? 2 : 3)) //wave of redirecting flames
                                {
                                    counter = 0;
                                    Vector2 vel = (Main.player[npc.target].Center - Main.npc[i].Center) / 45;
                                    Projectile.NewProjectile(Main.npc[i].Center, vel,
                                        ModContent.ProjectileType<CursedFireballHoming>(), npc.damage / 5, 0f, Main.myPlayer, npc.target, delay);
                                    delay += 4;
                                }
                            }
                        }
                    }
                }
            }

            if (NoSelfDestructTimer <= 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && UTurnAITimer % 6 == 3) //chose this number at random to avoid edge case
                {
                    //die if segment behind me is invalid
                    int ai0 = (int)npc.ai[0];
                    if (!(ai0 > -1 && ai0 < Main.maxNPCs && Main.npc[ai0].active && Main.npc[ai0].ai[1] == npc.whoAmI
                        && (Main.npc[ai0].type == NPCID.EaterofWorldsBody || Main.npc[ai0].type == NPCID.EaterofWorldsTail)))
                    {
                        //Main.NewText("ai0 npc invalid");
                        npc.life = 0;
                        npc.HitEffect();
                        npc.checkDead();
                        npc.active = false;
                        npc.netUpdate = false;
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
                        return false;
                    }
                }
            }
            else
            {
                NoSelfDestructTimer--;
            }

            if (!UTurn)
            {
                if (++FlamethrowerCDOrUTurnStoredTargetX >= 6)
                {
                    FlamethrowerCDOrUTurnStoredTargetX = 0;
                    if (Main.netMode != NetmodeID.MultiplayerClient) //cursed flamethrower, roughly same direction as head
                    {
                        Vector2 velocity = new Vector2(5f, 0f).RotatedBy(npc.rotation - Math.PI / 2.0 + MathHelper.ToRadians(Main.rand.Next(-15, 16)));
                        Projectile.NewProjectile(npc.Center, velocity, ProjectileID.EyeFire, npc.damage / 5, 0f, Main.myPlayer);
                    }
                }

                if (npc.whoAmI == firstEater)
                {
                    if (UTurnAITimer == 700 - 90) //roar telegraph
                        SoundEngine.PlaySound(SoundID.Roar, Main.player[npc.target].Center, 0);

                    if (UTurnAITimer > 700 && Main.netMode != NetmodeID.MultiplayerClient) //initiate mass u-turn
                    {
                        UTurnAITimer = 0;
                        if (npc.HasValidTarget && npc.Distance(Main.player[npc.target].Center) < 2400)
                        {
                            UTurn = true;

                            UTurnTotalSpacingDistance = NPC.CountNPCS(npc.type) / 2;

                            int headCounter = 0; //determine position of this head in the group
                            for (int i = 0; i < Main.maxNPCs; i++) //synchronize
                            {
                                if (Main.npc[i].active && Main.npc[i].type == npc.type)
                                {
                                    Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().UTurnTotalSpacingDistance = UTurnTotalSpacingDistance;
                                    Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().UTurnIndividualSpacingPosition = headCounter;
                                    Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().UTurn = true;

                                    Main.npc[i].netUpdate = true;
                                    NetSync(Main.npc[i]);

                                    headCounter *= -1; //alternate 0, 1, -1, 2, -2, 3, -3, etc.
                                    if (headCounter >= 0)
                                        headCounter++;
                                }
                            }

                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else //flying u-turn ai
            {
                if (UTurnAITimer < 120)
                {
                    Vector2 target = Main.player[npc.target].Center;
                    if (UTurnTotalSpacingDistance != 0)
                        target.X += 900f / UTurnTotalSpacingDistance * UTurnIndividualSpacingPosition; //space out
                    target.Y += 600f;

                    float speedModifier = 0.6f;
                    if (npc.Center.X < target.X)
                    {
                        npc.velocity.X += speedModifier;
                        if (npc.velocity.X < 0)
                            npc.velocity.X += speedModifier * 2;
                    }
                    else
                    {
                        npc.velocity.X -= speedModifier;
                        if (npc.velocity.X > 0)
                            npc.velocity.X -= speedModifier * 2;
                    }
                    if (npc.Center.Y < target.Y)
                    {
                        npc.velocity.Y += speedModifier;
                        if (npc.velocity.Y < 0)
                            npc.velocity.Y += speedModifier * 2;
                    }
                    else
                    {
                        npc.velocity.Y -= speedModifier;
                        if (npc.velocity.Y > 0)
                            npc.velocity.Y -= speedModifier * 2;
                    }
                    if (Math.Abs(npc.velocity.X) > 24)
                        npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                    if (Math.Abs(npc.velocity.Y) > 24)
                        npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);

                    npc.localAI[0] = 1f;

                    if (Main.netMode == NetmodeID.Server && --npc.netSpam < 0) //manual mp sync control
                    {
                        npc.netSpam = 5;
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
                    }
                }
                else if (UTurnAITimer == 120) //fly up
                {
                    SoundEngine.PlaySound(SoundID.Roar, Main.player[npc.target].Center, 0);
                    npc.velocity = Vector2.UnitY * -15f;
                    FlamethrowerCDOrUTurnStoredTargetX = (int)Main.player[npc.target].Center.X; //store their initial location

                    npc.netUpdate = true;
                }
                else if (UTurnAITimer < 240) //cancel early and turn once we fly past player
                {
                    if (npc.Center.Y < Main.player[npc.target].Center.Y - (FargoSoulsWorld.MasochistModeReal ? 200 : 450))
                        UTurnAITimer = 239;
                }
                else if (UTurnAITimer == 240) //recalculate velocity to u-turn and dive back down in the same spacing over player
                {
                    Vector2 target;
                    target.X = Main.player[npc.target].Center.X;
                    if (UTurnTotalSpacingDistance != 0)
                        target.X += 900f / UTurnTotalSpacingDistance * UTurnIndividualSpacingPosition; //space out
                    target.Y = npc.Center.Y;

                    float radius = Math.Abs(target.X - npc.Center.X) / 2;
                    npc.velocity = Vector2.Normalize(npc.velocity) * (float)Math.PI * radius / 30;

                    FlamethrowerCDOrUTurnStoredTargetX = Math.Sign(Main.player[npc.target].Center.X - FlamethrowerCDOrUTurnStoredTargetX); //which side player moved to from original pos

                    npc.netUpdate = true;
                }
                else if (UTurnAITimer < 270) //u-turn
                {
                    npc.velocity = npc.velocity.RotatedBy(MathHelper.ToRadians(6f) * FlamethrowerCDOrUTurnStoredTargetX);
                }
                else if (UTurnAITimer == 270)
                {
                    npc.velocity = Vector2.Normalize(npc.velocity) * 15f;
                    npc.netUpdate = true;
                }
                else if (UTurnAITimer > 300)
                {
                    UTurnAITimer = 0;
                    UTurnTotalSpacingDistance = 0;
                    UTurnIndividualSpacingPosition = 0;
                    UTurn = false;

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        if (Main.npc[i].active)
                        {
                            if (Main.npc[i].type == npc.type)
                            {
                                Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().UTurnTotalSpacingDistance = 0;
                                Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().UTurnIndividualSpacingPosition = 0;
                                Main.npc[i].GetEModeNPCMod<EaterofWorldsHead>().UTurn = false;
                                Main.npc[i].netUpdate = true;
                                if (Main.netMode == NetmodeID.Server)
                                    NetSync(npc);
                            }
                            else if (Main.npc[i].type == NPCID.EaterofWorldsBody || Main.npc[i].type == NPCID.EaterofWorldsTail)
                            {
                                Main.npc[i].netUpdate = true;
                            }
                        }
                    }

                    npc.netUpdate = true;
                }

                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

                if (npc.netUpdate)
                {
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
                        NetSync(npc);
                    }
                    npc.netUpdate = false;
                }
                return false;
            }

            //drop summon
            if (!NPC.downedBoss2 && Main.netMode != NetmodeID.MultiplayerClient && npc.HasPlayerTarget && !DroppedSummon)
            {
                Player player = Main.player[npc.target];

                //eater meme
                if (!player.dead && player.GetModPlayer<FargoSoulsPlayer>().FreeEaterSummon)
                {
                    player.GetModPlayer<FargoSoulsPlayer>().FreeEaterSummon = false;

                    Item.NewItem(player.Hitbox, ModContent.ItemType<WormyFood>());
                    DroppedSummon = true;
                }
            }

            return true;
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage /= 3;
            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
        }

        public override void LoadSprites(NPC npc, bool recolor)
        {
            base.LoadSprites(npc, recolor);

            LoadBossHeadSprite(recolor, 2);
            LoadGoreRange(recolor, 24, 29);
        }
    }

    public class EaterofWorldsSegment : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail);

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            npc.damage *= 2;
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            NPC head = FargoSoulsUtil.NPCExists(npc.ai[1], NPCID.EaterofWorldsHead);
            if (head != null) //segment directly behind head takes less damage too
                damage /= 3;

            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
        }

        public override bool CheckDead(NPC npc)
        {
            //no loot unless every other segment is dead (doesn't apply during swarms - if swarm, die and drop loot normally)
            if (!FargoSoulsWorld.SwarmActive && Main.npc.Any(n => n.active && n.whoAmI != npc.whoAmI && (n.type == NPCID.EaterofWorldsBody || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsTail)))
            {
                npc.active = false;
                SoundEngine.PlaySound(npc.DeathSound, npc.Center);
                return false;
            }

            return base.CheckDead(npc);
        }
    }
}
