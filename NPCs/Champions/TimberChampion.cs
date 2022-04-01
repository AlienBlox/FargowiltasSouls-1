using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using FargowiltasSouls.Projectiles.Champions;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Buffs.Masomode;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using FargowiltasSouls.ItemDropRules.Conditions;
using FargowiltasSouls.Items.Accessories.Enchantments;

namespace FargowiltasSouls.NPCs.Champions
{
    [AutoloadBossHead]
    public class TimberChampion : ModNPC
    {
        private const float BaseWalkSpeed = 4f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Champion of Timber");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "木英灵");
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.DebuffImmunitySets.Add(NPC.type, new Terraria.DataStructures.NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Chilled,
                    BuffID.OnFire,
                    BuffID.Suffocation,
                    ModContent.BuffType<Lethargic>(),
                    ModContent.BuffType<ClippedWings>()
                }
            });
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary.TimberChampion")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 234;
            NPC.damage = 130;
            NPC.defense = 50;
            NPC.lifeMax = 160000;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            //NPC.value = Item.buyPrice(0, 15);
            NPC.boss = true;

            Music = ModLoader.TryGetMod("FargowiltasMusic", out Mod musicMod)
                ? MusicID.Boss1 : MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Champions");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            //NPC.damage = (int)(NPC.damage * 0.5f);
            NPC.lifeMax = (int)(NPC.lifeMax * Math.Sqrt(bossLifeScale));
        }

        public override bool CanHitPlayer(Player target, ref int CooldownSlot)
        {
            CooldownSlot = 1;
            return true;
        }

        public override void AI()
        {
            if (NPC.localAI[2] == 0)
            {
                NPC.TargetClosest(false);
                NPC.localAI[2] = 1;
            }

            EModeGlobalNPC.championBoss = NPC.whoAmI;

            Player player = Main.player[NPC.target];
            NPC.direction = NPC.spriteDirection = NPC.position.X < player.position.X ? 1 : -1;
            
            switch ((int)NPC.ai[0])
            {
                case -1: //mourning wood movement
                    {
                        NPC.noTileCollide = true;
                        NPC.noGravity = true;

                        if (Math.Abs(player.Center.X - NPC.Center.X) < NPC.width / 2)
                        {
                            NPC.velocity.X *= 0.9f;
                            if (Math.Abs(NPC.velocity.X) < 0.1f)
                                NPC.velocity.X = 0f;
                        }
                        else
                        {
                            float maxwalkSpeed = BaseWalkSpeed;// * 0.75f;
                            if (NPC.direction > 0)
                                NPC.velocity.X = (NPC.velocity.X * 20 + maxwalkSpeed) / 21;
                            else
                                NPC.velocity.X = (NPC.velocity.X * 20 - maxwalkSpeed) / 21;
                        }

                        bool onPlatforms = false;
                        for (int i = (int)NPC.position.X; i <= NPC.position.X + NPC.width; i += 16)
                        {
                            if (Framing.GetTileSafely(new Vector2(i, NPC.Bottom.Y + 8)).TileType == TileID.Platforms)
                            {
                                onPlatforms = true;
                                break;
                            }
                        }

                        bool onCollision = Collision.SolidCollision(NPC.position, NPC.width, NPC.height);

                        if (NPC.position.X < player.position.X && NPC.position.X + NPC.width > player.position.X + player.width
                            && NPC.position.Y + NPC.height < player.position.Y + player.height - 16)
                        {
                            NPC.velocity.Y += 0.5f;
                        }
                        else if (onCollision || (onPlatforms && player.position.Y + player.height <= NPC.position.Y + NPC.height))
                        {
                            if (NPC.velocity.Y > 0f)
                                NPC.velocity.Y = 0f;

                            if (onCollision)
                            {
                                if (NPC.velocity.Y > -0.2f)
                                    NPC.velocity.Y -= 0.025f;
                                else
                                    NPC.velocity.Y -= 0.2f;

                                if (NPC.velocity.Y < -4f)
                                    NPC.velocity.Y = -4f;
                            }
                        }
                        else
                        {
                            if (NPC.velocity.Y < 0f)
                                NPC.velocity.Y = 0f;

                            if (NPC.velocity.Y < 0.1f)
                                NPC.velocity.Y += 0.025f;
                            else
                                NPC.velocity.Y += 0.5f;
                        }

                        if (NPC.velocity.Y > 10f)
                            NPC.velocity.Y = 10f;
                    }
                    break;

                case 0: //jump at player
                    NPC.noTileCollide = false;
                    NPC.noGravity = false;

                    if (++NPC.ai[1] == 60)
                    {
                        NPC.TargetClosest();

                        if (NPC.localAI[0] == 0 && NPC.life < NPC.lifeMax * .66f) //spawn palm tree supports
                        {
                            NPC.localAI[0] = 1;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<TimberPalmTree>(), NPC.damage / 4, 0f, Main.myPlayer, NPC.whoAmI);
                            }
                        }

                        if (NPC.localAI[1] == 0 && NPC.life < NPC.lifeMax * .33f) //spawn palm tree supports
                        {
                            NPC.localAI[1] = 1;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<TimberPalmTree>(), NPC.damage / 4, 0f, Main.myPlayer, NPC.whoAmI);
                            }
                        }

                        const float gravity = 0.4f;
                        const float time = 90f;
                        
                        Vector2 distance = player.Top - NPC.Bottom;

                        distance.X = distance.X / time;
                        distance.Y = distance.Y / time - 0.5f * gravity * time;
                        NPC.velocity = distance;

                        NPC.noTileCollide = true;
                        NPC.noGravity = true;
                        NPC.netUpdate = true;

                        if (Main.netMode != NetmodeID.MultiplayerClient) //explosive jump
                        {
                            Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center, Vector2.Zero, ProjectileID.DD2OgreSmash, NPC.damage / 4, 0, Main.myPlayer);
                        }

                        Terraria.Audio.SoundEngine.PlaySound(SoundID.Item, (int)NPC.position.X, (int)NPC.position.Y, 14);

                        for (int k = -2; k <= 2; k++) //explosions
                        {
                            Vector2 dustPos = NPC.Center;
                            int width = NPC.width / 5;
                            dustPos.X += width * k + Main.rand.NextFloat(-width, width);
                            dustPos.Y += Main.rand.NextFloat(NPC.height / 2);

                            for (int i = 0; i < 30; i++)
                            {
                                int dust = Dust.NewDust(dustPos, 32, 32, 31, 0f, 0f, 100, default(Color), 3f);
                                Main.dust[dust].velocity *= 1.4f;
                            }

                            for (int i = 0; i < 20; i++)
                            {
                                int dust = Dust.NewDust(dustPos, 32, 32, 6, 0f, 0f, 100, default(Color), 3.5f);
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].velocity *= 7f;
                                dust = Dust.NewDust(dustPos, 32, 32, 6, 0f, 0f, 100, default(Color), 1.5f);
                                Main.dust[dust].velocity *= 3f;
                            }

                            float scaleFactor9 = 0.5f;
                            for (int j = 0; j < 4; j++)
                            {
                                int gore = Gore.NewGore(dustPos, default(Vector2), Main.rand.Next(61, 64));
                                Main.gore[gore].velocity *= scaleFactor9;
                                Main.gore[gore].velocity.X += 1f;
                                Main.gore[gore].velocity.Y += 1f;
                            }
                        }
                    }
                    else if (NPC.ai[1] > 60)
                    {
                        NPC.noTileCollide = true;
                        NPC.noGravity = true;
                        NPC.velocity.Y += 0.4f;

                        if (NPC.ai[1] > 60 + 90)
                        {
                            NPC.TargetClosest();
                            NPC.ai[0]++;
                            NPC.ai[1] = 0;
                            NPC.netUpdate = true;
                        }
                    }
                    else //less than 60
                    {
                        if (Math.Abs(NPC.velocity.X) > Math.Abs(NPC.velocity.Y))
                            NPC.velocity.X = Math.Abs(NPC.velocity.Y) * Math.Sign(NPC.velocity.X);
                        if (NPC.velocity.Y == 0)
                            NPC.velocity.X *= 0.99f;

                        if (NPC.ai[0] != 0 && (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 2500f))
                        {
                            NPC.TargetClosest();
                            if (NPC.timeLeft > 30)
                                NPC.timeLeft = 30;

                            NPC.noTileCollide = true;
                            NPC.noGravity = true;
                            NPC.velocity.Y += 1f;

                            NPC.ai[1] = 0; //prevent proceeding to next steps of ai while despawning
                            return;
                        }
                        else
                        {
                            NPC.timeLeft = 600;
                        }

                        goto case -1;
                    }
                    break;

                case 1: //acorn sprays
                    if (++NPC.ai[2] > 35)
                    {
                        NPC.ai[2] = 0;
                        const float gravity = 0.2f;
                        float time = 60f;
                        Vector2 distance = player.Center - NPC.Center;// + player.velocity * 30f;
                        distance.X = distance.X / time;
                        distance.Y = distance.Y / time - 0.5f * gravity * time;
                        for (int i = 0; i < 20; i++)
                        {
                            Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center, distance + Main.rand.NextVector2Square(-0.5f, 0.5f) * 3,
                                ModContent.ProjectileType<TimberAcorn>(), NPC.damage / 5, 0f, Main.myPlayer);
                        }
                    }
                    
                    if (++NPC.ai[1] > 120)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                        NPC.TargetClosest();
                    }
                    goto case -1;

                case 2:
                    goto case 0;

                case 3: //snowball barrage
                    if (++NPC.ai[2] > 5)
                    {
                        NPC.ai[2] = 0;
                        if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] > 30 && NPC.ai[1] < 120)
                        {
                            Vector2 offset;
                            offset.X = Main.rand.NextFloat(0, NPC.width / 2) * NPC.direction;
                            offset.Y = 16;
                            Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center + offset,
                                Vector2.UnitY * -12f, ModContent.ProjectileType<TimberSnowball>(), NPC.damage / 4, 0f, Main.myPlayer);
                        }
                    }

                    if (++NPC.ai[1] > 150)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                        NPC.TargetClosest();
                    }
                    goto case -1;

                case 4:
                    goto case 0;

                case 5: //spray squirrels
                    if (++NPC.ai[2] > 6)
                    {
                        NPC.ai[2] = 0;
                        FargoSoulsUtil.NewNPCEasy(NPC.GetSpawnSourceForNPCFromNPCAI(), NPC.Center, ModContent.NPCType<LesserSquirrel>(),
                            velocity: new Vector2(Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-20, -10)));
                    }

                    if (++NPC.ai[1] > 180)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                        NPC.TargetClosest();
                    }
                    goto case -1;

                case 6:
                    goto case 0;

                case 7:
                    goto case 3;

                case 8:
                    goto case 0;

                case 9: //grappling hook
                    if (NPC.ai[2] == 0) //shoot hook
                    {
                        NPC.ai[2] = 1;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center, NPC.DirectionTo(player.Center) * 16, ModContent.ProjectileType<TimberHook>(), 0, 0f, Main.myPlayer, NPC.whoAmI);
                        }
                    }

                    if (++NPC.ai[1] < 240) //charge power
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default(Color), 2f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].velocity *= 8f;
                            d = Dust.NewDust(NPC.Center, 0, 0, DustID.Shadowflame, 0f, 0f, 0, default(Color), 3f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].velocity *= 16f;
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            Vector2 offset = new Vector2();
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            offset.X += (float)(Math.Sin(angle) * 500);
                            offset.Y += (float)(Math.Cos(angle) * 500);
                            Dust dust = Main.dust[Dust.NewDust(NPC.Center + offset - new Vector2(4, 4), 0, 0, DustID.Shadowflame, 0, 0, 100, Color.White, 2f)];
                            dust.velocity = NPC.velocity;
                            if (Main.rand.NextBool(3))
                                dust.velocity += Vector2.Normalize(offset) * -5f;
                            dust.noGravity = true;
                        }
                    }
                    else if (NPC.ai[1] == 240) //explode
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<TimberShadowflameBlast>(), NPC.damage / 2, 0f, Main.myPlayer);
                        }
                    }
                    else if (NPC.ai[1] > 300)
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true;
                        NPC.TargetClosest();
                    }
                    goto case -1;

                default:
                    NPC.ai[0] = 0;
                    goto case 0;
            }
        }

        private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f)
        {
            if (NPC.Center.X < targetPos.X)
            {
                NPC.velocity.X += speedModifier;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X += speedModifier * 2;
            }
            else
            {
                NPC.velocity.X -= speedModifier;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X -= speedModifier * 2;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.X) > cap)
                NPC.velocity.X = cap * Math.Sign(NPC.velocity.X);
            if (Math.Abs(NPC.velocity.Y) > cap)
                NPC.velocity.Y = cap * Math.Sign(NPC.velocity.Y);
        }

        public override void FindFrame(int frameHeight)
        {
            switch ((int)NPC.ai[0])
            {
                case 0:
                case 2:
                case 4:
                case 6:
                case 8:
                    if (NPC.ai[1] <= 60)
                        NPC.frame.Y = frameHeight * 6; //crouching for jump
                    else
                        NPC.frame.Y = frameHeight * 7; //jumping
                    break;

                default:
                    {
                        NPC.frameCounter += 1f / BaseWalkSpeed * Math.Abs(NPC.velocity.X);

                        if (NPC.frameCounter > 2.5f) //walking animation
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y += frameHeight;
                        }

                        if (NPC.frame.Y >= frameHeight * 6)
                            NPC.frame.Y = 0;

                        if (NPC.velocity.X == 0)
                            NPC.frame.Y = frameHeight; //stationary sprite if standing still

                        if (NPC.velocity.Y > 4)
                            NPC.frame.Y = frameHeight * 7; //jumping
                    }
                    break;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (FargoSoulsWorld.EternityMode)
                target.AddBuff(ModContent.BuffType<Buffs.Masomode.Guilty>(), 600);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                for (int i = 3; i <= 10; i++)
                {
                    Vector2 pos = NPC.position + new Vector2(Main.rand.NextFloat(NPC.width), Main.rand.NextFloat(NPC.height));
                    Gore.NewGore(pos, NPC.velocity, ModContent.Find<ModGore>(Mod.Name, $"TimberGore{i}").Type, NPC.scale);
                }

                if (Main.netMode != NetmodeID.MultiplayerClient && FargoSoulsWorld.EternityMode)
                {
                    FargoSoulsUtil.NewNPCEasy(NPC.GetSpawnSourceForNPCFromNPCAI(), NPC.Center, ModContent.NPCType<TimberChampionHead>(), NPC.whoAmI, target: NPC.target);
                }
            }
        }

        public override bool PreKill()
        {
            return !FargoSoulsWorld.EternityMode;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref FargoSoulsWorld.downedChampions[(int)FargoSoulsWorld.Downed.TimberChampion], -1);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //npcLoot.Add(new ChampionEnchDropRule(ModContent.ItemType<Items.Accessories.Forces.TimberForce>()));
            npcLoot.Add(new ChampionEnchDropRule(new int[] { 
                ModContent.ItemType<WoodEnchant>(),
                ModContent.ItemType<BorealWoodEnchant>(),
                ModContent.ItemType<RichMahoganyEnchant>(),
                ModContent.ItemType<EbonwoodEnchant>(),
                ModContent.ItemType<ShadewoodEnchant>(),
                ModContent.ItemType<PalmWoodEnchant>(),
                ModContent.ItemType<PearlwoodEnchant>()
            }));
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = NPC.direction < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }
    }
}
