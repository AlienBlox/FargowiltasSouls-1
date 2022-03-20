using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using FargowiltasSouls.NPCs;
//using FargowiltasSouls.Projectiles;
using FargowiltasSouls.Buffs.Masomode;
//using FargowiltasSouls.Projectiles.BossWeapons;
using FargowiltasSouls.Projectiles.Masomode;
//using FargowiltasSouls.Projectiles.Minions;
//using FargowiltasSouls.Items.Weapons.SwarmDrops;
//using FargowiltasSouls.NPCs.MutantBoss;

//using FargowiltasSouls.Items.Summons;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria.Graphics.Shaders;
using FargowiltasSouls.Toggler;
using FargowiltasSouls.Items.Accessories.Enchantments;
using FargowiltasSouls.Buffs.Souls;
using FargowiltasSouls.Projectiles.Souls;
using FargowiltasSouls.NPCs.EternityMode;
using FargowiltasSouls.Projectiles;
using FargowiltasSouls.Projectiles.Minions;
using FargowiltasSouls.Items.Accessories.Masomode;
using FargowiltasSouls.Projectiles.BossWeapons;
//using FargowiltasSouls.Items.Accessories.Souls;

namespace FargowiltasSouls
{
    public partial class FargoSoulsPlayer : ModPlayer
    {
        public ToggleBackend Toggler = new ToggleBackend();
        public Dictionary<string, bool> TogglesToSync = new Dictionary<string, bool>();

        public bool IsStandingStill;
        public float AttackSpeed;
        public float WingTimeModifier;

        public bool FreeEaterSummon = true;
        public int Screenshake;

        //        public bool Wood;
        public Item QueenStingerItem;
        public int QueenStingerCD;
        //        public bool EridanusEmpower;
        //        public int EridanusTimer;
        //        public bool GaiaSet;
        //        public bool GaiaOffense;
        public bool StyxSet;
        public int StyxMeter;

        //        //minions
        public bool BrainMinion;
        public bool EaterMinion;
        //        public bool BigBrainMinion;
        //        public bool DukeFishron;

        //        //mount
        //        public bool SquirrelMount;

        //        //pet
        //        public bool ChibiDevi;
        //        public bool MutantSpawn;
        //        public bool BabyAbom;

        #region enchantments
        public bool AdamantiteEnchantActive;
        public int AdamantiteCD = 0;
        public bool AncientCobaltEnchantActive;
        public bool AncientHallowEnchantActive;
        public bool AncientShadowEnchantActive;
        public bool ApprenticeEnchantActive;
        public int ApprenticeCD = 0;
        public bool BeeEnchantActive;
        public int BeeCD = 0;
        public bool BorealEnchantActive;
        public bool CactusEnchantActive;
        public int CactusProcCD = 0;
        public bool ChloroEnchantActive;
        public bool CobaltEnchantActive;
        public int CobaltCD = 0;
        public bool CopperEnchantActive;
        public int CopperProcCD = 0;
        public bool CrimsonEnchantActive;
        public bool CrimsonRegen;
        public int CrimsonTotalToRegen = 0;
        public int CrimsonRegenSoFar = 0;
        public int CrimsonRegenTimer = 0;
        public bool DarkArtistEnchantActive;
        public bool DarkArtistSpawn;
        public int DarkArtistSpawnCD = 0;
        public bool ForbiddenEnchantActive;
        public bool FossilEnchantActive;
        public bool FrostEnchantActive;
        public int IcicleCount = 0;
        private int icicleCD = 0;
        public bool GladiatorEnchantActive;
        public int GladiatorCD = 0;
        public bool GoldEnchantActive;
        public bool GoldShell;
        private int goldHP;
        public bool HallowEnchantActive;
        public bool HuntressEnchantActive;
        private int huntressCD = 0;
        public bool IronEnchantActive;
        public bool IronGuard;
        public int IronDebuffImmuneTime;
        public bool JungleEnchantActive;
        public int JungleCD;
        public bool LeadEnchantActive;
        public bool MahoganyEnchantActive;
        public bool MeteorEnchantActive;
        private int meteorTimer = 150;
        private int meteorCD = 0;
        public bool meteorShower;
        public bool MoltenEnchantActive;
        public bool MonkEnchantActive;
        public int MonkDashing = 0;
        private int monkTimer;
        public bool MythrilEnchantActive;
        public bool NecroEnchantActive;
        public int NecroCD;
        public bool NinjaEnchantActive;
        public Projectile NinjaSmokeBombProj = null;
        public bool FirstStrike;
        public int SmokeBombCD;
        public bool ObsidianEnchantActive;
        private int obsidianCD = 0;
        public bool LavaWet;
        public bool OriEnchantActive;
        public bool PalladEnchantActive;
        public int PalladCounter;
        //private int palladiumCD = 0;
        public bool PalmEnchantActive;
        public bool PearlwoodEnchantActive;
        public int PearlwoodCD = 0;
        public int PumpkinSpawnCD = 0;
        public bool RainEnchantActive;
        public bool RedEnchantActive;
        public bool ShadewoodEnchantActive;
        public int ShadewoodCD = 0;
        public bool ShadowEnchantActive;
        public bool ShinobiEnchantActive;
        public int dashCD;
        public bool ShroomEnchantActive;
        public bool SilverEnchantActive;
        public bool SnowEnchantActive;
        public bool SnowVisual;
        public bool SolarEnchantActive;
        public bool SpectreEnchantActive;
        public bool SpiderEnchantActive;
        public int SummonCrit;
        public bool SpookyEnchantActive;
        public bool SquireEnchantActive;
        public bool squireReduceIframes;
        public bool StardustEnchantActive;
        public bool FreezeTime = false;
        public int freezeLength = 540; //300;
        public bool TikiEnchantActive;
        public bool TikiMinion;
        public int actualMinions;
        public bool TikiSentry;
        public int actualSentries;
        public bool TinEnchantActive;
        public int TinCritMax = 0;
        public int TinCrit = 5;
        public int TinProcCD = 0;
        public bool TungstenEnchantActive;
        public float TungstenPrevSizeSave = -1;
        public int TungstenCD = 0;
        public bool TurtleEnchantActive;
        public int TurtleCounter = 0;
        public int TurtleShellHP = 25;
        private int turtleRecoverCD = 240;
        public bool ShellHide;
        public bool ValhallaEnchantActive;
        public bool VortexEnchantActive;
        public bool VortexStealth = false;
        public bool WizardEnchantActive;
        public bool WoodEnchantActive;

        //public bool ElementEnchant; //WTF is THIS
        public int CritterAttackTimer = 0;
        //public bool Solar;
        //public bool Nebula;

        public bool CosmoForce;
        public bool EarthForce;
        public bool LifeForce;
        public bool NatureForce;
        public bool SpiritForce;
        public bool ShadowForce;
        public bool TerraForce;
        public bool WillForce;
        public bool WoodForce;

        #endregion

        //        //soul effects
        public bool MagicSoul;
        public bool RangedSoul;
        public bool RangedEssence;
        //        public bool BuilderMode;
        //        public bool UniverseEffect;
        public bool FishSoul1;
        //        public bool FishSoul2;
        public bool TerrariaSoul;
        //        public bool VoidSoul;
        //        public int HealTimer;
        //        public int HurtTimer;
        public bool Eternity;
        public float eternityDamage = 0;

        //maso items
        public Item SlimyShieldItem;
        public bool SlimyShieldFalling;
        public Item AgitatingLensItem;
        public int AgitatingLensCD;
        public Item CorruptHeartItem;
        public int CorruptHeartCD;
        public bool GuttedHeart;
        public int GuttedHeartCD = 60; //should prevent spawning despite disabled toggle when loading into world
        //        public bool NecromanticBrew;
        public bool PureHeart;
        //        public bool PungentEyeballMinion;
        public bool CrystalSkullMinion;
        public bool FusedLens;
        //        public bool GroundStick;
        //        public bool Probes;
        //        public bool MagicalBulb;
        public bool SkullCharm;
        public Item PumpkingsCapeItem;
        public Item LihzahrdTreasureBoxItem;
        //        public int GroundPound;
        public Item BetsysHeartItem;
        public bool BetsyDashing;
        //        public int BetsyDashCD = 0;
        public bool MutantAntibodies;
        //        public bool GravityGlobeEX;
        //        public bool CelestialRune;
        public bool AdditionalAttacks;
        public int AdditionalAttacksTimer;
        //        public bool MoonChalice;
        //        public bool LunarCultist;
        //        public bool TrueEyes;
        public Item AbomWandItem;
        public int AbomWandCD;
        public bool MasochistSoul;
        public bool MasochistHeart;
        //        public bool CelestialSeal;
        public bool SandsofTime;
        public bool DragonFang;
        public bool SecurityWallet;
        public Item FrigidGemstoneItem;
        public Item WretchedPouchItem;
        public int WretchedPouchCD;
        public int FrigidGemstoneCD;
        public bool NymphsPerfume;
        public bool NymphsPerfumeRespawn;
        public int NymphsPerfumeCD = 30;
        public bool SqueakyAcc;
        public bool RainbowSlime;
        //        public bool SkeletronArms;
        public bool SuperFlocko;
        public bool IceQueensCrown;
        public bool MiniSaucer;
        public bool TribalCharm;
        public bool TribalAutoFire;
        public bool SupremeDeathbringerFairy;
        //        public bool GodEaterImbue;
        //        public bool MutantSetBonus;
        //        public bool Abominationn;
        //        public bool PhantasmalRing;
        public bool MutantsDiscountCard;
        public bool MutantsPact;
        public bool RabiesVaccine;
        //        public bool TwinsEX;
        public bool TimsConcoction;
        public bool ReceivedMasoGift;
        public bool Graze;
        public float GrazeRadius;
        public int GrazeCounter;
        public double GrazeBonus;
        public Item DevianttHeartItem;
        public int DevianttHeartsCD;
        public Item MutantEyeItem;
        public bool MutantEyeVisual;
        public int MutantEyeCD;
        public bool AbominableWandRevived;
        public bool AbomRebirth;
        public bool WasHurtBySomething;

        //        //public int PreNerfDamage;

        //debuffs
        public bool Hexed;
        public bool Unstable;
        private int unstableCD = 0;
        public bool Fused;
        public bool Shadowflame;
        public bool Oiled;
        public bool DeathMarked;
        public bool Hypothermia;
        public bool noDodge;
        public bool noSupersonic;
        public bool Bloodthirsty;
        public bool DisruptedFocus;
        public bool SinisterIcon;
        public bool SinisterIconDrops;

        public bool GodEater;               //defense removed, endurance removed, colossal DOT
        public bool FlamesoftheUniverse;    //activates various vanilla debuffs
        public bool MutantNibble;           //disables potions, moon bite effect, feral bite effect, disables lifesteal
        public int StatLifePrevious = -1;   //used for mutantNibble
        public bool Asocial;                //disables minions, disables pets
        public bool WasAsocial;
        public bool HidePetToggle0 = true;
        public bool HidePetToggle1 = true;
        public bool Kneecapped;             //disables running :v
        public bool Defenseless;            //-30 defense, no damage reduction, cross necklace and knockback prevention effects disabled
        public bool Purified;               //purges all other buffs
        public bool Infested;               //weak DOT that grows exponentially stronger
        public int MaxInfestTime;
        public bool FirstInfection = true;
        public float InfestedDust;
        public bool Rotting;                //inflicts DOT and almost every stat reduced
        public bool SqueakyToy;             //all attacks do one damage and make squeaky noises
        public bool Atrophied;              //melee speed and damage reduced. maybe Player cannot fire melee projectiles?
        public bool Jammed;                 //ranged damage and speed reduced, all non-custom ammo set to baseline ammos
        public bool Slimed;
        public byte lightningRodTimer;
        public bool ReverseManaFlow;
        public bool CurseoftheMoon;
        public bool OceanicMaul;
        public int MaxLifeReduction;
        public bool Midas;
        public bool MutantPresence;
        public bool DevianttPresence;
        public bool Swarming;
        public bool LowGround;
        public bool Flipped;
        public bool Mash;
        public bool[] MashPressed = new bool[4];
        public int MashCounter;
        public int StealingCooldown;
        public bool LihzahrdCurse;
        //public bool LihzahrdBlessing;
        public bool Berserked;
        public bool HolyPrice;
        public bool NanoInjection;

        public int ReallyAwfulDebuffCooldown;

        public int MasomodeCrystalTimer;
        public int MasomodeFreezeTimer;
        public int MasomodeSpaceBreathTimer;
        public int MasomodeWeaponUseTimer;
        public int MasomodeMinionNerfTimer;
        public const int MaxMasomodeMinionNerfTimer = 300;
        public bool ReduceMasomodeMinionNerf;

        public IList<string> disabledSouls = new List<string>();

        //        private Mod dbzMod = ModLoader.GetMod("DBZMOD");

        public bool DoubleTap
        {
            get
            {
                return Main.ReversedUpDownArmorSetBonuses ?
                    Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[1] > 0 && Player.doubleTapCardinalTimer[1] != 15
                    : Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[0] > 0 && Player.doubleTapCardinalTimer[0] != 15;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            //idk ech
            string name = "FargoDisabledSouls" + Player.name;
            var FargoDisabledSouls = new List<string>();

            //if (CelestialSeal) FargoDisabledSouls.Add("CelestialSeal");
            if (MutantsDiscountCard) FargoDisabledSouls.Add("MutantsDiscountCard");
            if (MutantsPact) FargoDisabledSouls.Add("MutantsPact");
            if (ReceivedMasoGift) FargoDisabledSouls.Add("ReceivedMasoGift");
            if (RabiesVaccine) FargoDisabledSouls.Add("RabiesVaccine");

            Toggler.Save();

            tag.Add(name, FargoDisabledSouls);
        }

        public override void LoadData(TagCompound tag)
        {
            string name = "FargoDisabledSouls" + Player.name;
            //string log = name + " loaded: ";

            disabledSouls = tag.GetList<string>(name);

            //CelestialSeal = disabledSouls.Contains("CelestialSeal");
            MutantsDiscountCard = disabledSouls.Contains("MutantsDiscountCard");
            MutantsPact = disabledSouls.Contains("MutantsPact");
            ReceivedMasoGift = disabledSouls.Contains("ReceivedMasoGift");
            RabiesVaccine = disabledSouls.Contains("RabiesVaccine");
        }

        public override void Initialize()
        {
            Toggler.Load(this);
        }

        public override void OnEnterWorld(Player Player)
        {
            disabledSouls.Clear();

            //if (ModLoader.GetMod("FargowiltasMusic") == null)
            //{
            //    Main.NewText("Fargo's Music Mod not found!", Color.LimeGreen);
            //    Main.NewText("Please install Fargo's Music Mod for the full experience!!", Color.LimeGreen);
            //}

            ///*for (int i = 0; i < 200; i++)
            //{
            //    if (Main.npc[i].type == NPCID.LunarTowerSolar
            //    || Main.npc[i].type == NPCID.LunarTowerVortex
            //    || Main.npc[i].type == NPCID.LunarTowerNebula
            //    || Main.npc[i].type == NPCID.LunarTowerStardust)
            //    {
            //        if (Main.netMode == NetmodeID.MultiPlayerClient)
            //        {
            //            var netMessage = mod.GetPacket();
            //            netMessage.Write((byte)1);
            //            netMessage.Write((byte)i);
            //            netMessage.Send();
            //            Main.npc[i].lifeMax *= 2;
            //        }
            //        else
            //        {
            //            Main.npc[i].GetGlobalNPC<FargoSoulsGlobalNPC>().SetDefaults(Main.npc[i]);
            //            Main.npc[i].life = Main.npc[i].lifeMax;
            //        }
            //    }
            //}*/
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Mash)
            {
                const int increment = 1;

                if (triggersSet.Up)
                {
                    if (!MashPressed[0])
                        MashCounter += increment;
                    MashPressed[0] = true;
                }
                else
                {
                    MashPressed[0] = false;
                }

                if (triggersSet.Left)
                {
                    if (!MashPressed[1])
                        MashCounter += increment;
                    MashPressed[1] = true;
                }
                else
                {
                    MashPressed[1] = false;
                }

                if (triggersSet.Right)
                {
                    if (!MashPressed[2])
                        MashCounter += increment;
                    MashPressed[2] = true;
                }
                else
                {
                    MashPressed[2] = false;
                }

                if (triggersSet.Down)
                {
                    if (!MashPressed[3])
                        MashCounter += increment;
                    MashPressed[3] = true;
                }
                else
                {
                    MashPressed[3] = false;
                }
            }

            //            if (Fargowiltas.GoldKey.JustPressed && GoldEnchant)
            //            {
            //                if (!Player.HasBuff(ModContent.BuffType<GoldenStasis>()) && !Player.HasBuff(ModContent.BuffType<GoldenStasisCD>()))
            //                {
            //                    int duration = 300;

            //                    if (WillForce)
            //                    {
            //                        duration *= 2;
            //                    }

            //                    Player.AddBuff(ModContent.BuffType<GoldenStasis>(), duration);
            //                    Player.AddBuff(ModContent.BuffType<GoldenStasisCD>(), 3600);

            //                    goldHP = Player.statLife;
            //                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(FargowiltasSouls.Instance, "Sounds/Zhonyas").WithVolume(1f), Player.Center);
            //                }
            //                //cancel it early
            //                else
            //                {
            //                    Player.ClearBuff(ModContent.BuffType<GoldenStasis>());
            //                }
            //            }

            //            if (GoldShell)
            //            {
            //                return;
            //            }

            //            if (Fargowiltas.FreezeKey.JustPressed && StardustEnchant && !Player.HasBuff(ModContent.BuffType<TimeStopCD>()))
            //            {
            //                int cooldownInSeconds = 60;
            //                if (CosmoForce)
            //                    cooldownInSeconds = 50;
            //                if (TerrariaSoul)
            //                    cooldownInSeconds = 40;
            //                if (Eternity)
            //                    cooldownInSeconds = 30;
            //                Player.AddBuff(ModContent.BuffType<TimeStopCD>(), cooldownInSeconds * 60);
            //                FreezeTime = true;
            //                freezeLength = 540;
            //                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(FargowiltasSouls.Instance, "Sounds/ZaWarudo").WithVolume(1f).WithPitchVariance(.5f), Player.Center);
            //            }



            if (FargowiltasSouls.SmokeBombKey.JustPressed && NinjaEnchantActive && SmokeBombCD == 0)
            {
                NinjaEnchant.SmokeBombKey(this);
            }

            //            if (Fargowiltas.BetsyDashKey.JustPressed && BetsysHeart && BetsyDashCD <= 0)
            //            {
            //                BetsyDashCD = 180;
            //                if (Player.whoAmI == Main.myPlayer)
            //                {
            //                    /*Player.controlLeft = false;
            //                    Player.controlRight = false;
            //                    Player.controlJump = false;
            //                    Player.controlDown = false;*/
            //                    Player.controlUseItem = false;
            //                    Player.controlUseTile = false;
            //                    Player.controlHook = false;
            //                    Player.controlMount = false;

            //                    Player.immune = true;
            //                    Player.immuneTime = Math.Max(Player.immuneTime, 2);
            //                    Player.hurtCooldowns[0] = Math.Max(Player.hurtCooldowns[0], 2);
            //                    Player.hurtCooldowns[1] = Math.Max(Player.hurtCooldowns[1], 2);

            //                    Player.itemAnimation = 0;
            //                    Player.itemTime = 0;

            //                    Vector2 vel = Player.DirectionTo(Main.MouseWorld) * (MasochistHeart ? 25 : 20);
            //                    Projectile.NewProjectile(Player.Center, vel, ModContent.ProjectileType<Projectiles.Masomode.BetsyDash>(), (int)(100 * Player.GetDamage(DamageClass.Melee)), 0f, Player.whoAmI);
            //                    Player.AddBuff(ModContent.BuffType<Buffs.Souls.BetsyDash>(), 20);

            //                    //immune to all debuffs
            //                    foreach (int debuff in Fargowiltas.DebuffIDs)
            //                    {
            //                        if (!Player.HasBuff(debuff))
            //                        {
            //                            Player.buffImmune[debuff] = true;
            //                        }
            //                    }
            //                }
            //            }

            if (FargowiltasSouls.MutantBombKey.JustPressed && MutantEyeItem != null && MutantEyeCD <= 0)
            {
                MutantEyeCD = 3600;

                if (!Main.dedServ && Main.LocalPlayer.active)
                    Main.LocalPlayer.GetModPlayer<FargoSoulsPlayer>().Screenshake = 30;

                const int invulTime = 90;
                Player.immune = true;
                Player.immuneTime = invulTime;
                Player.hurtCooldowns[0] = invulTime;
                Player.hurtCooldowns[1] = invulTime;

                SoundEngine.PlaySound(SoundID.Item84, Player.Center);

                const int max = 100; //make some indicator dusts
                for (int i = 0; i < max; i++)
                {
                    Vector2 vector6 = Vector2.UnitY * 30f;
                    vector6 = vector6.RotatedBy((i - (max / 2 - 1)) * 6.28318548f / max) + Main.LocalPlayer.Center;
                    Vector2 vector7 = vector6 - Main.LocalPlayer.Center;
                    int d = Dust.NewDust(vector6 + vector7, 0, 0, 229, 0f, 0f, 0, default(Color), 3f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity = vector7;
                }

                for (int i = 0; i < 50; i++) //make some indicator dusts
                {
                    int d = Dust.NewDust(Player.position, Player.width, Player.height, 229, 0f, 0f, 0, default(Color), 2.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 24f;
                }

                FargoSoulsUtil.ClearHostileProjectiles(1);

                void SpawnSphereRing(int ringMax, float speed, int damage2, float rotationModifier)
                {
                    float rotation = 2f * (float)Math.PI / ringMax;
                    Vector2 vel = Vector2.UnitY * speed;
                    int type = ModContent.ProjectileType<PhantasmalSphereRing>();
                    for (int i = 0; i < ringMax; i++)
                    {
                        vel = vel.RotatedBy(rotation);
                        Projectile.NewProjectile(Player.GetProjectileSource_Accessory(MutantEyeItem), Player.Center, vel, type, damage2, 3f, Main.myPlayer, rotationModifier * Player.direction, speed);
                    }
                }

                int damage = (int)(1700 * Player.GetDamage(DamageClass.Magic));
                SpawnSphereRing(24, 12f, damage, -1f);
                SpawnSphereRing(24, 12f, damage, 1f);
            }

            //            if (triggersSet.Left && Player.confused && Player.gravControl)
            //            {
            //                Player.gravDir *= -1;
            //            }

            if (FargowiltasSouls.SoulToggleKey.JustPressed)
            {
                FargowiltasSouls.UserInterfaceManager.ToggleSoulToggler();
            }
        }

        public override void ResetEffects()
        {
            //            if (CelestialSeal)
            //            {
            //                Player.extraAccessory = true;
            //                Player.extraAccessorySlots = 2;
            //            }

            //            SummonCrit = 0;

            AttackSpeed = 1f;
            if (Screenshake > 0)
                Screenshake--;

            //            Wood = false;

            //            wingTimeModifier = 1f;

            QueenStingerItem = null;
            //            EridanusEmpower = false;
            //            GaiaSet = false;
            StyxSet = false;

            BrainMinion = false;
            EaterMinion = false;
            //            BigBrainMinion = false;
            //            DukeFishron = false;

            //            SquirrelMount = false;

            //            ChibiDevi = false;
            //            MutantSpawn = false;
            //            BabyAbom = false;

            //            #region enchantments 
            //            PetsActive = true;
            //            ShadowEnchant = false;
            //            CrimsonEnchant = false;
            //            CrimsonRegen = false;
            //            SpectreEnchant = false;
            //            BeeEnchant = false;
            //            SpiderEnchant = false;
            StardustEnchantActive = false;
            //            MythrilEnchant = false;
            FossilEnchantActive = false;
            //            JungleEnchant = false;
            //            ElementEnchant = false;
            //            ShroomEnchant = false;
            //            CobaltEnchant = false;
            //            SpookyEnchant = false;
            //            HallowEnchant = false;
            AncientHallowEnchantActive = false;
            //            ChloroEnchant = false;
            //            VortexEnchant = false;
            AdamantiteEnchantActive = false;
            //            FrostEnchant = false;
            //            PalladEnchant = false;
            //            OriEnchant = false;
            //            MeteorEnchant = false;
            //            MoltenEnchant = false;
            CopperEnchantActive = false;
            NinjaEnchantActive = false;
            FirstStrike = false;
            //            NearSmoke = false;
            //            IronEnchant = false;
            //            IronGuard = false;
            //            TurtleEnchant = false;
            //            ShellHide = false;
            LeadEnchantActive = false;
            GladiatorEnchantActive = false;
            //            GoldEnchant = false;
            //            GoldShell = false;
            CactusEnchantActive = false;
            //            ForbiddenEnchant = false;
            //            SilverEnchant = false;
            //            PlatinumEnchant = false;
            NecroEnchantActive = false;
            //            ObsidianEnchant = false;
            //            LavaWet = false;
            TinEnchantActive = false;
            //            TikiEnchant = false;
            //            TikiMinion = false;
            //            TikiSentry = false;
            //            SolarEnchant = false;
            //            ShinobiEnchant = false;
            //            ValhallaEnchant = false;
            //            DarkEnchant = false;
            //            RedEnchant = false;
            TungstenEnchantActive = false;

            MahoganyEnchantActive = false;
            BorealEnchantActive = false;
            WoodEnchantActive = false;
            PalmEnchantActive = false;
            ShadewoodEnchantActive = false;
            PearlwoodEnchantActive = false;

            //            RainEnchant = false;
            //            AncientCobaltEnchant = false;
            //            AncientShadowEnchant = false;
            //            SquireEnchant = false;
            //            squireReduceIframes = false;
            //            ApprenticeEnchant = false;
            //            HuntressEnchant = false;
            //            MonkEnchant = false;
            //            SnowEnchant = false;
            //            SnowVisual = false;

            //            Solar = false;
            //            Nebula = false;

            //            CosmoForce = false;
            //            EarthForce = false;
            //            LifeForce = false;
            //            NatureForce = false;
            //            SpiritForce = false;
            //            TerraForce = false;
            //            ShadowForce = false;
            //            WillForce = false;
            //            WoodForce = false;

            //            #endregion

            //            //souls
            MagicSoul = false;
            RangedSoul = false;
            RangedEssence = false;
            //            BuilderMode = false;
            //            UniverseEffect = false;
            FishSoul1 = false;
            //            FishSoul2 = false;
            //            TerrariaSoul = false;
            //            VoidSoul = false;
            Eternity = false;

            //maso
            SlimyShieldItem = null;
            AgitatingLensItem = null;
            CorruptHeartItem = null;
            GuttedHeart = false;
            //            NecromanticBrew = false;
            PureHeart = false;
            //            PungentEyeballMinion = false;
            CrystalSkullMinion = false;
            FusedLens = false;
            //            GroundStick = false;
            //            Probes = false;
            //            MagicalBulb = false;
            SkullCharm = false;
            PumpkingsCapeItem = null;
            LihzahrdTreasureBoxItem = null;
            BetsysHeartItem = null;
            BetsyDashing = false;
            MutantAntibodies = false;
            //            GravityGlobeEX = false;
            //            CelestialRune = false;
            AdditionalAttacks = false;
            //            MoonChalice = false;
            //            LunarCultist = false;
            //            TrueEyes = false;
            AbomWandItem = null;
            MasochistSoul = false;
            MasochistHeart = false;
            SandsofTime = false;
            DragonFang = false;
            SecurityWallet = false;
            FrigidGemstoneItem = null;
            WretchedPouchItem = null;
            NymphsPerfume = false;
            NymphsPerfumeRespawn = false;
            SqueakyAcc = false;
            RainbowSlime = false;
            //            SkeletronArms = false;
            SuperFlocko = false;
            IceQueensCrown = false;
            MiniSaucer = false;
            TribalCharm = false;
            SupremeDeathbringerFairy = false;
            //            GodEaterImbue = false;
            //            MutantSetBonus = false;
            //            Abominationn = false;
            //            PhantasmalRing = false;
            //            TwinsEX = false;
            TimsConcoction = false;
            Graze = false;
            GrazeRadius = 100f;
            DevianttHeartItem = null;
            MutantEyeItem = null;
            MutantEyeVisual = false;
            AbomRebirth = false;
            WasHurtBySomething = false;

            //debuffs
            Hexed = false;
            Unstable = false;
            Fused = false;
            Shadowflame = false;
            Oiled = false;
            Slimed = false;
            noDodge = false;
            noSupersonic = false;
            Bloodthirsty = false;
            DisruptedFocus = false;
            SinisterIcon = false;
            SinisterIconDrops = false;

            GodEater = false;
            FlamesoftheUniverse = false;
            MutantNibble = false;
            Asocial = false;
            Kneecapped = false;
            Defenseless = false;
            Purified = false;
            Infested = false;
            Rotting = false;
            SqueakyToy = false;
            Atrophied = false;
            Jammed = false;
            ReverseManaFlow = false;
            CurseoftheMoon = false;
            OceanicMaul = false;
            DeathMarked = false;
            Hypothermia = false;
            Midas = false;
            MutantPresence = MutantPresence ? Player.HasBuff(ModContent.BuffType<Buffs.Boss.MutantPresence>()) : false;
            DevianttPresence = false;
            Swarming = false;
            LowGround = false;
            Flipped = false;
            LihzahrdCurse = false;
            //LihzahrdBlessing = false;
            Berserked = false;
            HolyPrice = false;
            NanoInjection = false;
            ReduceMasomodeMinionNerf = false;

            //            if (WizardEnchant)
            //            {
            //                WizardEnchant = false;
            //                for (int i = 3; i <= 9; i++)
            //                {
            //                    if (!Player.armor[i].IsAir && (Player.armor[i].type == ModContent.ItemType<WizardEnchant>() || Player.armor[i].type == ModContent.ItemType<Items.Accessories.Forces.WillForce>()))
            //                    {
            //                        WizardEnchant = true;
            //                        CosmoForce = true;
            //                        EarthForce = true;
            //                        LifeForce = true;
            //                        NatureForce = true;
            //                        ShadowForce = true;
            //                        SpiritForce = true;
            //                        TerraForce = true;
            //                        WillForce = true;
            //                        WoodForce = true;
            //                        break;
            //                    }
            //                }
            //            }

            if (!Mash && MashCounter > 0)
                MashCounter--;
            Mash = false;
        }

        public override void OnRespawn(Player Player)
        {
            if (NymphsPerfumeRespawn && !FargoSoulsUtil.AnyBossAlive())
            {
                Player.statLife = Player.statLifeMax2;
            }
        }

        public override void UpdateDead()
        {
            if (SandsofTime && !FargoSoulsUtil.AnyBossAlive() && Player.respawnTimer > 10)
                Player.respawnTimer -= Eternity ? 6 : 1;

            if (Main.netMode == NetmodeID.MultiplayerClient && FargoSoulsWorld.MasochistModeReal && FargoSoulsUtil.AnyBossAlive())
            {
                if (Player.respawnTimer < 10)
                    Player.respawnTimer = 10;
                if (Main.npc[FargoSoulsGlobalNPC.boss].HasValidTarget && Main.npc[FargoSoulsGlobalNPC.boss].HasPlayerTarget)
                    Player.Center = Main.player[Main.npc[FargoSoulsGlobalNPC.boss].target].Center;
            }

            ReallyAwfulDebuffCooldown = 0;
            //            IronDebuffImmuneTime = 0;

            //            FreezeTime = false;
            //            freezeLength = 0;

            //            /*if (!Main.dedServ)
            //            {
            //                if (Fargowiltas.OldMusicFade > Main.musicVolume)
            //                {
            //                    Main.musicVolume = Fargowiltas.OldMusicFade;
            //                    Fargowiltas.OldMusicFade = 0;
            //                }
            //            }*/

            //            wingTimeModifier = 1f;
            FreeEaterSummon = true;
            if (Screenshake > 0)
                Screenshake--;

            //            EridanusEmpower = false;
            //            EridanusTimer = 0;
            //            GaiaSet = false;
            //            GaiaOffense = false;
            StyxSet = false;
            StyxMeter = 0;

            //debuffs
            Hexed = false;
            Unstable = false;
            unstableCD = 0;
            Fused = false;
            Shadowflame = false;
            Oiled = false;
            Slimed = false;
            noDodge = false;
            noSupersonic = false;
            lightningRodTimer = 0;

            //            BuilderMode = false;

            SlimyShieldFalling = false;
            CorruptHeartCD = 60;
            GuttedHeartCD = 60;
            //            NecromanticBrew = false;
            //            GroundPound = 0;
            NymphsPerfume = false;
            NymphsPerfumeCD = 30;
            //            PungentEyeballMinion = false;
            CrystalSkullMinion = false;
            //            MagicalBulb = false;
            //            LunarCultist = false;
            //            TrueEyes = false;
            BetsyDashing = false;

            WretchedPouchItem = null;
            WretchedPouchCD = 0;

            GodEater = false;
            FlamesoftheUniverse = false;
            MutantNibble = false;
            Asocial = false;
            Kneecapped = false;
            Defenseless = false;
            Purified = false;
            Infested = false;
            Rotting = false;
            SqueakyToy = false;
            Atrophied = false;
            Jammed = false;
            CurseoftheMoon = false;
            OceanicMaul = false;
            DeathMarked = false;
            Hypothermia = false;
            Midas = false;
            Bloodthirsty = false;
            DisruptedFocus = false;
            SinisterIcon = false;
            SinisterIconDrops = false;
            Graze = false;
            GrazeRadius = 100f;
            GrazeBonus = 0;
            DevianttHeartItem = null;
            MutantEyeItem = null;
            MutantEyeVisual = false;
            MutantEyeCD = 60;
            AbominableWandRevived = false;
            AbomRebirth = false;
            WasHurtBySomething = false;
            Mash = false;
            //            WizardEnchant = false;
            MashCounter = 0;

            MaxLifeReduction = 0;

            MasomodeWeaponUseTimer = 0;
            MasomodeMinionNerfTimer = 0;
        }

        public override void PreUpdate()
        {
            //            if (HurtTimer > 0)
            //                HurtTimer--;

            IsStandingStill = Math.Abs(Player.velocity.X) < 0.05 && Math.Abs(Player.velocity.Y) < 0.05;


            //            Player.npcTypeNoAggro[0] = true;

            if (FargoSoulsWorld.EternityMode && Player.active && !Player.dead && !Player.ghost)
            {
                //falling gives you dazed. wings save you
                /*if (Player.velocity.Y == 0f && Player.wingsLogic == 0 && !Player.noFallDmg && !Player.ghost && !Player.dead)
                {
                    int num21 = 25;
                    num21 += Player.extraFall;
                    int num22 = (int)(Player.position.Y / 16f) - Player.fallStart;
                    if (Player.mount.CanFly)
                    {
                        num22 = 0;
                    }
                    if (Player.mount.Cart && Minecart.OnTrack(Player.position, Player.width, Player.height))
                    {
                        num22 = 0;
                    }
                    if (Player.mount.Type == 1)
                    {
                        num22 = 0;
                    }
                    Player.mount.FatigueRecovery();

                    if (((Player.gravDir == 1f && num22 > num21) || (Player.gravDir == -1f && num22 < -num21)))
                    {
                        Player.immune = false;
                        int dmg = (int)(num22 * Player.gravDir - num21) * 10;
                        if (Player.mount.Active)
                            dmg = (int)(dmg * Player.mount.FallDamage);

                        Player.Hurt(PlayerDeathReason.ByOther(0), dmg, 0);
                        Player.AddBuff(BuffID.Dazed, 120);
                    }
                    Player.fallStart = (int)(Player.position.Y / 16f);
                }*/

                if (!NPC.downedBoss3 && Player.ZoneDungeon && !NPC.AnyNPCs(NPCID.DungeonGuardian))
                {
                    NPC.SpawnOnPlayer(Player.whoAmI, NPCID.DungeonGuardian);
                }
                

                if (Player.ZoneUnderworldHeight)
                {
                    if (!(Player.fireWalk || PureHeart || Player.lavaMax > 0))
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.OnFire, 2);
                }

                if (Player.ZoneJungle)
                {
                    if (Framing.GetTileSafely(Player.Center).WallType == WallID.LihzahrdBrickUnsafe)
                        Player.AddBuff(ModContent.BuffType<LihzahrdCurse>(), 2);

                    if (Player.wet && !Player.lavaWet && !Player.honeyWet && !MutantAntibodies)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Poisoned, 2);
                }

                if (Player.ZoneSnow)
                {
                    //if (!PureHeart && !Main.dayTime && Framing.GetTileSafely(Player.Center).WallType == WallID.None)
                    //    Player.AddBuff(BuffID.Chilled, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);

                    if (Player.wet && !Player.lavaWet && !Player.honeyWet && !MutantAntibodies)
                    {
                        //Player.AddBuff(BuffID.Frostburn, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                        MasomodeFreezeTimer++;
                        if (MasomodeFreezeTimer >= 600)
                        {
                            FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Frozen, 120);
                            MasomodeFreezeTimer = -300;
                        }
                    }
                    else
                    {
                        MasomodeFreezeTimer = 0;
                    }
                }
                else
                {
                    MasomodeFreezeTimer = 0;
                }

                /*if (Player.wet && !MutantAntibodies)
                {
                    if (Player.ZoneDesert)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Slow, 2);
                    if (Player.ZoneDungeon)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Cursed, 2);
                    Tile currentTile = Framing.GetTileSafely(Player.Center);
                    if (currentTile.WallType == WallID.GraniteUnsafe)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Weak, 2);
                    if (currentTile.WallType == WallID.MarbleUnsafe)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.BrokenArmor, 2);
                }*/

                if (Player.ZoneCorrupt)
                {
                    if (!PureHeart)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Darkness, 2);
                    if (Player.wet && !Player.lavaWet && !Player.honeyWet && !MutantAntibodies)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.CursedInferno, 2);
                }

                if (Player.ZoneCrimson)
                {
                    if (!PureHeart)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Bleeding, 2);
                    if (Player.wet && !Player.lavaWet && !Player.honeyWet && !MutantAntibodies)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Ichor, 2);
                }

                if (Player.ZoneHallow)
                {
                    if (Player.ZoneRockLayerHeight && !PureHeart)
                    {
                        float playerAbove = Player.Center.Y - 16 * 50;
                        float playerBelow = Player.Center.Y + 16 * 50;
                        if (playerAbove / 16 < Main.maxTilesY && playerBelow / 16 < Main.maxTilesY
                            && !Collision.CanHitLine(new Vector2(Player.Left.X, playerAbove), 0, 0, new Vector2(Player.Left.X, playerBelow), 0, 0)
                            && !Collision.CanHitLine(new Vector2(Player.Right.X, playerAbove), 0, 0, new Vector2(Player.Right.X, playerBelow), 0, 0))
                        {
                            Player.AddBuff(ModContent.BuffType<FlippedHallow>(), 90);
                        }
                    }
                    if (Player.wet && !Player.lavaWet && !Player.honeyWet && !MutantAntibodies)
                        FargoSoulsUtil.AddDebuffFixedDuration(Player, BuffID.Confused, 2);
                }

                if (!PureHeart && Main.raining && (Player.ZoneOverworldHeight || Player.ZoneSkyHeight) && Player.HeldItem.type != ItemID.Umbrella)
                {
                    Tile currentTile = Framing.GetTileSafely(Player.Center);
                    if (currentTile.WallType == WallID.None)
                    {
                        if (Player.ZoneSnow)
                            Player.AddBuff(ModContent.BuffType<Hypothermia>(), 2);
                        else
                            Player.AddBuff(BuffID.Wet, 2);
                        /*if (Main.hardMode)
                        {
                            lightningCounter++;

                            if (lightningCounter >= 600)
                            {
                                //tends to spawn in ceilings if the Player goes indoors/underground
                                Point tileCoordinates = Player.Top.ToTileCoordinates();

                                tileCoordinates.X += Main.rand.Next(-25, 25);
                                tileCoordinates.Y -= 15 + Main.rand.Next(-5, 5);

                                for (int index = 0; index < 10 && !WorldGen.SolidTile(tileCoordinates.X, tileCoordinates.Y) && tileCoordinates.Y > 10; ++index) tileCoordinates.Y -= 1;

                                Projectile.NewProjectile(tileCoordinates.X * 16 + 8, tileCoordinates.Y * 16 + 17, 0f, 0f, ProjectileID.VortexVortexLightning, 0, 2f, Main.myPlayer,
                                    0f, 0);

                                lightningCounter = 0;
                            }
                        }*/
                    }
                }

                if (Player.wet && !Player.lavaWet && !Player.honeyWet && !(Player.accFlipper || Player.gills || MutantAntibodies))
                    Player.AddBuff(ModContent.BuffType<Lethargic>(), 2);

                if (!PureHeart && !Player.buffImmune[BuffID.Suffocation] && Player.ZoneSkyHeight && Player.whoAmI == Main.myPlayer)
                {
                    bool inLiquid = Collision.DrownCollision(Player.position, Player.width, Player.height, Player.gravDir);
                    if (!inLiquid)
                    {
                        Player.breath -= 3;
                        if (++MasomodeSpaceBreathTimer > 10)
                        {
                            MasomodeSpaceBreathTimer = 0;
                            Player.breath--;
                        }
                        if (Player.breath == 0)
                            SoundEngine.PlaySound(SoundID.Item3);
                        if (Player.breath <= 0)
                            Player.AddBuff(BuffID.Suffocation, 2);
                    }
                }

                if (!PureHeart && !Player.buffImmune[BuffID.Webbed] && Player.stickyBreak > 0)
                {
                    Vector2 tileCenter = Player.Center;
                    tileCenter.X /= 16;
                    tileCenter.Y /= 16;
                    Tile currentTile = Framing.GetTileSafely((int)tileCenter.X, (int)tileCenter.Y);
                    if (currentTile != null && currentTile.WallType == WallID.SpiderUnsafe)
                    {
                        Player.AddBuff(BuffID.Webbed, 30);
                        Player.AddBuff(BuffID.Slow, 90);
                        Player.stickyBreak = 0;

                        Vector2 vector = Collision.StickyTiles(Player.position, Player.velocity, Player.width, Player.height);
                        if (vector.X != -1 && vector.Y != -1)
                        {
                            int num3 = (int)vector.X;
                            int num4 = (int)vector.Y;
                            WorldGen.KillTile(num3, num4, false, false, false);
                            if (Main.netMode == NetmodeID.MultiplayerClient && !Main.tile[num3, num4].HasTile)
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, num3, num4, 0f, 0, 0, 0);
                        }
                    }
                }

                if (!PureHeart && Main.bloodMoon)
                    Player.AddBuff(BuffID.WaterCandle, 2);

                //no more because cactus harder to break now
                /*if (!SandsofTime)
                {
                    Vector2 tileCenter = Player.Center;
                    tileCenter.X /= 16;
                    tileCenter.Y /= 16;
                    Tile currentTile = Framing.GetTileSafely((int)tileCenter.X, (int)tileCenter.Y);
                    if (currentTile != null && currentTile.type == TileID.Cactus && currentTile.nactive())
                    {
                        int damage = 10;
                        if (Player.ZoneCorrupt)
                        {
                            damage *= 2;
                            Player.AddBuff(BuffID.CursedInferno, Main.expertMode && Main.expertDebuffTime > 1 ? 150 : 300);
                        }
                        if (Player.ZoneCrimson)
                        {
                            damage *= 2;
                            Player.AddBuff(BuffID.Ichor, Main.expertMode && Main.expertDebuffTime > 1 ? 150 : 300);
                        }
                        if (Player.ZoneHoly)
                        {
                            damage *= 2;
                            Player.AddBuff(BuffID.Confused, Main.expertMode && Main.expertDebuffTime > 1 ? 150 : 300);
                        }

                        if (Main.hardMode)
                            damage *= 2;

                        if (Player.hurtCooldowns[0] <= 0) //same i-frames as spike tiles
                            Player.Hurt(PlayerDeathReason.ByCustomReason(Player.name + " was pricked by a Cactus."), damage, 0, false, false, false, 0);
                    }
                }*/

                if (MasomodeCrystalTimer > 0)
                    MasomodeCrystalTimer--;
            }

            if (!Infested && !FirstInfection)
                FirstInfection = true;

            //            if (Eternity && TinCrit < 50)
            //                TinCrit = 50;
            //            else if(TerrariaSoul && TinCrit < 20)
            //                TinCrit = 20;
            //            else if (TerraForce && TinCrit < 10)
            //                TinCrit = 10;



            //            if (VortexStealth && !VortexEnchant)
            //                VortexStealth = false;

            if (Unstable && Player.whoAmI == Main.myPlayer)
            {
                if (unstableCD == 0)
                {
                    Vector2 pos = Player.position;

                    int x = Main.rand.Next((int)pos.X - 500, (int)pos.X + 500);
                    int y = Main.rand.Next((int)pos.Y - 500, (int)pos.Y + 500);
                    Vector2 teleportPos = new Vector2(x, y);

                    while (Collision.SolidCollision(teleportPos, Player.width, Player.height) && teleportPos.X > 50 && teleportPos.X < (double)(Main.maxTilesX * 16 - 50) && teleportPos.Y > 50 && teleportPos.Y < (double)(Main.maxTilesY * 16 - 50))
                    {
                        x = Main.rand.Next((int)pos.X - 500, (int)pos.X + 500);
                        y = Main.rand.Next((int)pos.Y - 500, (int)pos.Y + 500);
                        teleportPos = new Vector2(x, y);
                    }

                    Player.Teleport(teleportPos, 1);
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, teleportPos.X, teleportPos.Y, 1);

                    unstableCD = 60;
                }
                unstableCD--;
            }



            //            if (ObsidianEnchant && obsidianCD > 0)
            //                obsidianCD--;


            //            if (BeeEnchant && beeCD > 0)
            //                beeCD--;

            //            if (GoldShell)
            //            {
            //                Player.immune = true;
            //                Player.immuneTime = 2;
            //                Player.hurtCooldowns[0] = 2;
            //                Player.hurtCooldowns[1] = 2;
            //                Player.stealth = 1;

            //                //immune to DoT
            //                if (Player.statLife < goldHP)
            //                    Player.statLife = goldHP;

            //                if (Player.ownedProjectileCounts[ModContent.ProjectileType<GoldShellProj>()] <= 0)
            //                    Projectile.NewProjectile(Player.Center.X, Player.Center.Y, 0f, 0f, ModContent.ProjectileType<GoldShellProj>(), 0, 0, Main.myPlayer);
            //            }

            //            if ((CobaltEnchant || AncientCobaltEnchant) && CobaltCD > 0)
            //                CobaltCD--;


            //            if (LihzahrdTreasureBox && Player.gravDir > 0 && Player.GetToggleValue("MasoGolem"))
            //            {
            //                if (Player.controlDown && !Player.mount.Active && !Player.controlJump)
            //                {
            //                    if (Player.velocity.Y != 0f)
            //                    {
            //                        if (Player.velocity.Y < 15f)
            //                            Player.velocity.Y = 15f;
            //                        if (GroundPound <= 0)
            //                            GroundPound = 1;
            //                    }
            //                }
            //                if (GroundPound > 0)
            //                {
            //                    if (Player.velocity.Y < 0f || Player.mount.Active)
            //                    {
            //                        GroundPound = 0;
            //                    }
            //                    else if (Player.velocity.Y == 0f)
            //                    {
            //                        if (Player.whoAmI == Main.myPlayer)
            //                        {
            //                            int x = (int)(Player.Center.X) / 16;
            //                            int y = (int)(Player.position.Y + Player.height + 8) / 16;
            //                            if (GroundPound > 15 && x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY
            //                                && Main.tile[x, y] != null && Main.tile[x, y].nactive() && Main.tileSolid[Main.tile[x, y].type])
            //                            {
            //                                GroundPound = 0;

            //                                int baseDamage = 60;
            //                                if (MasochistSoul)
            //                                    baseDamage *= 3;
            //                                Projectile.NewProjectile(Player.Center, Vector2.Zero, ModContent.ProjectileType<ExplosionSmall>(), baseDamage * 2, 12f, Player.whoAmI);
            //                                y -= 2;
            //                                for (int i = -3; i <= 3; i++)
            //                                {
            //                                    if (i == 0)
            //                                        continue;
            //                                    int tilePosX = x + 16 * i;
            //                                    int tilePosY = y;
            //                                    if (Main.tile[tilePosX, tilePosY] != null && tilePosX >= 0 && tilePosX < Main.maxTilesX)
            //                                    {
            //                                        while (Main.tile[tilePosX, tilePosY] != null && tilePosY >= 0 && tilePosY < Main.maxTilesY
            //                                            && !(Main.tile[tilePosX, tilePosY].nactive() && Main.tileSolid[Main.tile[tilePosX, tilePosY].type]))
            //                                        {
            //                                            tilePosY++;
            //                                        }
            //                                        Projectile.NewProjectile(tilePosX * 16 + 8, tilePosY * 16 + 8, 0f, -8f, ModContent.ProjectileType<GeyserFriendly>(), baseDamage, 8f, Player.whoAmI);
            //                                    }
            //                                }
            //                            }
            //                        }

            //                    }
            //                    else
            //                    {
            //                        Player.maxFallSpeed = 15f;
            //                        GroundPound++;
            //                    }
            //                }
            //            }

            //            //horizontal dash
            //            if (MonkDashing > 0)
            //            {
            //                MonkDashing--;

            //                //no loss of height
            //                //Player.maxFallSpeed = 0f;
            //                //Player.fallStart = (int)(Player.position.Y / 16f);
            //                //Player.gravity = 0f;
            //                //Player.position.Y = Player.oldPosition.Y;


            //                //Main.NewText(MonkDashing);

            //                /*if (MonkDashing == 0)
            //                {
            //                    Player.velocity *= 0f;
            //                    Player.dashDelay = 0;
            //                }*/
            //            }
            //            //vertical dash
            //            else if (MonkDashing < 0)
            //            {
            //                MonkDashing++;

            //                Player.immune = true;
            //                Player.maxFallSpeed *= 30f;
            //                Player.gravity = 1.5f;

            //                /*if (MonkDashing == 0)
            //                {
            //                    Player.velocity *= 0.5f;
            //                    Player.dashDelay = 0;
            //                }*/
            //            }
        }

        public override void PostUpdateBuffs()
        {
            if (FargoSoulsWorld.EternityMode)
            {
                Player.pickSpeed -= 0.25f;

                Player.tileSpeed += 0.25f;
                Player.wallSpeed += 0.25f;

                Player.moveSpeed += 0.25f;

                Player.manaRegenDelay = Math.Min(Player.manaRegenDelay, 30);
                Player.manaRegenBonus += 5;

                Player.wellFed = true; //no longer expert half regen unless fed
            }

            if (Berserked && !Player.CCed)
            {
                Player.controlUseItem = true;
                Player.releaseUseItem = true;
                //Player.HeldItem.autoReuse = true;
            }
        }

        public void BionomicPassiveEffect()
        {
            Player.buffImmune[BuffID.WindPushed] = true;
            Player.buffImmune[BuffID.Suffocation] = true;
            Player.manaFlower = true;
            Player.nightVision = true;
            SandsofTime = true;
            SecurityWallet = true;
            TribalCharm = true;
            NymphsPerfumeRespawn = true;
            if (Player.GetToggleValue("MasoCarrot", false))
                Player.scope = true;
        }

        public override void PostUpdateEquips()
        {
            //            Player.wingTimeMax = (int)(Player.wingTimeMax * wingTimeModifier);

            if (Player.armor.Any(i => i.active && (i.type == ModContent.ItemType<BionomicCluster>())))// || i.type == ModContent.ItemType<MasochistSoul>())))
                BionomicPassiveEffect();

            if (noDodge)
            {
                Player.onHitDodge = false;
                Player.shadowDodge = false;
                Player.blackBelt = false;
            }

            if (FargoSoulsWorld.EternityMode && Player.iceBarrier)
                Player.endurance -= 0.1f;

            if (Player.setSquireT2 || Player.setSquireT3 || Player.setMonkT2 || Player.setMonkT3 || Player.setHuntressT2 || Player.setHuntressT3 || Player.setApprenticeT2 || Player.setApprenticeT3 || Player.setForbidden)
                ReduceMasomodeMinionNerf = true;

            //            if (SquireEnchant)
            //                Player.setSquireT2 = true;

            //            if (ValhallaEnchant)
            //                Player.setSquireT3 = true;

            //            if (ApprenticeEnchant)
            //                Player.setApprenticeT2 = true;

            //            if (DarkEnchant)
            //                Player.setApprenticeT3 = true;

            //            if (HuntressEnchant)
            //                Player.setHuntressT2 = true;

            //            if (RedEnchant)
            //                Player.setHuntressT3 = true;

            //            if (MonkEnchant)
            //                Player.setMonkT2 = true;

            //            if (ShinobiEnchant)
            //                Player.setMonkT3 = true;
        }

        public override void PostUpdateMiscEffects()
        {
            if (TinEnchantActive)
                TinEnchant.TinPostUpdate(this);


            //disable minion nerf during ooa
            if (DD2Event.Ongoing && !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.betsyBoss, NPCID.DD2Betsy))
            {
                int n = NPC.FindFirstNPC(NPCID.DD2EterniaCrystal);
                if (n != Main.maxNPCs && Player.Distance(Main.npc[n].Center) < 3000)
                {
                    MasomodeMinionNerfTimer -= 2;
                    if (MasomodeMinionNerfTimer < 0)
                        MasomodeMinionNerfTimer = 0;
                }
            }

            if (MasomodeWeaponUseTimer > 0)
            {
                MasomodeWeaponUseTimer -= 1;
                MasomodeMinionNerfTimer += 1;
            }
            else
            {
                if (MasomodeMinionNerfTimer > 0)
                    MasomodeMinionNerfTimer -= 1;
            }

            if (MasomodeMinionNerfTimer > MaxMasomodeMinionNerfTimer)
                MasomodeMinionNerfTimer = MaxMasomodeMinionNerfTimer;

            //Main.NewText($"{MasomodeWeaponUseTimer} {MasomodeMinionNerfTimer} {ReduceMasomodeMinionNerf}");

            //            if (dashCD > 0)
            //                dashCD--;

            if (ReallyAwfulDebuffCooldown > 0)
                ReallyAwfulDebuffCooldown--;

            //            if (IronDebuffImmuneTime > 0)
            //                IronDebuffImmuneTime--;

            if (OceanicMaul && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, NPCID.DukeFishron))
            {
                Player.statLifeMax2 /= 5;
                if (Player.statLifeMax2 < 100)
                    Player.statLifeMax2 = 100;
            }

            //            if (GaiaOffense && !GaiaSet)
            //                GaiaOffense = false;

            if (QueenStingerItem != null && QueenStingerCD > 0)
            {
                QueenStingerCD--;
            }

            //            if (SpiderEnchant)
            //            {
            //                SummonCrit += LifeForce ? 30 : 15;
            //                if (TerrariaSoul)
            //                {
            //                    SummonCrit = Math.Max(SummonCrit, Player.meleeCrit);
            //                    SummonCrit = Math.Max(SummonCrit, Player.rangedCrit);
            //                    SummonCrit = Math.Max(SummonCrit, Player.magicCrit);
            //                }
            //            }

            if (RabiesVaccine)
                Player.buffImmune[BuffID.Rabies] = true;

            if (StealingCooldown > 0 && !Player.dead)
                StealingCooldown--;

            if (LihzahrdCurse)
            {
                Player.dangerSense = false;
                Player.InfoAccMechShowWires = false;
            }

            if ((Player.HeldItem.type == ItemID.WireCutter || Player.HeldItem.type == ItemID.WireKite)
                && (LihzahrdCurse || !Player.buffImmune[ModContent.BuffType<LihzahrdCurse>()])
                && (Framing.GetTileSafely(Player.Center).WallType == WallID.LihzahrdBrickUnsafe
                || Framing.GetTileSafely(Main.MouseWorld).WallType == WallID.LihzahrdBrickUnsafe))
                Player.controlUseItem = false;

            //            if (Solar)
            //            {
            //                if (!Player.setSolar && !TerrariaSoul) //nerf DR
            //                {
            //                    Player.endurance -= 0.15f;
            //                }

            //                Player.AddBuff(BuffID.SolarShield3, 5, false);
            //                Player.setSolar = true;
            //                Player.solarCounter++;
            //                int solarCD = 240;
            //                if (Player.solarCounter >= solarCD)
            //                {
            //                    if (Player.solarShields > 0 && Player.solarShields < 3)
            //                    {
            //                        for (int i = 0; i < 22; i++)
            //                        {
            //                            if (Player.buffType[i] >= BuffID.SolarShield1 && Player.buffType[i] <= BuffID.SolarShield2)
            //                            {
            //                                Player.DelBuff(i);
            //                            }
            //                        }
            //                    }
            //                    if (Player.solarShields < 3)
            //                    {
            //                        Player.AddBuff(BuffID.SolarShield1 + Player.solarShields, 5, false);
            //                        for (int i = 0; i < 16; i++)
            //                        {
            //                            Dust dust = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, 6, 0f, 0f, 100)];
            //                            dust.noGravity = true;
            //                            dust.scale = 1.7f;
            //                            dust.fadeIn = 0.5f;
            //                            dust.velocity *= 5f;
            //                        }
            //                        Player.solarCounter = 0;
            //                    }
            //                    else
            //                    {
            //                        Player.solarCounter = solarCD;
            //                    }
            //                }
            //                for (int i = Player.solarShields; i < 3; i++)
            //                {
            //                    Player.solarShieldPos[i] = Vector2.Zero;
            //                }
            //                for (int i = 0; i < Player.solarShields; i++)
            //                {
            //                    Player.solarShieldPos[i] += Player.solarShieldVel[i];
            //                    Vector2 value = (Player.miscCounter / 100f * 6.28318548f + i * (6.28318548f / Player.solarShields)).ToRotationVector2() * 6f;
            //                    value.X = Player.direction * 20;
            //                    Player.solarShieldVel[i] = (value - Player.solarShieldPos[i]) * 0.2f;
            //                }
            //                if (Player.dashDelay >= 0)
            //                {
            //                    Player.solarDashing = false;
            //                    Player.solarDashConsumedFlare = false;
            //                }
            //                bool flag = Player.solarDashing && Player.dashDelay < 0;
            //                if (Player.solarShields > 0 || flag)
            //                {
            //                    Player.dash = 3;
            //                }
            //            }

            //            if (Nebula && !Player.setNebula)
            //            {
            //                Player.setNebula = true;

            //                if (Player.nebulaCD > 0)
            //                    Player.nebulaCD--;

            //                if (!TerrariaSoul && !CosmoForce) //cap boosters
            //                {
            //                    void DecrementBuff(int buffType)
            //                    {
            //                        for (int i = 0; i < Player.buffType.Length; i++)
            //                        {
            //                            if (Player.buffType[i] == buffType && Player.buffTime[i] > 3)
            //                            {
            //                                Player.buffTime[i] = 3;
            //                                break;
            //                            }
            //                        }
            //                    };

            //                    if (Player.nebulaLevelDamage == 3)
            //                        DecrementBuff(BuffID.NebulaUpDmg3);
            //                    if (Player.nebulaLevelLife == 3)
            //                        DecrementBuff(BuffID.NebulaUpLife3);
            //                    if (Player.nebulaLevelMana == 3)
            //                        DecrementBuff(BuffID.NebulaUpMana3);
            //                }
            //            }

            if (AbomWandItem != null)
            {
                if (AbominableWandRevived) //has been revived already
                {
                    if (Player.statLife >= Player.statLifeMax2) //can revive again
                    {
                        AbominableWandRevived = false;

                        SoundEngine.PlaySound(SoundID.Item28, Player.Center);

                        const int max = 50; //make some indicator dusts
                        for (int i = 0; i < max; i++)
                        {
                            Vector2 vector6 = Vector2.UnitY * 8f;
                            vector6 = vector6.RotatedBy((i - (max / 2 - 1)) * 6.28318548f / max) + Main.LocalPlayer.Center;
                            Vector2 vector7 = vector6 - Main.LocalPlayer.Center;
                            int d = Dust.NewDust(vector6 + vector7, 0, 0, 87, 0f, 0f, 0, default(Color), 2f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].velocity = vector7;
                        }

                        for (int i = 0; i < 30; i++)
                        {
                            int d = Dust.NewDust(Player.position, Player.width, Player.height, 87, 0f, 0f, 0, default(Color), 2.5f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].velocity *= 8f;
                        }
                    }
                    else //cannot currently revive
                    {
                        Player.AddBuff(ModContent.BuffType<AbomCooldown>(), 2);
                    }
                }
            }

            if (Flipped && !Player.gravControl)
            {
                Player.gravControl = true;
                Player.controlUp = false;
                Player.gravDir = -1f;
                //Player.fallStart = (int)(Player.position.Y / 16f);
                //Player.jump = 0;
            }

            if (DevianttHeartItem != null)
            {
                if (DevianttHeartsCD > 0)
                    DevianttHeartsCD--;
            }

            if (Graze && ++GrazeCounter > 60) //decrease graze bonus over time
            {
                GrazeCounter = 0;
                if (GrazeBonus > 0f)
                    GrazeBonus -= 0.01;
            }

            if (LowGround)
            {
                Player.waterWalk = false;
                Player.waterWalk2 = false;
            }

            //            if (BetsysHeart && BetsyDashCD > 0)
            //            {
            //                BetsyDashCD--;
            //                if (BetsyDashCD == 0)
            //                {
            //                    SoundEngine.PlaySound(SoundID.Item9, Player.Center);

            //                    for (int i = 0; i < 30; i++)
            //                    {
            //                        int d = Dust.NewDust(Player.position, Player.width, Player.height, 87, 0, 0, 0, default, 2.5f);
            //                        Main.dust[d].noGravity = true;
            //                        Main.dust[d].velocity *= 4f;
            //                    }
            //                }
            //            }

            //            if (GravityGlobeEX && Player.GetToggleValue("MasoGrav2", false))
            //                Player.gravity = Player.defaultGravity;

            //            if (TikiEnchant && Player.GetToggleValue("Tiki"))
            //            {
            //                actualMinions = Player.maxMinions;
            //                Player.maxMinions = 999;

            //                if (Player.slotsMinions >= actualMinions)
            //                    TikiMinion = true;

            //                actualSentries = Player.maxTurrets;
            //                Player.maxTurrets = 999;

            //                if (getNumSentries() >= actualSentries)
            //                    TikiSentry = true;

            //            }

            //            if (JungleEnchant)
            //            {
            //                JungleEffect();
            //            }

            if (Atrophied)
            {
                Player.GetDamage(DamageClass.Melee) *= 0.01f;
                Player.GetCritChance(DamageClass.Melee) /= 100;
            }

            if (GuttedHeart && Player.whoAmI == Main.myPlayer)
            {
                //Player.statLifeMax2 += Player.statLifeMax / 10;
                GuttedHeartCD--;

                if (Player.velocity == Vector2.Zero && Player.itemAnimation == 0)
                    GuttedHeartCD--;

                if (GuttedHeartCD <= 0)
                {
                    GuttedHeartCD = 900;
                    if (Player.GetToggleValue("MasoBrain"))
                    {
                        int count = 0;
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<CreeperGutted>() && Main.npc[i].ai[0] == Player.whoAmI)
                                count++;
                        }
                        if (count < 5)
                        {
                            int multiplier = 1;
                            if (PureHeart)
                                multiplier = 2;
                            if (MasochistSoul)
                                multiplier = 5;
                            if (Main.netMode == NetmodeID.SinglePlayer)
                            {
                                int n = NPC.NewNPC(NPC.GetSpawnSource_NPCRelease(Player.whoAmI), (int)Player.Center.X, (int)Player.Center.Y, ModContent.NPCType<CreeperGutted>(), 0, Player.whoAmI, 0f, multiplier);
                                if (n != Main.maxNPCs)
                                    Main.npc[n].velocity = Vector2.UnitX.RotatedByRandom(2 * Math.PI) * 8;
                            }
                            else if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                var netMessage = Mod.GetPacket();
                                netMessage.Write((byte)0);
                                netMessage.Write((byte)Player.whoAmI);
                                netMessage.Write((byte)multiplier);
                                netMessage.Send();
                            }
                        }
                        else
                        {
                            int lowestHealth = -1;
                            for (int i = 0; i < Main.maxNPCs; i++)
                            {
                                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<CreeperGutted>() && Main.npc[i].ai[0] == Player.whoAmI)
                                {
                                    if (lowestHealth < 0)
                                        lowestHealth = i;
                                    else if (Main.npc[i].life < Main.npc[lowestHealth].life)
                                        lowestHealth = i;
                                }
                            }
                            if (Main.npc[lowestHealth].life < Main.npc[lowestHealth].lifeMax)
                            {
                                if (Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    int damage = Main.npc[lowestHealth].lifeMax - Main.npc[lowestHealth].life;
                                    Main.npc[lowestHealth].life = Main.npc[lowestHealth].lifeMax;
                                    CombatText.NewText(Main.npc[lowestHealth].Hitbox, CombatText.HealLife, damage);
                                }
                                else if (Main.netMode == NetmodeID.MultiplayerClient)
                                {
                                    var netMessage = Mod.GetPacket();
                                    netMessage.Write((byte)11);
                                    netMessage.Write((byte)Player.whoAmI);
                                    netMessage.Write((byte)lowestHealth);
                                    netMessage.Send();
                                }
                            }
                        }
                    }
                }
            }

            if (Slimed)
            {
                //slowed effect
                Player.moveSpeed *= .75f;
                Player.jump /= 2;
            }

            if (GodEater)
            {
                Player.statDefense = 0;
                Player.endurance = 0;
            }

            if (MutantNibble) //disables lifesteal, mostly
            {
                if (Player.statLife > 0 && StatLifePrevious > 0 && Player.statLife > StatLifePrevious)
                    Player.statLife = StatLifePrevious;
                if (Player.potionDelay < 2)
                    Player.potionDelay = 2;
            }

            if (Defenseless)
            {
                Player.statDefense -= 30;
                Player.endurance = 0;
                Player.longInvince = false;
                Player.noKnockback = false;
            }

            if (Purified)
            {
                KillPets();

                //removes all buffs/debuffs, but it interacts really weirdly with luiafk infinite potions.
                for (int i = 21; i >= 0; i--)
                {
                    if (Player.buffType[i] > 0 && Player.buffTime[i] > 0 && !Main.debuff[Player.buffType[i]])
                        Player.DelBuff(i);
                }
            }
            else if (Asocial)
            {
                KillPets();
                Player.maxMinions = 0;
                Player.maxTurrets = 0;
                Player.GetDamage(DamageClass.Summon) -= .5f;
            }
            else if (WasAsocial) //should only occur when above debuffs end
            {
                Player.hideMisc[0] = HidePetToggle0;
                Player.hideMisc[1] = HidePetToggle1;

                WasAsocial = false;
            }

            if (Rotting)
            {
                Player.moveSpeed *= 0.9f;
                //Player.statLifeMax2 -= Player.statLifeMax / 5;
                Player.statDefense -= 10;
                Player.endurance -= 0.1f;
                AttackSpeed -= 0.1f;
                Player.GetDamage(DamageClass.Generic) -= 0.1f;
            }

            if (Kneecapped)
            {
                Player.accRunSpeed = Player.maxRunSpeed;
            }

            if (OceanicMaul)
            {
                if (MaxLifeReduction > Player.statLifeMax2 - 100)
                    MaxLifeReduction = Player.statLifeMax2 - 100;
                Player.statLifeMax2 -= MaxLifeReduction;
                //if (Player.statLife > Player.statLifeMax2) Player.statLife = Player.statLifeMax2;
            }
            else
            {
                MaxLifeReduction = 0;
            }

            //            if (Eternity)
            //                Player.statManaMax2 = 999;
            //            else if (UniverseEffect)
            //                Player.statManaMax2 += 300;

            if (TungstenEnchantActive)
            {
                TungstenEnchant.TungstenIncreaseWeaponSize(this);
            }
            else
            {
                if (TungstenPrevSizeSave != -1)
                {
                    Player.HeldItem.scale = TungstenPrevSizeSave;
                }
            }

            if (AdditionalAttacks && AdditionalAttacksTimer > 0)
                AdditionalAttacksTimer--;

            //            if (WoodEnchant && CritterAttackTimer > 0)
            //            {
            //                CritterAttackTimer--;
            //            }

            //            if (Player.whoAmI == Main.myPlayer && Player.controlUseItem && Player.HeldItem.type == ModContent.ItemType<EaterLauncher>())
            //            {
            //                for (int i = 0; i < 20; i++)
            //                {
            //                    Vector2 offset = new Vector2();
            //                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
            //                    offset.X += (float)(Math.Sin(angle) * 300);
            //                    offset.Y += (float)(Math.Cos(angle) * 300);
            //                    Dust dust = Main.dust[Dust.NewDust(
            //                        Player.Center + offset - new Vector2(4, 4), 0, 0,
            //                        DustID.PurpleCrystalShard, 0, 0, 100, Color.White, 1f
            //                        )];
            //                    dust.velocity = Player.velocity;
            //                    if (Main.rand.NextBool(3))
            //                        dust.velocity += Vector2.Normalize(offset) * 5f;
            //                    dust.noGravity = true;

            //                    Vector2 offset2 = new Vector2();
            //                    double angle2 = Main.rand.NextDouble() * 2d * Math.PI;
            //                    offset2.X += (float)(Math.Sin(angle2) * 500);
            //                    offset2.Y += (float)(Math.Cos(angle2) * 500);
            //                    Dust dust2 = Main.dust[Dust.NewDust(
            //                        Player.Center + offset2 - new Vector2(4, 4), 0, 0,
            //                        DustID.PurpleCrystalShard, 0, 0, 100, Color.White, 1f
            //                        )];
            //                    dust2.velocity = Player.velocity;
            //                    if (Main.rand.NextBool(3))
            //                        dust2.velocity += Vector2.Normalize(offset2) * -5f;
            //                    dust2.noGravity = true;
            //                }
            //            }

            if (MutantPresence || DevianttPresence)
            {
                Player.statDefense /= 2;
                Player.endurance /= 2;
                Player.shinyStone = false;
            }

            if (TinEnchantActive)
            {
                //t
            }

            if (SlimyShieldItem != null || LihzahrdTreasureBoxItem != null)
            {
                //Player.justJumped use this tbh
                if (SlimyShieldFalling) //landing
                {
                    if (Player.velocity.Y < 0f)
                        SlimyShieldFalling = false;

                    if (Player.velocity.Y == 0f)
                    {
                        SlimyShieldFalling = false;
                        if (Player.whoAmI == Main.myPlayer && Player.gravDir > 0)
                        {
                            if (SlimyShieldItem != null && Player.GetToggleValue("MasoSlime"))
                            {
                                SoundEngine.PlaySound(SoundID.Item21, Player.Center);
                                Vector2 mouse = Main.MouseWorld;
                                int damage = 8;
                                if (SupremeDeathbringerFairy)
                                    damage = 16;
                                if (MasochistSoul)
                                    damage = 80;
                                damage = (int)(damage * Player.GetDamage(DamageClass.Melee));
                                for (int i = 0; i < 3; i++)
                                {
                                    Vector2 spawn = new Vector2(mouse.X + Main.rand.Next(-200, 201), mouse.Y - Main.rand.Next(600, 901));
                                    if (Collision.CanHitLine(mouse, 0, 0, spawn, 0, 0))
                                    {
                                        Vector2 speed = mouse - spawn;
                                        speed.Normalize();
                                        speed *= 10f;
                                        Projectile.NewProjectile(Player.GetProjectileSource_Accessory(SlimyShieldItem), spawn, speed, ModContent.ProjectileType<SlimeBall>(), damage, 1f, Main.myPlayer);
                                    }
                                }
                            }

                            if (LihzahrdTreasureBoxItem != null && Player.GetToggleValue("MasoBoulder"))
                            {
                                int dam = 60;
                                if (MasochistSoul)
                                    dam *= 3;
                                for (int i = -5; i <= 5; i += 2)
                                {
                                    Projectile.NewProjectile(Player.GetProjectileSource_Accessory(LihzahrdTreasureBoxItem), Player.Center, -12f * Vector2.UnitY.RotatedBy(Math.PI / 2 / 6 * i),
                                        ModContent.ProjectileType<LihzahrdBoulderFriendly>(), (int)(dam * Player.GetDamage(DamageClass.Melee)), 7.5f, Player.whoAmI);
                                }
                            }
                        }
                    }
                }
                else if (Player.velocity.Y > 3f)
                {
                    SlimyShieldFalling = true;
                }
            }

            if (AgitatingLensItem != null)
            {
                if (AgitatingLensCD++ > 15)
                {
                    AgitatingLensCD = 0;
                    if ((Math.Abs(Player.velocity.X) >= 5 || Math.Abs(Player.velocity.Y) >= 5) && Player.whoAmI == Main.myPlayer && Player.GetToggleValue("MasoEye"))
                    {
                        int damage = 12;
                        if (SupremeDeathbringerFairy)
                            damage = 24;
                        if (MasochistSoul)
                            damage = 60;
                        damage = (int)(damage * Player.GetDamage(DamageClass.Magic));
                        int proj = Projectile.NewProjectile(Player.GetProjectileSource_Accessory(AgitatingLensItem), Player.Center, Player.velocity * 0.1f, ModContent.ProjectileType<BloodScytheFriendly>(), damage, 5f, Player.whoAmI);
                    }
                }
            }

            if (WretchedPouchItem != null && --WretchedPouchCD <= 0)
            {
                WretchedPouchCD = 25;

                if (Player.whoAmI == Main.myPlayer && Player.GetToggleValue("MasoPouch"))
                {
                    NPC target = Main.npc.FirstOrDefault(n => n.active && n.Distance(Player.Center) < 360 && n.CanBeChasedBy() && Collision.CanHit(Player.position, Player.width, Player.height, n.position, n.width, n.height));
                    if (target != null)
                    {
                        SoundEngine.PlaySound(SoundID.Item103, Player.Center);

                        int dam = 40;
                        if (MasochistSoul)
                            dam *= 3;
                        dam = (int)(dam * Player.GetDamage(DamageClass.Magic));

                        void ShootTentacle(Vector2 baseVel, float variance, int aiMin, int aiMax)
                        {
                            Vector2 speed = baseVel.RotatedBy(variance * (Main.rand.NextDouble() - 0.5));
                            float ai0 = Main.rand.Next(aiMin, aiMax) * (1f / 1000f);
                            if (Main.rand.NextBool())
                                ai0 *= -1f;
                            float ai1 = Main.rand.Next(aiMin, aiMax) * (1f / 1000f);
                            if (Main.rand.NextBool())
                                ai1 *= -1f;
                            Projectile.NewProjectile(Player.GetProjectileSource_Accessory(WretchedPouchItem), Player.Center, speed, ModContent.ProjectileType<ShadowflameTentacle>(), dam, 4f, Player.whoAmI, ai0, ai1);
                        };

                        Vector2 vel = 8f * Player.DirectionTo(target.Center);
                        const int max = 6;
                        const float rotationOffset = MathHelper.TwoPi / max;
                        for (int i = 0; i < 3; i++) //shoot right at them
                            ShootTentacle(vel, rotationOffset, 60, 90);
                        for (int i = 0; i < 6; i++) //shoot everywhere
                            ShootTentacle(vel.RotatedBy(rotationOffset * i), rotationOffset, 30, 50);
                    }
                }
            }

            //            if (PalladEnchant)
            //            {
            //                int increment = Player.statLife - StatLifePrevious;
            //                if (increment > 0)
            //                {
            //                    PalladCounter += increment;
            //                    if (PalladCounter > 80)
            //                    {
            //                        PalladCounter = 0;
            //                        if (Player.whoAmI == Main.myPlayer && Player.statLife < Player.statLifeMax2 && Player.GetToggleValue("PalladiumOrb"))
            //                        {
            //                            int damage = EarthForce ? 80 : 40;
            //                            Projectile.NewProjectile(Player.Center, -Vector2.UnitY, ModContent.ProjectileType<PalladOrb>(),
            //                                HighestDamageTypeScaling(damage), 10f, Player.whoAmI, -1);
            //                        }
            //                    }
            //                }
            //            }

            StatLifePrevious = Player.statLife;
        }

        public override float UseTimeMultiplier(Item item)
        {
            int useTime = item.useTime;
            int useAnimate = item.useAnimation;

            if (useTime == 0 || useAnimate == 0 || item.damage <= 0)
            {
                return 1f;
            }

            if (Berserked)
            {
                AttackSpeed += .1f;
            }

            if (MagicSoul && item.DamageType == DamageClass.Magic)
            {
                AttackSpeed += .2f;
            }

            //            if (ThrowSoul && item.thrown)
            //            {
            //                AttackSpeed += .2f;
            //            }

            //            if (item.summon && (TikiMinion || TikiSentry))
            //            {
            //                AttackSpeed *= 0.75f;
            //            }

            //checks so weapons dont break
            while (useTime / AttackSpeed < 1)
            {
                AttackSpeed -= .1f;
            }

            while (useAnimate / AttackSpeed < 3)
            {
                AttackSpeed -= .1f;
            }

            return 1f / AttackSpeed;
        }

        public override void UpdateBadLifeRegen()
        {
            if (NanoInjection)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 10;
            }

            if (Shadowflame)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 10;
            }

            if (GodEater)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegen -= 170;

                Player.lifeRegenTime = 0;

                if (Player.lifeRegenCount > 0)
                    Player.lifeRegenCount = 0;

                Player.lifeRegenCount -= 70;
            }

            if (MutantNibble)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                if (Player.lifeRegenCount > 0)
                    Player.lifeRegenCount = 0;

                Player.lifeRegenTime = 0;
            }

            if (Infested)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= InfestedExtraDot();
            }

            if (Rotting)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 2;
            }

            if (CurseoftheMoon)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                if (Player.lifeRegenCount > 0)
                    Player.lifeRegenCount = 0;

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 20;
            }

            if (Oiled && Player.lifeRegen < 0)
            {
                Player.lifeRegen *= 2;
            }

            if (MutantPresence)
            {
                if (Player.lifeRegen > 5)
                    Player.lifeRegen = 5;
            }

            if (AbomRebirth)
            {
                Player.lifeRegen = 0;
                Player.lifeRegenCount = 0;
                Player.lifeRegenTime = 0;
            }

            if (FlamesoftheUniverse)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                if (Player.lifeRegenCount > 0)
                    Player.lifeRegenCount = 0;

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 30 + 50 + 48 + 30;
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            //            if (squireReduceIframes && (SquireEnchant || ValhallaEnchant))
            //            {
            //                if (Main.rand.NextBool(3))
            //                {
            //                    float scale = ValhallaEnchant ? 2f : 1.5f;
            //                    int type = ValhallaEnchant ? 87 : 91;
            //                    int dust = Dust.NewDust(Player.position, Player.width, Player.height, type, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 87, default(Color), scale);
            //                    Main.dust[dust].noGravity = true;
            //                    Main.dust[dust].velocity *= 1.8f;
            //                    Main.dust[dust].velocity.Y -= 0.5f;
            //                    if (Main.rand.NextBool(4))
            //                    {
            //                        Main.dust[dust].noGravity = false;
            //                        Main.dust[dust].scale *= 0.5f;
            //                    }
            //                    Main.PlayerDrawDust.Add(dust);
            //                }
            //                fullBright = true;
            //            }

            if (Shadowflame)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width, Player.height, DustID.Shadowflame, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
                }
                fullBright = true;
            }

            if (Rotting)
            {
                if (drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width, Player.height, DustID.Blood, Player.velocity.X * 0.1f, Player.velocity.Y * 0.1f, 0, default(Color), 2f);
                    Main.dust[dust].noGravity = Main.rand.NextBool();
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
                }
            }

            if (Purified)
            {
                if (drawInfo.shadow == 0f)
                {
                    int index2 = Dust.NewDust(Player.position, Player.width, Player.height, 91, 0.0f, 0.0f, 100, default, 2.5f);
                    Dust dust = Main.dust[index2];
                    Main.dust[index2].velocity *= 2f;
                    Main.dust[index2].noGravity = true;
                    drawInfo.DustCache.Add(index2);
                }
            }

            if (Hexed)
            {
                if (Main.rand.NextBool(3) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width, Player.height, DustID.BubbleBlock, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 2.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 2f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].color = Color.GreenYellow;
                    drawInfo.DustCache.Add(dust);
                }
                if (Main.rand.NextBool() && drawInfo.shadow == 0f)
                {
                    int index2 = Dust.NewDust(Player.position, Player.width, Player.height, 106, 0.0f, 0.0f, 100, default, 2.5f);
                    Dust dust = Main.dust[index2];
                    Main.dust[index2].noGravity = true;
                    drawInfo.DustCache.Add(index2);
                }
                fullBright = true;
            }

            if (Infested)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width, Player.height, 44, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), InfestedDust);
                    Main.dust[dust].noGravity = true;
                    //Main.dust[dust].velocity *= 1.8f;
                    // Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
                }
                fullBright = true;
            }

            if (GodEater)
            {
                if (Main.rand.NextBool(3) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, 86, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.2f;
                    Main.dust[dust].velocity.Y -= 0.15f;
                    drawInfo.DustCache.Add(dust);
                }
                r *= 0.15f;
                g *= 0.03f;
                b *= 0.09f;
                fullBright = true;
            }

            if (FlamesoftheUniverse)
            {
                /*drawInfo.drawPlayer.onFire = true;
                drawInfo.drawPlayer.onFire2 = true;
                drawInfo.drawPlayer.onFrostBurn = true;
                drawInfo.drawPlayer.ichor = true;
                drawInfo.drawPlayer.burned = true;*/
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int d = Dust.NewDust(Player.position, Player.width, Player.height, 21, Player.velocity.X * 0.2f, Player.velocity.Y * 0.2f, 100, new Color(50 * Main.rand.Next(6) + 5, 50 * Main.rand.Next(6) + 5, 50 * Main.rand.Next(6) + 5), 2.5f);
                    Main.dust[d].velocity.Y -= 1;
                    Main.dust[d].velocity *= 2f;
                    Main.dust[d].noGravity = true;
                    drawInfo.DustCache.Add(d);
                }
                fullBright = true;
            }

            if (CurseoftheMoon)
            {
                if (Main.rand.NextBool(5))
                {
                    int d = Dust.NewDust(Player.Center, 0, 0, 229, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 3f;
                    drawInfo.DustCache.Add(d);
                }
                if (Main.rand.NextBool(5))
                {
                    int d = Dust.NewDust(Player.position, Player.width, Player.height, 229, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity.Y -= 1f;
                    Main.dust[d].velocity *= 2f;
                    drawInfo.DustCache.Add(d);
                }
            }

            if (DeathMarked)
            {
                if (Main.rand.NextBool() && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width, Player.height, 109, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 0, default(Color), 1.5f);
                    Main.dust[dust].velocity.Y--;
                    if (Main.rand.Next(3) != 0)
                    {
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale += 0.5f;
                        Main.dust[dust].velocity *= 3f;
                        Main.dust[dust].velocity.Y -= 0.5f;
                    }
                    drawInfo.DustCache.Add(dust);
                }
                r *= 0.2f;
                g *= 0.2f;
                b *= 0.2f;
                fullBright = true;
            }

            if (Fused)
            {
                if (Main.rand.NextBool() && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position + new Vector2(Player.width / 2, Player.height / 5), 0, 0, DustID.Torch, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 0, default(Color), 2f);
                    Main.dust[dust].velocity.Y -= 2f;
                    Main.dust[dust].velocity *= 2f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].scale += 0.5f;
                        Main.dust[dust].noGravity = true;
                    }
                    drawInfo.DustCache.Add(dust);
                }
            }

            //            if (ForbiddenEnchant && drawInfo.shadow == 0f)
            //            {
            //                Color color12 = Player.GetImmuneAlphaPure(Lighting.GetColor((int)(drawInfo.position.X + Player.width * 0.5) / 16, (int)(drawInfo.position.Y + Player.height * 0.5) / 16, Color.White), drawInfo.shadow);
            //                Color color21 = Color.Lerp(color12, value2: Color.White, 0.7f);

            //                Texture2D texture2D2 = Main.extraTexture[74];
            //                Texture2D texture = Main.glowMaskTexture[217];
            //                bool flag8 = !Player.setForbiddenCooldownLocked;
            //                int num52 = (int)((Player.miscCounter / 300f * 6.28318548f).ToRotationVector2().Y * 6f);
            //                float num53 = (Player.miscCounter / 75f * 6.28318548f).ToRotationVector2().X * 4f;
            //                Color color22 = new Color(80, 70, 40, 0) * (num53 / 8f + 0.5f) * 0.8f;
            //                if (!flag8)
            //                {
            //                    num52 = 0;
            //                    num53 = 2f;
            //                    color22 = new Color(80, 70, 40, 0) * 0.3f;
            //                    color21 = color21.MultiplyRGB(new Color(0.5f, 0.5f, 1f));
            //                }
            //                Vector2 vector4 = new Vector2(((int)(drawInfo.position.X - Main.screenPosition.X - (Player.bodyFrame.Width / 2) + (Player.width / 2))), ((int)(drawInfo.position.Y - Main.screenPosition.Y + Player.height - Player.bodyFrame.Height + 4f))) + Player.bodyPosition + new Vector2((Player.bodyFrame.Width / 2), (Player.bodyFrame.Height / 2));
            //                vector4 += new Vector2((float)(-(float)Player.direction * 10), (float)(-20 + num52));
            //                DrawData value = new DrawData(texture2D2, vector4, null, color21, Player.bodyRotation, texture2D2.Size() / 2f, 1f, drawInfo.spriteEffects, 0);

            //                int num6 = 0;
            //                if (Player.dye[1] != null)
            //                {
            //                    num6 = Player.dye[1].dye;
            //                }
            //                value.shader = num6;
            //                Main.PlayerDrawData.Add(value);
            //                for (float num54 = 0f; num54 < 4f; num54 += 1f)
            //                {
            //                    value = new DrawData(texture, vector4 + (num54 * 1.57079637f).ToRotationVector2() * num53, null, color22, Player.bodyRotation, texture2D2.Size() / 2f, 1f, drawInfo.spriteEffects, 0);
            //                    Main.PlayerDrawData.Add(value);
            //                }
            //            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (proj.hostile)
                return;

            //reduce minion damage in emode if using a weapon, scales as you use weapons
            if (FargoSoulsUtil.IsMinionDamage(proj) && FargoSoulsWorld.EternityMode && MasomodeMinionNerfTimer > 0)
            {
                double modifier = ReduceMasomodeMinionNerf ? 0.5 : 0.75;
                modifier *= Math.Min((double)MasomodeMinionNerfTimer / MaxMasomodeMinionNerfTimer, 1.0);
                
                damage = (int)(damage * (1.0 - modifier));
            }

            //            if (apprenticeBonusDamage)
            //            {
            //                if (ShadowForce)
            //                {
            //                    damage = (int)(damage * 2.5f);
            //                }
            //                else
            //                {
            //                    damage = (int)(damage * 1.5f);
            //                }

            //                apprenticeBonusDamage = false;
            //                apprenticeSwitchReady = false;
            //                apprenticeCD = 0;

            //                //dust
            //                int dustId = Dust.NewDust(new Vector2(proj.position.X, proj.position.Y + 2f), proj.width, proj.height + 5, DustID.FlameBurst, 0, 0, 100, Color.Black, 2f);
            //                Main.dust[dustId].noGravity = true;

            //                Projectile.NewProjectile(target.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, damage, 0, Player.whoAmI);
            //            }

            //            if (Eternity)
            //            {
            //                if (crit)
            //                {
            //                    damage *= 5;
            //                }
            //            }
            //            else if (UniverseEffect)
            //            {
            //                if (crit)
            //                {
            //                    damage = (int)(damage * 2.5f);
            //                }
            //            }

            if (Hexed || (ReverseManaFlow && proj.DamageType == DamageClass.Magic))
            {
                target.life += damage;
                target.HealEffect(damage);

                if (target.life > target.lifeMax)
                {
                    target.life = target.lifeMax;
                }

                damage = 0;
                knockback = 0;
                crit = false;

                return;

            }

            if (SqueakyToy)
            {
                damage = 1;
                Squeak(target.Center);
                return;
            }

            if (Asocial && FargoSoulsUtil.IsMinionDamage(proj, true, false))
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }

            if (Atrophied && (proj.DamageType == DamageClass.Melee || proj.DamageType == DamageClass.Throwing))
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }

            ModifyHitNPCBoth(target, ref damage, ref crit);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            //            if (Eternity)
            //            {
            //                if (crit)
            //                {
            //                    damage *= 5;
            //                }
            //            }
            //            else if (UniverseEffect)
            //            {
            //                if (crit)
            //                {
            //                    damage = (int)(damage * 2.5f);
            //                }
            //            }

            if (Hexed || (ReverseManaFlow && item.DamageType == DamageClass.Magic))
            {
                target.life += damage;
                target.HealEffect(damage);

                if (target.life > target.lifeMax)
                {
                    target.life = target.lifeMax;
                }

                damage = 0;
                knockback = 0;
                crit = false;

                return;

            }

            if (SqueakyToy)
            {
                damage = 1;
                Squeak(target.Center);
                return;
            }

            if (Atrophied)
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }

            ModifyHitNPCBoth(target, ref damage, ref crit);
        }

        public void ModifyHitNPCBoth(NPC target, ref int damage, ref bool crit)
        {
            if (HolyPrice)
                damage = (int)(0.75 * damage);

            if (FirstStrike)
            {
                crit = true;
                damage = (int)(damage * 1.5f);
                Player.ClearBuff(ModContent.BuffType<FirstStrike>());
                //target.defense -= 5;
                target.AddBuff(BuffID.BrokenArmor, 600);
            }

            if (TungstenEnchantActive && Toggler != null && Player.GetToggleValue("Tungsten"))
            {
                TungstenEnchant.TungstenModifyDamage(Player, ref damage, ref crit);
            }
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            if (!SqueakyToy) return;
            damage = 1;
            Squeak(target.Center);
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
        {
            if (!SqueakyToy) return;
            damage = 1;
            Squeak(target.Center);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == NPCID.TargetDummy || target.friendly)
                return;

            if (proj.minion)// && proj.type != ModContent.ProjectileType<CelestialRuneAncientVision>() && proj.type != ModContent.ProjectileType<SpookyScythe>())
                TryAdditionalAttacks(proj.damage, proj.DamageType);

            OnHitNPCEither(target, damage, knockback, crit, projectile: proj);

            //            if (OriEnchant && proj.type == ProjectileID.FlowerPetal)
            //            {
            //                //int[] fireDebuffs = { BuffID.OnFire, BuffID.CursedInferno, BuffID.Frostburn, BuffID.ShadowFlame};
            //                //int debuff = Main.rand.Next(4);

            //                target.AddBuff(ModContent.BuffType<OriPoison>(), 300);
            //                target.immune[proj.owner] = 2;
            //            }

            //            
        }

        private void OnHitNPCEither(NPC target, int damage, float knockback, bool crit, Projectile projectile = null, Item item = null)
        {
            if (StyxSet)
            {
                StyxMeter += damage;
            }

            if (PearlwoodEnchantActive && Player.GetToggleValue("Pearl") && PearlwoodCD == 0 && (projectile == null || projectile.type != ProjectileID.HallowBossRainbowStreak))
            {
                PearlwoodEnchant.PearlwoodStarDrop(this, target, damage);
            }

                //            if (BeeEnchant && Player.GetToggleValue("Bee") && beeCD <= 0 && target.realLife == -1
                //                && (projectile == null || (projectile.type != ProjectileID.Bee && projectile.type != ProjectileID.GiantBee && projectile.maxPenetrate != 1 && projectile.owner == Main.myPlayer)))
                //            {
                //                bool force = LifeForce;
                //                if (force || Main.rand.NextBool())
                //                {
                //                    int beeDamage = projectile != null ? projectile.damage : item != null ? item.damage : damage;
                //                    if (beeDamage > 0)
                //                    {
                //                        if (!TerrariaSoul)
                //                            beeDamage = Math.Min(beeDamage, HighestDamageTypeScaling(300));
                //                        float beeKB = projectile != null ? projectile.knockBack : item != null ? item.knockBack : knockback;
                //                        int p = Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-35, 36) * 0.2f, Main.rand.Next(-35, 36) * 0.2f,
                //                            force ? ProjectileID.GiantBee : Player.beeType(), beeDamage, Player.beeKB(beeKB), Player.whoAmI);
                //                        if (p != Main.maxProjectiles)
                //                        {
                //                            if (projectile != null)
                //                            {
                //                                Main.projectile[p].melee = projectile.melee;
                //                                Main.projectile[p].ranged = projectile.ranged;
                //                                Main.projectile[p].magic = projectile.magic;
                //                                Main.projectile[p].minion = projectile.minion;
                //                            }
                //                            else if (item != null)
                //                            {
                //                                Main.projectile[p].melee = item.melee;
                //                                Main.projectile[p].ranged = item.ranged;
                //                                Main.projectile[p].magic = item.magic;
                //                                Main.projectile[p].minion = item.summon;
                //                            }
                //                        }
                //                    }
                //                    beeCD = 15;
                //                }
                //            }

                if (QueenStingerItem != null && QueenStingerCD <= 0 && Player.GetToggleValue("MasoHoney"))
            {
                QueenStingerCD = SupremeDeathbringerFairy ? 300 : 600;

                for (int j = 0; j < 15; j++) //spray honey
                {
                    Projectile.NewProjectile(Player.GetProjectileSource_Accessory(QueenStingerItem), target.Center, new Vector2(Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-8, -5)),
                        ModContent.ProjectileType<HoneyDrop>(), 0, 0f, Main.myPlayer);
                }
            }

            //            if (PalladEnchant && !Player.onHitRegen)
            //            {
            //                Player.AddBuff(BuffID.RapidHealing, Math.Min(300, damage / 3)); //heal time based on damage dealt, capped at 5sec
            //            }

            if (CopperEnchantActive)
            {
                CopperEnchant.CopperProc(this, target);
            }

            if (ShadewoodEnchantActive)
            {
                ShadewoodEnchant.ShadewoodProc(this, target, projectile);
            }

            //            if (Player.GetToggleValue("Obsidian") && ObsidianEnchant && obsidianCD == 0)
            //            {
            //                Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<ExplosionSmall>(), damage, 0, Player.whoAmI);
            //                obsidianCD = 30;
            //            }

            //            

            if (DevianttHeartItem != null && DevianttHeartsCD <= 0 && Player.GetToggleValue("MasoDevianttHearts")
                && (projectile == null || (projectile.type != ModContent.ProjectileType<FriendRay>() && projectile.type != ModContent.ProjectileType<FriendHeart>())))
            {
                DevianttHeartsCD = AbomWandItem == null ? 600 : 300;

                if (Main.myPlayer == Player.whoAmI)
                {
                    Vector2 offset = 300 * Player.DirectionFrom(Main.MouseWorld);
                    for (int i = -3; i <= 3; i++)
                    {
                        Vector2 spawnPos = Player.Center + offset.RotatedBy(Math.PI / 7 * i);
                        Vector2 speed = Vector2.Normalize(Main.MouseWorld - spawnPos);

                        int baseHeartDamage = AbomWandItem == null ? 17 : 170;
                        //heartDamage = (int)(heartDamage * Player.GetDamage(DamageClass.Summon));

                        float ai1 = (Main.MouseWorld - spawnPos).Length() / 17;

                        if (MutantEyeItem == null)
                            FargoSoulsUtil.NewSummonProjectile(Player.GetProjectileSource_Accessory(DevianttHeartItem), spawnPos, 17f * speed, ModContent.ProjectileType<FriendHeart>(), baseHeartDamage, 3f, Player.whoAmI, -1, ai1);
                        else
                            FargoSoulsUtil.NewSummonProjectile(Player.GetProjectileSource_Accessory(DevianttHeartItem), spawnPos, speed, ModContent.ProjectileType<FriendRay>(), baseHeartDamage, 3f, Player.whoAmI, (float)Math.PI / 7 * i);

                        FargoSoulsUtil.HeartDust(spawnPos, speed.ToRotation());
                    }
                }
            }

            //            if (GodEaterImbue)
            //            {
            //                if (target.FindBuffIndex(ModContent.BuffType<GodEater>()) < 0 && target.aiStyle != 37)
            //                {
            //                    if (target.type != ModContent.NPCType<MutantBoss>())
            //                    {
            //                        target.DelBuff(4);
            //                        target.buffImmune[ModContent.BuffType<GodEater>()] = false;
            //                    }
            //                    target.AddBuff(ModContent.BuffType<GodEater>(), 420);
            //                }
            //            }

            if (GladiatorEnchantActive && Player.whoAmI == Main.myPlayer && Player.GetToggleValue("Gladiator") && GladiatorCD <= 0 && (projectile == null || projectile.type != ModContent.ProjectileType<GladiatorJavelin>()))
            {
                GladiatorEnchant.GladiatorSpearDrop(this, item, projectile, target, damage);
            }

            //            if (SolarEnchant && Player.GetToggleValue("SolarFlare") && Main.rand.NextBool(4))
            //                target.AddBuff(ModContent.BuffType<SolarFlare>(), 300);

            if (TinEnchantActive)
            {
                TinEnchant.TinOnHitEnemy(this, crit);
            }

            if (LeadEnchantActive)
            {
                target.AddBuff(ModContent.BuffType<LeadPoison>(), 30);
            }


            //            /*if (PalladEnchant && !TerrariaSoul && palladiumCD == 0 && !target.immortal && !Player.moonLeech)
            //            {
            //                int heal = damage / 10;

            //                if ((EarthForce) && heal > 16)
            //                    heal = 16;
            //                else if (!EarthForce && !WizardEnchant && heal > 8)
            //                    heal = 8;
            //                else if (heal < 1)
            //                    heal = 1;
            //                Player.statLife += heal;
            //                Player.HealEffect(heal);
            //                palladiumCD = 240;
            //            }*/

            if (NymphsPerfume && NymphsPerfumeCD <= 0 && !target.immortal && !Player.moonLeech)
            {
                NymphsPerfumeCD = 600;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Item.NewItem(Player.GetItemSource_OnHit(target, ItemSourceID.None), target.Hitbox, ItemID.Heart);
                }
                else if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    var netMessage = Mod.GetPacket();
                    netMessage.Write((byte)9);
                    netMessage.Write((byte)Player.whoAmI);
                    netMessage.Write((byte)target.whoAmI);
                    netMessage.Send();
                }
            }

            //            if (UniverseEffect)
            //                target.AddBuff(ModContent.BuffType<FlamesoftheUniverse>(), 240, true);

            if (MasochistSoul)
            {
                target.AddBuff(ModContent.BuffType<Sadism>(), 600);
                //if (target.FindBuffIndex(ModContent.BuffType<Sadism>()) < 0 && target.aiStyle != 37)
                //{
                //    if (target.type != ModContent.NPCType<MutantBoss>())
                //    {
                //        target.DelBuff(4);
                //        target.buffImmune[ModContent.BuffType<Sadism>()] = false;
                //    }
                //    target.AddBuff(ModContent.BuffType<Sadism>(), 600);
                //}
            }
            else
            {
                if (BetsysHeartItem != null && crit)
                    target.AddBuff(BuffID.BetsysCurse, 300);

                if (PumpkingsCapeItem != null && crit)
                    target.AddBuff(ModContent.BuffType<Rotting>(), 300);

                if (QueenStingerItem != null)
                    target.AddBuff(SupremeDeathbringerFairy ? BuffID.Venom : BuffID.Poisoned, 120, true);

                if (FusedLens)
                    target.AddBuff(Main.rand.NextBool() ? BuffID.CursedInferno : BuffID.Ichor, 360);
            }

            //            if (!TerrariaSoul)
            //            {
            //                if (AncientShadowEnchant && Player.GetToggleValue("AncientShadow") && (projectile == null || projectile.type != ProjectileID.ShadowFlame) && Main.rand.NextBool(5))
            //                    target.AddBuff(BuffID.Darkness, 600, true);

            //                
            //            }

            //            if (GroundStick && Main.rand.NextBool(10) && Player.GetToggleValue("MasoLightning"))
            //                target.AddBuff(ModContent.BuffType<LightningRod>(), 300);

            //            if (GoldEnchant)
            //                target.AddBuff(BuffID.Midas, 120, true);

            if (DragonFang && !target.boss && !target.buffImmune[ModContent.BuffType<ClippedWings>()] && Main.rand.NextBool(10))
            {
                target.velocity.X = 0f;
                target.velocity.Y = 10f;
                target.AddBuff(ModContent.BuffType<ClippedWings>(), 240);
                target.netUpdate = true;
            }

            //            if (SpectreEnchant && Player.GetToggleValue("Spectre") && !target.immortal && Main.rand.NextBool())
            //            {
            //                if (projectile == null)
            //                {
            //                    //forced orb spawn reeeee
            //                    float num = 4f;
            //                    float speedX = Main.rand.Next(-100, 101);
            //                    float speedY = Main.rand.Next(-100, 101);
            //                    float num2 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
            //                    num2 = num / num2;
            //                    speedX *= num2;
            //                    speedY *= num2;
            //                    Projectile p = FargoSoulsUtil.NewProjectileDirectSafe(target.position, new Vector2(speedX, speedY), ProjectileID.SpectreWrath, damage / 2, 0, Player.whoAmI, target.whoAmI);

            //                    if ((SpiritForce || (crit && Main.rand.NextBool(5))) && p != null)
            //                    {
            //                        SpectreHeal(target, p);
            //                    }
            //                }
            //                else if (projectile.type != ProjectileID.SpectreWrath)
            //                {
            //                    SpectreHurt(projectile);

            //                    if (SpiritForce || (crit && Main.rand.NextBool(5)))
            //                    {
            //                        SpectreHeal(target, projectile);
            //                    }
            //                }
            //            }

            if (AbomWandItem != null)
            {
                //target.AddBuff(ModContent.BuffType<OceanicMaul>(), 900);
                //target.AddBuff(ModContent.BuffType<CurseoftheMoon>(), 900);
                if (crit && AbomWandCD <= 0 && Player.GetToggleValue("MasoFishron") && (projectile == null || projectile.type != ModContent.ProjectileType<AbomScytheFriendly>()))
                {
                    AbomWandCD = 360;

                    float screenX = Main.screenPosition.X;
                    if (Player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;

                    int dam = 150;
                    if (MutantEyeItem != null)
                        dam *= 3;

                    if (projectile != null && FargoSoulsUtil.IsMinionDamage(projectile))
                    {
                        int p = FargoSoulsUtil.NewSummonProjectile(Player.GetProjectileSource_Accessory(AbomWandItem), spawn, vel, ModContent.ProjectileType<SpectralAbominationn>(), dam, 10f, Player.whoAmI, target.whoAmI);
                        if (p != Main.maxProjectiles)
                            Main.projectile[p].DamageType = DamageClass.Summon;
                    }
                    else
                    {
                        dam = (int)(dam * Player.GetDamage(projectile == null ? DamageClass.Melee : projectile.DamageType));

                        int p = Projectile.NewProjectile(Player.GetProjectileSource_Accessory(AbomWandItem), spawn, vel, ModContent.ProjectileType<SpectralAbominationn>(), dam, 10f, Player.whoAmI, target.whoAmI);
                        if (p != Main.maxProjectiles)
                            Main.projectile[p].DamageType = projectile == null ? DamageClass.Melee : projectile.DamageType;
                    }
                }
            }

            if (CorruptHeartItem != null && CorruptHeartCD <= 0)
            {
                CorruptHeartCD = 60;
                if (Player.GetToggleValue("MasoEater") && (projectile == null || projectile.type != ProjectileID.TinyEater))
                {
                    SoundEngine.PlaySound(SoundID.NPCHit, (int)Player.Center.X, (int)Player.Center.Y, 1, 1f, 0);
                    for (int index1 = 0; index1 < 20; ++index1)
                    {
                        int index2 = Dust.NewDust(Player.position, Player.width, Player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust = Main.dust[index2];
                        dust.scale = dust.scale * 1.1f;
                        Main.dust[index2].noGravity = true;
                    }
                    for (int index1 = 0; index1 < 30; ++index1)
                    {
                        int index2 = Dust.NewDust(Player.position, Player.width, Player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust1 = Main.dust[index2];
                        dust1.velocity = dust1.velocity * 2.5f;
                        Dust dust2 = Main.dust[index2];
                        dust2.scale = dust2.scale * 0.8f;
                        Main.dust[index2].noGravity = true;
                    }
                    int num = 2;
                    if (Main.rand.NextBool(3))
                        ++num;
                    if (Main.rand.NextBool(6))
                        ++num;
                    if (Main.rand.NextBool(9))
                        ++num;
                    int dam = PureHeart ? 30 : 12;
                    if (MasochistSoul)
                        dam *= 2;
                    for (int index = 0; index < num; ++index)
                        Projectile.NewProjectile(Player.GetProjectileSource_Accessory(CorruptHeartItem), Player.Center.X, Player.Center.Y, Main.rand.Next(-35, 36) * 0.02f * 10f,
                            Main.rand.Next(-35, 36) * 0.02f * 10f, ProjectileID.TinyEater, (int)(dam * Player.GetDamage(DamageClass.Melee)), 1.75f, Player.whoAmI);
                }
            }

            if (FrigidGemstoneItem != null && FrigidGemstoneCD <= 0 && !target.immortal && (projectile == null || projectile.type != ModContent.ProjectileType<FrostFireball>()))
            {
                FrigidGemstoneCD = 30;
                float screenX = Main.screenPosition.X;
                if (Player.direction < 0)
                    screenX += Main.screenWidth;
                float screenY = Main.screenPosition.Y;
                screenY += Main.rand.Next(Main.screenHeight);
                Vector2 spawn = new Vector2(screenX, screenY);
                Vector2 vel = target.Center - spawn;
                vel.Normalize();
                vel *= 8f;
                int dam = (int)(40 * Player.GetDamage(DamageClass.Magic));
                if (MasochistSoul)
                    dam *= 2;
                Projectile.NewProjectile(Player.GetProjectileSource_Accessory(FrigidGemstoneItem), spawn, vel, ModContent.ProjectileType<FrostFireball>(), dam, 6f, Player.whoAmI, target.whoAmI);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == NPCID.TargetDummy || target.friendly)
                return;

            OnHitNPCEither(target, damage, knockback, crit, item: item);
        }

        //        public override void MeleeEffects(Item item, Rectangle hitbox)
        //        {
        //            if (ShroomEnchant && Player.GetToggleValue("ShroomiteShroom") && Player.stealth == 0 && !item.noMelee && (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.1) || Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.3) || Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.5) || Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.7) || Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.9)))
        //            {
        //                //hellish code from hammush
        //                float num340 = 0f;
        //                float num341 = 0f;
        //                float num342 = 0f;
        //                float num343 = 0f;
        //                if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.9))
        //                {
        //                    num340 = -7f;
        //                }
        //                if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.7))
        //                {
        //                    num340 = -6f;
        //                    num341 = 2f;
        //                }
        //                if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.5))
        //                {
        //                    num340 = -4f;
        //                    num341 = 4f;
        //                }
        //                if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.3))
        //                {
        //                    num340 = -2f;
        //                    num341 = 6f;
        //                }
        //                if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.1))
        //                {
        //                    num341 = 7f;
        //                }
        //                if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.7))
        //                {
        //                    num343 = 26f;
        //                }
        //                if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.3))
        //                {
        //                    num343 -= 4f;
        //                    num342 -= 20f;
        //                }
        //                if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.1))
        //                {
        //                    num342 += 6f;
        //                }
        //                if (Player.direction == -1)
        //                {
        //                    if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.9))
        //                    {
        //                        num343 -= 8f;
        //                    }
        //                    if (Player.itemAnimation == (int)((double)Player.itemAnimationMax * 0.7))
        //                    {
        //                        num343 -= 6f;
        //                    }
        //                }
        //                num340 *= 1.5f;
        //                num341 *= 1.5f;
        //                num343 *= (float)Player.direction;
        //                num342 *= Player.gravDir;
        //                Projectile.NewProjectile((float)(hitbox.X + hitbox.Width / 2) + num343, (float)(hitbox.Y + hitbox.Height / 2) + num342, (float)Player.direction * num341, num340 * Player.gravDir, ModContent.ProjectileType<ShroomiteShroom>(), item.damage / 5, 0f, Player.whoAmI, 0f, 0);
        //            }
        //        }

        public override bool CanBeHitByNPC(NPC npc, ref int CooldownSlot)
        {
            if (BetsyDashing)
                return false;
            return true;
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (BetsyDashing)
                return false;
            return true;
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (npc.coldDamage && Hypothermia)
                damage = (int)(damage * 1.2);

            if (npc.GetGlobalNPC<FargoSoulsGlobalNPC>().CurseoftheMoon)
                damage = (int)(damage * 0.8);

            if (IronDebuffImmuneTime > 0 || BetsyDashing || GoldShell || Player.HasBuff(ModContent.BuffType<ShellHide>()) || MonkDashing > 0)
            {
                foreach (int debuff in FargowiltasSouls.DebuffIDs) //immune to all debuffs
                {
                    if (!Player.HasBuff(debuff))
                        Player.buffImmune[debuff] = true;
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (proj.coldDamage && Hypothermia)
                damage = (int)(damage * 1.2);

            //implement when projectile source IS ACTUALLY FUCKING USABLE
            //if (npc.GetGlobalNPC<FargoSoulsGlobalNPC>().CurseoftheMoon)
            //damage = (int)(damage * 0.8);

            if (IronDebuffImmuneTime > 0 || BetsyDashing || GoldShell || Player.HasBuff(ModContent.BuffType<ShellHide>()) || MonkDashing > 0)
            {
                foreach (int debuff in FargowiltasSouls.DebuffIDs) //immune to all debuffs
                {
                    if (!Player.HasBuff(debuff))
                        Player.buffImmune[debuff] = true;
                }
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (FargoSoulsWorld.EternityMode && Player.shadowDodge) //prehurt hook not called on titanium dodge
                Player.AddBuff(ModContent.BuffType<HolyPrice>(), 600);
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (FargoSoulsWorld.EternityMode && Player.shadowDodge) //prehurt hook not called on titanium dodge
                Player.AddBuff(ModContent.BuffType<HolyPrice>(), 600);
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            //if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<NPCs.DeviBoss.DeviBoss>()))
            //{
            //    ((NPCs.DeviBoss.DeviBoss)Main.npc[EModeGlobalNPC.deviBoss].modNPC).PlayerInvulTriggered = true;
            //}

            //if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.abomBoss, ModContent.NPCType<NPCs.AbomBoss.AbomBoss>()))
            //{
            //    ((NPCs.AbomBoss.AbomBoss)Main.npc[EModeGlobalNPC.abomBoss].modNPC).PlayerInvulTriggered = true;
            //}

            //if (IronGuard && ironShieldTimer > 0 && !Player.immune)
            //{
            //    Player.immune = true;
            //    int invul = Player.longInvince ? 90 : 60;
            //    Player.immuneTime = invul;
            //    Player.hurtCooldowns[0] = invul;
            //    Player.hurtCooldowns[1] = invul;
            //    Player.AddBuff(BuffID.ParryDamageBuff, 300);
            //    Projectile.NewProjectile(Player.Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0f, Main.myPlayer);

            //    IronDebuffImmuneTime = invul;
            //    ironShieldCD = invul + 40;

            //    foreach (int debuff in Fargowiltas.DebuffIDs) //immune to all debuffs
            //    {
            //        if (!Player.HasBuff(debuff))
            //            Player.buffImmune[debuff] = true;
            //    }

            //    return false;
            //}

            if (FargoSoulsWorld.EternityMode && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.moonBoss, NPCID.MoonLordCore)
                && Player.Distance(Main.npc[EModeGlobalNPC.moonBoss].Center) < 2500)
            {
                Player.AddBuff(ModContent.BuffType<CurseoftheMoon>(), 180);
            }

            if (Player.whoAmI == Main.myPlayer && SqueakyAcc && Player.GetToggleValue("MasoSqueak") && Main.rand.NextBool(10))
            {
                Squeak(Player.Center);
                damage = 1;
            }

            if (DeathMarked)
            {
                damage = (int)(damage * 1.5);
            }

            //if (CrimsonEnchant && Player.GetToggleValue("Crimson"))
            //{
            //    //if was already healing, kill it
            //    if (Player.HasBuff(ModContent.BuffType<CrimsonRegen>()))
            //    {
            //        damage += CrimsonRegenSoFar;
            //    }
            //    else
            //    {
            //        Player.AddBuff(ModContent.BuffType<CrimsonRegen>(), 2);
            //    }

            //    CrimsonTotalToRegen = (damage - CrimsonRegenSoFar) / 2;

            //    if (NatureForce)
            //    {
            //        CrimsonTotalToRegen *= 2;
            //    }

            //    CrimsonRegenSoFar = 0;
            //}

            if (StyxSet && !BetsyDashing && !GoldShell && damage > 1 && Player.ownedProjectileCounts[ModContent.ProjectileType<StyxArmorScythe>()] > 0)
            {
                int scythesSacrificed = 0;
                const int maxSacrifice = 4;
                const double maxDR = 0.2;
                int scytheType = ModContent.ProjectileType<StyxArmorScythe>();
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].type == scytheType && Main.projectile[i].owner == Player.whoAmI)
                    {
                        if (Player.whoAmI == Main.myPlayer)
                            Main.projectile[i].Kill();
                        if (++scythesSacrificed >= maxSacrifice)
                            break;
                    }
                }

                damage = (int)(damage * (1.0 - maxDR / maxSacrifice * scythesSacrificed));
            }

            return true;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            WasHurtBySomething = true;

            //            if (MythrilEnchant && !TerrariaSoul)
            //            {
            //                Player.AddBuff(ModContent.BuffType<DisruptedFocus>(), 300);
            //            }

            if (TinEnchantActive)
            {
                TinEnchant.TinHurt(this);
            }

            //            if (ShellHide)
            //            {
            //                TurtleShellHP--;

            //                //some funny dust
            //                const int max = 30;
            //                for (int i = 0; i < max; i++)
            //                {
            //                    Vector2 vector6 = Vector2.UnitY * 5f;
            //                    vector6 = vector6.RotatedBy((i - (max / 2 - 1)) * 6.28318548f / max) + Main.LocalPlayer.Center;
            //                    Vector2 vector7 = vector6 - Main.LocalPlayer.Center;
            //                    int d = Dust.NewDust(vector6 + vector7, 0, 0, DustID.GoldFlame, 0f, 0f, 0, default(Color), 2f);
            //                    Main.dust[d].noGravity = true;
            //                    Main.dust[d].velocity = vector7;
            //                }
            //            }

            //            if (HurtTimer <= 0)
            //            {
            //                HurtTimer = 20;

            //                if (MoonChalice)
            //                {
            //                    if (Player.GetToggleValue("MasoVision"))
            //                    {
            //                        int dam = 50;
            //                        if (MasochistSoul)
            //                            dam *= 2;
            //                        for (int i = 0; i < 5; i++)
            //                            Projectile.NewProjectile(Player.Center.X, Player.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11),
            //                                ModContent.ProjectileType<AncientVision>(), (int)(dam * Player.GetDamage(DamageClass.Summon)), 6f, Player.whoAmI);
            //                    }
            //                }
            //                else if (CelestialRune && Player.GetToggleValue("MasoVision"))
            //                {
            //                    Projectile.NewProjectile(Player.Center, new Vector2(0, -10), ModContent.ProjectileType<AncientVision>(),
            //                        (int)(40 * Player.GetDamage(DamageClass.Summon)), 3f, Player.whoAmI);
            //                }

            //                /*if (LihzahrdTreasureBox && SoulConfig.Instance.GetValue(SoulConfig.Instance.LihzahrdBoxSpikyBalls))
            //                {
            //                    int dam = 60;
            //                    if (MasochistSoul)
            //                        dam *= 2;
            //                    for (int i = 0; i < 9; i++)
            //                        Projectile.NewProjectile(Player.Center.X, Player.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11),
            //                            ModContent.ProjectileType<LihzahrdSpikyBallFriendly>(), (int)(dam * Player.GetDamage(DamageClass.Melee)), 2f, Player.whoAmI);
            //                }*/

            //                if (MoltenEnchant && Player.GetToggleValue("MoltenE") && Player.whoAmI == Main.myPlayer/* && Main.netMode != NetModeID.MultiPlayerClient*/)
            //                {
            //                    int baseDamage = 50;
            //                    int multiplier = 2;
            //                    int cap = 150;

            //                    if (NatureForce)
            //                    {
            //                        baseDamage = 50;
            //                        multiplier = 4;
            //                        cap = 250;
            //                    }

            //                    if (TerrariaSoul)
            //                    {
            //                        baseDamage = 250;
            //                        multiplier = 5;
            //                        cap = 500;
            //                    }

            //                    int explosionDamage = baseDamage + (int)damage * multiplier;
            //                    if (explosionDamage > cap)
            //                        explosionDamage = cap;

            //                    Projectile p = FargoSoulsUtil.NewProjectileDirectSafe(Player.Center, Vector2.Zero, ModContent.ProjectileType<Explosion>(), (int)(explosionDamage * Player.GetDamage(DamageClass.Melee)), 0f, Main.myPlayer);
            //                    if (p != null)
            //                        p.GetGlobalProjectile<FargoSoulsGlobalProjectile>().CanSplit = false;
            //                }

            if (FossilEnchantActive)
            {
                FossilEnchant.FossilHurt(this, (int)damage);
            }

            if (IceQueensCrown && Player.GetToggleValue("IceQueensCrown"))
            {
                int freezeRange = 16 * 20;
                if (MasochistHeart)
                    freezeRange = 16 * 40;
                if (MasochistSoul)
                    freezeRange = 16 * 60;

                int freezeDuration = MasochistHeart || MasochistSoul ? 90 : 60;

                foreach (NPC n in Main.npc.Where(n => n.active && !n.friendly && n.damage > 0 && Player.Distance(FargoSoulsUtil.ClosestPointInHitbox(n, Player.Center)) < freezeRange && !n.dontTakeDamage))
                {
                    n.GetGlobalNPC<FargoSoulsGlobalNPC>().SnowChilled = true;
                    n.GetGlobalNPC<FargoSoulsGlobalNPC>().SnowChilledTimer = freezeDuration;
                    n.netUpdate = true;
                }

                foreach (Projectile p in Main.projectile.Where(p => p.active && p.hostile && p.damage > 0 && Player.Distance(FargoSoulsUtil.ClosestPointInHitbox(p, Player.Center)) < freezeRange && FargoSoulsUtil.CanDeleteProjectile(p)))
                {
                    p.GetGlobalProjectile<FargoSoulsGlobalProjectile>().ChilledProj = true;
                    p.GetGlobalProjectile<FargoSoulsGlobalProjectile>().ChilledTimer = freezeDuration;
                    p.netUpdate = true;
                }

                for (int i = 0; i < 40; i++)
                {
                    int d = Dust.NewDust(Player.Center, 0, 0, 76, 0, 0, 100, Color.White, 2.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 6f;
                }

                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(Player.Center, 0, 0, 88, 0, 0, 0, default, 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 6f;
                }
            }

            if (Midas && Main.myPlayer == Player.whoAmI)
                Player.DropCoins();

            GrazeBonus = 0;
            GrazeCounter = 0;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            bool retVal = true;

            if (Player.statLife <= 0) //revives
            {
                //                /*if (MutantSetBonus && Player.whoAmI == Main.myPlayer && retVal && Player.FindBuffIndex(ModContent.BuffType<MutantRebirth>()) == -1)
                //                {
                //                    Player.statLife = Player.statLifeMax2;
                //                    Player.HealEffect(Player.statLifeMax2);
                //                    Player.immune = true;
                //                    Player.immuneTime = 180;
                //                    Player.hurtCooldowns[0] = 180;
                //                    Player.hurtCooldowns[1] = 180;
                //                    Main.NewText("You've been revived!", Color.LimeGreen);
                //                    Player.AddBuff(ModContent.BuffType<MutantRebirth>(), 10800);
                //                    Projectile.NewProjectile(Player.Center, -Vector2.UnitY, ModContent.ProjectileType<GiantDeathray>(), (int)(7000 * Player.GetDamage(DamageClass.Summon)), 10f, Player.whoAmI);
                //                    retVal = false;
                //                }*/
                //                if (Player.whoAmI == Main.myPlayer && retVal && AbomRebirth)
                //                {
                //                    if (!WasHurtBySomething)
                //                    {
                //                        Player.statLife = 1;
                //                        //CombatText.NewText(Player.Hitbox, Color.SandyBrown, "You've been revived!");
                //                        return false; //this is deliberate
                //                    }
                //                }

                if (Player.whoAmI == Main.myPlayer && retVal && FossilEnchantActive && Player.FindBuffIndex(ModContent.BuffType<FossilReviveCD>()) == -1)
                {
                    FossilEnchant.FossilRevive(this);
                    retVal = false;
                }

                if (Player.whoAmI == Main.myPlayer && retVal && AbomWandItem != null && !AbominableWandRevived)
                {
                    AbominableWandRevived = true;
                    int heal = 1;
                    Player.statLife = heal;
                    Player.HealEffect(heal);
                    Player.immune = true;
                    Player.immuneTime = 120;
                    Player.hurtCooldowns[0] = 120;
                    Player.hurtCooldowns[1] = 120;
                    CombatText.NewText(Player.Hitbox, Color.Yellow, "You've been revived!", true);
                    Main.NewText("You've been revived!", Color.Yellow);
                    Player.AddBuff(ModContent.BuffType<AbomRebirth>(), MutantEyeItem == null ? 900 : 600);
                    retVal = false;
                    for (int i = 0; i < 24; i++)
                    {
                        Projectile.NewProjectile(Player.GetProjectileSource_Accessory(AbomWandItem), Player.Center, Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(4f, 16f),
                            ModContent.ProjectileType<StyxArmorScythe2>(), 0, 10f, Main.myPlayer, -60 - Main.rand.Next(60), -1);
                    }
                }
            }

            //            if (TinEnchant)
            //            {
            //                if (Eternity)
            //                {
            //                    TinCrit = 50;
            //                    eternityDamage = 0;
            //                }
            //                else if (TerrariaSoul)
            //                {
            //                    TinCrit = 20;
            //                }
            //                else if (TerraForce)
            //                {
            //                    TinCrit = 10;
            //                }
            //                else
            //                {
            //                    TinCrit = 4;
            //                }
            //            }

            //            //add more tbh
            if (Infested && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " could not handle the infection.");
            }

            if (Rotting && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " rotted away.");
            }

            if ((GodEater || FlamesoftheUniverse || CurseoftheMoon) && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was annihilated by divine wrath.");
            }

            if (DeathMarked)
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was reaped by the cold hand of death.");
            }

            /*if (MutantPresence)
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was penetrated.");
            }*/

            if (StatLifePrevious > 0 && Player.statLife > StatLifePrevious)
                StatLifePrevious = Player.statLife;

            //            if (MutantSetBonus && Player.whoAmI == Main.myPlayer && Player.statLife > 0 && Player.GetToggleValue("MasoReviveDeathray"))
            //            {
            //                Player.immune = true;
            //                if (Player.immuneTime < 180)
            //                    Player.immuneTime = 180;
            //                if (Player.hurtCooldowns[0] < 180)
            //                    Player.hurtCooldowns[0] = 180;
            //                if (Player.hurtCooldowns[1] < 180)
            //                    Player.hurtCooldowns[1] = 180;
            //                Projectile.NewProjectile(Player.Center, -Vector2.UnitY, ModContent.ProjectileType<GiantDeathray>(), (int)(7000 * Player.GetDamage(DamageClass.Summon)), 10f, Player.whoAmI);
            //            }

            return retVal;
        }

        //        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        //        {
        //            if (GaiaOffense)
        //            {
        //                int gaiaShader = GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<GaiaDye>()); //set armor and accessory shaders to gaia shader if set bonus is triggered
        //                drawInfo.bodyArmorShader = gaiaShader;
        //                drawInfo.headArmorShader = gaiaShader;
        //                drawInfo.legArmorShader = gaiaShader;
        //                drawInfo.wingShader = gaiaShader;
        //                drawInfo.handOnShader = gaiaShader;
        //                drawInfo.handOffShader = gaiaShader;
        //                drawInfo.shoeShader = gaiaShader;
        //            }

        //            if (IronGuard)
        //            {
        //                Player.bodyFrame.Y = Player.bodyFrame.Height * 10;
        //                if (ironShieldTimer > 0)
        //                {
        //                    int ironShader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.ReflectiveSilverDye);
        //                    drawInfo.bodyArmorShader = ironShader;
        //                    drawInfo.headArmorShader = ironShader;
        //                    drawInfo.legArmorShader = ironShader;
        //                    drawInfo.wingShader = ironShader;
        //                    drawInfo.handOnShader = ironShader;
        //                    drawInfo.handOffShader = ironShader;
        //                    drawInfo.shoeShader = ironShader;
        //                }
        //            }
        //        }

        //        public void AddPet(bool toggle, bool vanityToggle, int buff, int proj)
        //        {
        //            if (vanityToggle)
        //            {
        //                PetsActive = false;
        //                return;
        //            }

        //            if (Player.whoAmI == Main.myPlayer && toggle && Player.FindBuffIndex(buff) == -1 && Player.ownedProjectileCounts[proj] < 1)
        //            {
        //                Projectile p = Main.projectile[Projectile.NewProjectile(Player.Center.X, Player.Center.Y, 0f, -1f, proj, 0, 0f, Player.whoAmI)];
        //                p.netUpdate = true;
        //            }
        //        }

        public void AddMinion(Item item, bool toggle, int proj, int damage, float knockback)
        {
            if (Player.whoAmI != Main.myPlayer) return;
            if (Player.ownedProjectileCounts[proj] < 1 && Player.whoAmI == Main.myPlayer && toggle)
            {
                Projectile pro = Main.projectile[Projectile.NewProjectile(Player.GetProjectileSource_Accessory(item), Player.Center.X, Player.Center.Y, 0f, -1f, proj, damage, knockback, Main.myPlayer)];
                pro.netUpdate = true;
            }
        }

        private void KillPets()
        {
            int petId = Player.miscEquips[0].buffType;
            int lightPetId = Player.miscEquips[1].buffType;

            Player.buffImmune[petId] = true;
            Player.buffImmune[lightPetId] = true;

            Player.ClearBuff(petId);
            Player.ClearBuff(lightPetId);

            //memorizes Player selections
            if (!WasAsocial)
            {
                HidePetToggle0 = Player.hideMisc[0];
                HidePetToggle1 = Player.hideMisc[1];

                WasAsocial = true;
            }

            //disables pet and light pet too!
            if (!Player.hideMisc[0])
            {
                Player.TogglePet();
            }

            if (!Player.hideMisc[1])
            {
                Player.ToggleLight();
            }

            Player.hideMisc[0] = true;
            Player.hideMisc[1] = true;
        }

        public void Squeak(Vector2 center)
        {
            if (!Main.dedServ)
            {
                int rng = Main.rand.Next(6);

                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(FargowiltasSouls.Instance, "Sounds/SqueakyToy/squeak" + (rng + 1)).WithPitchVariance(.5f), center);
            }
        }

        private int InfestedExtraDot()
        {
            int buffIndex = Player.FindBuffIndex(ModContent.BuffType<Infested>());
            if (buffIndex == -1)
            {
                buffIndex = Player.FindBuffIndex(ModContent.BuffType<InfestedEX>());
                if (buffIndex == -1)
                    return 0;
            }

            int timeLeft = Player.buffTime[buffIndex];
            float baseVal = (float)(MaxInfestTime - timeLeft) / 90; //change the denominator to adjust max power of DOT
            int modifier = (int)(baseVal * baseVal + 4);

            InfestedDust = baseVal / 10 + 1f;
            if (InfestedDust > 5f)
                InfestedDust = 5f;

            return modifier * 2;
        }

        //        public void AllDamageUp(float dmg)
        //        {
        //            Player.GetDamage(DamageClass.Magic) += dmg;
        //            Player.GetDamage(DamageClass.Melee) += dmg;
        //            Player.GetDamage(DamageClass.Ranged) += dmg;
        //            Player.GetDamage(DamageClass.Summon) += dmg;
        //        }

        //        public void AllCritUp(int crit)
        //        {
        //            Player.meleeCrit += crit;
        //            Player.rangedCrit += crit;
        //            Player.magicCrit += crit;
        //        }


        public override bool PreItemCheck()
        {
            if (Berserked || (TribalCharm && Player.GetToggleValue("TribalCharm", false) && Player.HeldItem.type != ItemID.RodofDiscord && Player.HeldItem.fishingPole == 0))
            {
                TribalAutoFire = Player.HeldItem.autoReuse;
                Player.HeldItem.autoReuse = true;
            }

            /*if (FargoSoulsWorld.MasochistMode) //maso item nerfs
            {
                PreNerfDamage = Player.HeldItem.damage;
                Player.HeldItem.damage = (int)(Player.HeldItem.damage * MasoItemNerfs(Player.HeldItem.type));
            }*/

            return true;
        }

        public override void PostItemCheck()
        {
            if (Berserked || (TribalCharm && Player.GetToggleValue("TribalCharm", false) && Player.HeldItem.type != ItemID.RodofDiscord && Player.HeldItem.fishingPole == 0))
            {
                Player.HeldItem.autoReuse = TribalAutoFire;
            }

            /*if (FargoSoulsWorld.MasochistMode) //revert maso item nerfs
            {
                Player.HeldItem.damage = PreNerfDamage;
            }*/
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage, ref float flat)
        {
            if (FargoSoulsWorld.EternityMode)
            {
                damage *= MasoItemNerfs(item.type);

                if (item.DamageType == DamageClass.Ranged) //changes all of these to additive
                {
                    //shroomite headpieces
                    if (item.useAmmo == AmmoID.Arrow || item.useAmmo == AmmoID.Stake)
                    {
                        damage /= Player.arrowDamage.Multiplicative;
                        damage += Player.arrowDamage.Multiplicative - 1f;
                    }
                    else if (item.useAmmo == AmmoID.Bullet || item.useAmmo == AmmoID.CandyCorn)
                    {
                        damage /= Player.bulletDamage.Multiplicative;
                        damage += Player.bulletDamage.Multiplicative - 1f;
                    }
                    else if (item.useAmmo == AmmoID.Rocket || item.useAmmo == AmmoID.StyngerBolt || item.useAmmo == AmmoID.JackOLantern || item.useAmmo == AmmoID.NailFriendly)
                    {
                        damage /= Player.bulletDamage.Multiplicative;
                        damage += Player.bulletDamage.Multiplicative - 1f;
                    }
                }
            }
        }

        private float MasoItemNerfs(int type)
        {
            switch (type)
            {
                case ItemID.BlizzardStaff:
                    AttackSpeed *= 0.5f;
                    return 2f / 3f;

                case ItemID.DemonScythe:
                    if (!NPC.downedBoss2)
                    {
                        AttackSpeed *= 0.75f;
                        return 0.5f;
                    }
                    return 2f / 3f;

                case ItemID.StarCannon:
                case ItemID.ElectrosphereLauncher:
                case ItemID.DaedalusStormbow:
                case ItemID.BeesKnees:
                case ItemID.LaserMachinegun:
                    return 2f / 3f;

                case ItemID.Beenade:
                case ItemID.Razorpine:
                    AttackSpeed *= 2f / 3f;
                    return 2f / 3f;

                case ItemID.DD2BetsyBow:
                case ItemID.Uzi:
                case ItemID.PhoenixBlaster:
                case ItemID.LastPrism:
                case ItemID.OnyxBlaster:
                case ItemID.Handgun:
                case ItemID.SpikyBall:
                case ItemID.SDMG:
                case ItemID.Xenopopper:
                case ItemID.NebulaArcanum:
                case ItemID.PainterPaintballGun:
                case ItemID.MoltenFury:
                case ItemID.Phantasm:
                    return 0.75f;

                case ItemID.VampireKnives:
                    AttackSpeed *= 0.75f;
                    return 0.75f;

                case ItemID.SnowmanCannon:
                case ItemID.SkyFracture:
                    return 0.8f;

                case ItemID.SpaceGun:
                    if (!NPC.downedBoss2)
                    {
                        AttackSpeed *= 0.75f;
                        return 0.75f;
                    }
                    return 0.85f;

                case ItemID.Tsunami:
                case ItemID.Flairon:
                case ItemID.ChlorophyteShotbow:
                case ItemID.HellwingBow:
                case ItemID.DartPistol:
                case ItemID.DartRifle:
                case ItemID.Megashark:
                case ItemID.BatScepter:
                case ItemID.ChainGun:
                case ItemID.VortexBeater:
                case ItemID.RavenStaff:
                case ItemID.XenoStaff:
                case ItemID.StardustDragonStaff:
                    return 0.85f;

                case ItemID.BeeGun:
                case ItemID.Grenade:
                case ItemID.StickyGrenade:
                case ItemID.BouncyGrenade:
                    AttackSpeed *= 2f / 3f;
                    return 1f;

                case ItemID.DD2BallistraTowerT1Popper:
                case ItemID.DD2BallistraTowerT2Popper:
                case ItemID.DD2BallistraTowerT3Popper:
                case ItemID.DD2ExplosiveTrapT1Popper:
                case ItemID.DD2ExplosiveTrapT2Popper:
                case ItemID.DD2ExplosiveTrapT3Popper:
                case ItemID.DD2FlameburstTowerT1Popper:
                case ItemID.DD2FlameburstTowerT2Popper:
                case ItemID.DD2FlameburstTowerT3Popper:
                case ItemID.DD2LightningAuraT1Popper:
                case ItemID.DD2LightningAuraT2Popper:
                case ItemID.DD2LightningAuraT3Popper:
                    AttackSpeed *= 2f / 3f;
                    return 1f;

                case ItemID.MonkStaffT3: //sky dragon's fury
                    return 1.25f;

                default:
                    return 1f;
            }
        }

        //        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        //        {
        //            if (bait.type == ModContent.ItemType<TruffleWormEX>())
        //            {
        //                caughtType = 0;
        //                bool spawned = false;
        //                for (int i = 0; i < 1000; i++)
        //                {
        //                    if (Main.projectile[i].active && Main.projectile[i].bobber
        //                        && Main.projectile[i].owner == Player.whoAmI && Player.whoAmI == Main.myPlayer)
        //                    {
        //                        Main.projectile[i].ai[0] = 2f; //cut fishing lines
        //                        Main.projectile[i].netUpdate = true;

        //                        if (!spawned && Main.projectile[i].wet && FargoSoulsWorld.EternityMode && !NPC.AnyNPCs(NPCID.DukeFishron)) //should spawn boss
        //                        {
        //                            spawned = true;
        //                            if (Main.netMode == NetmodeID.SinglePlayer) //singlePlayer
        //                            {
        //                                EModeGlobalNPC.spawnFishronEX = true;
        //                                NPC.NewNPC((int)Main.projectile[i].Center.X, (int)Main.projectile[i].Center.Y + 100,
        //                                    NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, Player.whoAmI);
        //                                EModeGlobalNPC.spawnFishronEX = false;
        //                                Main.NewText("Duke Fishron EX has awoken!", 50, 100, 255);
        //                            }
        //                            else if (Main.netMode == NetmodeID.MultiPlayerClient) //MP, broadcast(?) packet from spawning Player's client
        //                            {
        //                                var netMessage = mod.GetPacket();
        //                                netMessage.Write((byte)77);
        //                                netMessage.Write((byte)Player.whoAmI);
        //                                netMessage.Write((int)Main.projectile[i].Center.X);
        //                                netMessage.Write((int)Main.projectile[i].Center.Y + 100);
        //                                netMessage.Send();
        //                            }
        //                            else if (Main.netMode == NetmodeID.Server)
        //                            {
        //                                ChatHelper.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral("???????"), Color.White);
        //                            }
        //                        }
        //                    }
        //                }
        //                if (spawned)
        //                {
        //                    bait.stack--;
        //                    if (bait.stack <= 0)
        //                        bait.SetDefaults(0);
        //                }
        //            }
        //        }

        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            if (Player.whoAmI == Main.myPlayer && GuttedHeart && Player.GetToggleValue("MasoBrain"))
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.type == ModContent.NPCType<CreeperGutted>() && npc.ai[0] == Player.whoAmI)
                    {
                        int heal = npc.lifeMax - npc.life;

                        if (Main.netMode == NetmodeID.SinglePlayer)
                        {
                            if (heal > 0)
                            {
                                npc.HealEffect(heal);
                                npc.life = npc.lifeMax;
                            }
                        }
                        else if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            var netMessage = Mod.GetPacket();
                            netMessage.Write((byte)11);
                            netMessage.Write((byte)Player.whoAmI);
                            netMessage.Write((byte)i);
                            netMessage.Send();
                        }
                    }
                }
            }
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (weapon.DamageType == DamageClass.Ranged)
            {
                if (RangedEssence && Main.rand.NextBool(10))
                    return false;
                if (RangedSoul && Main.rand.NextBool(5))
                    return false;
            }
            //if (GaiaSet && Main.rand.NextBool(10))
            //    return false;
            return true;
        }

        //        int frameCounter = 0;
        //        int frameSnow = 1;
        //        int frameMutantAura = 0;
        //        //int frameMutantLightning = 0;

        //        public static readonly PlayerLayer BlizzardEffect = new PlayerLayer("FargowiltasSouls", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        //        {
        //            if (drawInfo.shadow != 0f)
        //            {
        //                return;
        //            }
        //            Player drawPlayer = drawInfo.drawPlayer;
        //            Mod mod = ModLoader.GetMod("FargowiltasSouls");
        //            FargoSoulsPlayer modPlayer = drawPlayer.GetModPlayer<FargoSoulsPlayer>();

        //            if (++modPlayer.frameCounter > 60)
        //                modPlayer.frameCounter = 0;

        //            if (!drawPlayer.dead)
        //            {
        //                if (modPlayer.MutantSetBonus)
        //                {
        //                    if (modPlayer.frameCounter % 4 == 0)
        //                    {
        //                        if (++modPlayer.frameMutantAura >= 19)
        //                            modPlayer.frameMutantAura = 0;
        //                    }

        //                    Texture2D texture = FargowiltasSouls.Instance.Assets.Request<Texture2D>("NPCs/MutantBoss/MutantAura", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        //                    int frameSize = texture.Height / 19;
        //                    int drawX = (int)(drawPlayer.MountedCenter.X - Main.screenPosition.X);
        //                    int drawY = (int)(drawPlayer.MountedCenter.Y - Main.screenPosition.Y - 16 * drawPlayer.gravDir);
        //                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, frameSize * modPlayer.frameMutantAura, texture.Width, frameSize), Color.White, drawPlayer.gravDir < 0 ? MathHelper.Pi : 0, new Vector2(texture.Width / 2f, frameSize / 2f), 1f, drawPlayer.direction < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        //                    Main.PlayerDrawData.Add(data);
        //                }
        //                /*if (modPlayer.MutantSetBonus)
        //                {
        //                    if (modPlayer.frameCounter % 4 == 0)
        //                    {
        //                        if (++modPlayer.frameMutantLightning >= 20)
        //                            modPlayer.frameMutantLightning = 0;
        //                    }

        //                    Texture2D texture = FargowiltasSouls.Instance.Assets.Request<Texture2D>("NPCs/MutantBoss/MutantLightning", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        //                    int frameSize = texture.Height / 20;
        //                    int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
        //                    int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);
        //                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, frameSize * modPlayer.frameMutantLightning, texture.Width, frameSize), Color.White, 0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f, SpriteEffects.None, 0);
        //                    Main.PlayerDrawData.Add(data);
        //                }*/
        //                if (modPlayer.SnowVisual)
        //                {
        //                    if (modPlayer.frameCounter % 5 == 0)
        //                    {
        //                        if (++modPlayer.frameSnow > 20)
        //                            modPlayer.frameSnow = 1;
        //                    }

        //                    Texture2D texture = FargowiltasSouls.Instance.Assets.Request<Texture2D>("Projectiles/Souls/SnowBlizzard", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        //                    int frameSize = texture.Height / 20;
        //                    int drawX = (int)(drawPlayer.MountedCenter.X - Main.screenPosition.X);
        //                    int drawY = (int)(drawPlayer.MountedCenter.Y - Main.screenPosition.Y);
        //                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, frameSize * modPlayer.frameSnow, texture.Width, frameSize), Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f)), drawPlayer.gravDir < 0 ? MathHelper.Pi : 0f, new Vector2(texture.Width / 2f, frameSize / 2f), 1f, SpriteEffects.None, 0);
        //                    Main.PlayerDrawData.Add(data);
        //                }

        //                //GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(drawPlayer.dye[1].type, drawPlayer, data);
        //            }
        //        });

        //        public static readonly PlayerLayer MashLayer = new PlayerLayer("FargowiltasSouls", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        //        {
        //            Player drawPlayer = drawInfo.drawPlayer;
        //            if (drawPlayer.whoAmI != Main.myPlayer || !drawPlayer.active || drawPlayer.dead || drawPlayer.ghost)
        //                return;

        //            Texture2D dpad = ModContent.GetTexture("FargowiltasSouls/UI/DPad");
        //            int num156 = dpad.Height / 4; //ypos of lower right corner of sprite to draw
        //            int y3 = num156 * (int)(Main.GlobalTime % 0.5 * 8); //ypos of upper left corner of sprite to draw
        //            Rectangle rectangle = new Rectangle(0, y3, dpad.Width, num156);
        //            Vector2 origin2 = rectangle.Size() / 2f;
        //            Vector2 drawPos = (drawPlayer.gravDir > 0 ? drawPlayer.Bottom : drawPlayer.Top) - Main.screenPosition;
        //            drawPos.Y += 48 * drawPlayer.gravDir;
        //            DrawData data = new DrawData(dpad, drawPos, rectangle, Color.White, drawPlayer.gravDir < 0 ? MathHelper.Pi : 0f, rectangle.Size() / 2, 2.5f, SpriteEffects.None, 0);
        //            Main.PlayerDrawData.Add(data);
        //        });

        //        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        //        {
        //            BlizzardEffect.visible = true;
        //            layers.Add(BlizzardEffect);

        //            if (Mash)
        //            {
        //                MashLayer.visible = true;
        //                layers.Add(MashLayer);
        //            }

        //            if (BetsyDashing || ShellHide || GoldShell) //dont draw Player during betsy dash
        //                while (layers.Count > 0)
        //                    layers.RemoveAt(0);

        //            if (SquirrelMount)
        //            {
        //                foreach (PlayerLayer PlayerLayer in layers)
        //                {
        //                    if (PlayerLayer != PlayerLayer.MountBack && PlayerLayer != PlayerLayer.MountFront && PlayerLayer != PlayerLayer.MiscEffectsFront && PlayerLayer != PlayerLayer.MiscEffectsBack)
        //                    {
        //                        PlayerLayer.visible = false;
        //                    }
        //                }
        //            }
        //        }

        //        private int getHealMultiplier(int heal)
        //        {
        //            float bonus = 0f;

        //            if (Player.GetToggleValue("Valhalla", false))
        //            {
        //                //if (TerrariaSoul) bonus = 1f; else 
        //                if (WillForce || (ValhallaEnchant && WizardEnchant))
        //                    bonus = 1f / 2f;
        //                else if (ValhallaEnchant || (SquireEnchant && WizardEnchant))
        //                    bonus = 1f / 3f;
        //                else if (SquireEnchant)
        //                    bonus = 1f / 4f;
        //            }

        //            heal = (int)(heal * (1 + bonus));

        //            return heal;
        //        }

        //        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        //        {
        //            healValue = getHealMultiplier(healValue);
        //        }

        public void HealPlayer(int amount)
        {
            //amount = getHealMultiplier(amount);

            Player.statLife += amount;
            if (Player.statLife > Player.statLifeMax2)
                Player.statLife = Player.statLifeMax2;
            Player.HealEffect(amount);
        }

        public override void ModifyScreenPosition()
        {
            if (Screenshake > 0)
                Main.screenPosition += Main.rand.NextVector2Circular(7, 7);
        }

        //        public override void clientClone(ModPlayer clientClone)
        //        {
        //            FargoSoulsPlayer modPlayer = clientClone as FargoSoulsPlayer;
        //            modPlayer.Toggler = Toggler;
        //        }

        public void SyncToggle(string key)
        {
            if (!TogglesToSync.ContainsKey(key))
                TogglesToSync.Add(key, Player.GetToggle(key).ToggleBool);
        }

        //        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        //        {
        //            foreach (KeyValuePair<string, bool> toggle in TogglesToSync)
        //            {
        //                ModPacket packet = mod.GetPacket();

        //                packet.Write((byte)80);
        //                packet.Write((byte)Player.whoAmI);
        //                packet.Write(toggle.Key);
        //                packet.Write(toggle.Value);

        //                packet.Send(toWho, fromWho);
        //            }

        //            TogglesToSync.Clear();
        //        }

        //        public override void SendClientChanges(ModPlayer clientPlayer)
        //        {
        //            FargoSoulsPlayer modPlayer = clientPlayer as FargoSoulsPlayer;
        //            if (modPlayer.Toggler.Toggles != Toggler.Toggles)
        //            {
        //                ModPacket packet = mod.GetPacket();
        //                packet.Write((byte)79);
        //                packet.Write((byte)Player.whoAmI);
        //                packet.Write((byte)Toggler.Toggles.Count);

        //                for (int i = 0; i < Toggler.Toggles.Count; i++)
        //                {
        //                    packet.Write(Toggler.Toggles.Values.ElementAt(i).ToggleBool);
        //                }

        //                packet.Send();
        //            }
        //        }

        public void AddBuffNoStack(int buff, int duration)
        {
            if (!Player.HasBuff(buff) && ReallyAwfulDebuffCooldown <= 0)
            {
                Player.AddBuff(buff, duration);
                int d = Player.FindBuffIndex(buff);
                if (d != -1) //if debuff successfully applied
                    ReallyAwfulDebuffCooldown = Player.buffTime[d] + 240;
            }
        }

        public void TryAdditionalAttacks(int damage, DamageClass damageType)
        {
            if (Player.whoAmI != Main.myPlayer)
                return;

            Vector2 position = Player.Center;
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - position);

            if (CactusEnchantActive)
            {
                CactusEnchant.CactusSelfProc(this);
            }

            if (AdditionalAttacks && AdditionalAttacksTimer <= 0)
            {
                AdditionalAttacksTimer = 60;

                if (BorealEnchantActive && Player.GetToggleValue("Boreal"))
                {
                    BorealWoodEnchant.BorealSnowballs(this, damage);
                }

                //if (CelestialRune && Player.GetToggleValue("MasoCelest"))
                //{
                //    if (melee) //fireball
                //    {
                //        SoundEngine.PlaySound(SoundID.Item34, position);
                //        for (int i = 0; i < 3; i++)
                //        {
                //            Projectile.NewProjectile(position, velocity.RotatedByRandom(Math.PI / 6) * Main.rand.NextFloat(6f, 10f),
                //                ModContent.ProjectileType<CelestialRuneFireball>(), (int)(50f * Player.GetDamage(DamageClass.Melee)), 9f, Player.whoAmI);
                //        }
                //    }
                //    if (ranged) //lightning
                //    {
                //        for (int i = -1; i <= 1; i++)
                //        {
                //            float ai1 = Main.rand.Next(100);
                //            Vector2 vel = Vector2.Normalize(velocity.RotatedByRandom(Math.PI / 4)).RotatedBy(MathHelper.ToRadians(5) * i) * 7f;
                //            Projectile.NewProjectile(position, vel, ModContent.ProjectileType<CelestialRuneLightningArc>(),
                //                (int)(50f * Player.GetDamage(DamageClass.Ranged)), 1f, Player.whoAmI, velocity.ToRotation(), ai1);
                //        }
                //    }
                //    if (magic) //ice mist
                //    {
                //        Projectile.NewProjectile(position, velocity * 4.25f, ModContent.ProjectileType<CelestialRuneIceMist>(), (int)(50f * Player.GetDamage(DamageClass.Magic)), 4f, Player.whoAmI);
                //    }
                //    if (minion) //ancient vision
                //    {
                //        Projectile.NewProjectile(position, velocity * 16f, ModContent.ProjectileType<CelestialRuneAncientVision>(), (int)(50f * Player.GetDamage(DamageClass.Summon)), 3f, Player.whoAmI);
                //    }
                //}

                if (PumpkingsCapeItem != null && Player.GetToggleValue("MasoPump"))
                {
                    if (damageType == DamageClass.Melee) //flaming jack
                    {
                        float distance = 2000f;
                        int target = -1;
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].CanBeChasedBy())
                            {
                                float newDist = Main.npc[i].Distance(Player.Center);
                                if (newDist < distance)
                                {
                                    distance = newDist;
                                    target = i;
                                }
                            }
                        }
                        if (target != -1)
                            Projectile.NewProjectile(Player.GetProjectileSource_Accessory(PumpkingsCapeItem), position, velocity * 8f, ProjectileID.FlamingJack, (int)(75f * Player.GetDamage(DamageClass.Melee)), 7.5f, Player.whoAmI, target, 0);
                    }
                    if (damageType == DamageClass.Ranged) //jack o lantern
                    {
                        Projectile.NewProjectile(Player.GetProjectileSource_Accessory(PumpkingsCapeItem), position, velocity * 11f, ProjectileID.JackOLantern, (int)(65f * Player.GetDamage(DamageClass.Ranged)), 8f, Player.whoAmI);
                    }
                    if (damageType == DamageClass.Magic) //bat scepter
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 newVel = velocity * 10f;
                            newVel.X += Main.rand.Next(-35, 36) * 0.02f;
                            newVel.Y += Main.rand.Next(-35, 36) * 0.02f;
                            Projectile.NewProjectile(Player.GetProjectileSource_Accessory(PumpkingsCapeItem), position, newVel, ProjectileID.Bat, (int)(45f * Player.GetDamage(DamageClass.Magic)), 3f, Player.whoAmI);
                        }
                    }
                    if (damageType == DamageClass.Summon)
                    {
                        const int max = 6;
                        for (int i = 0; i < max; i++)
                        {
                            FargoSoulsUtil.NewSummonProjectile(Player.GetProjectileSource_Accessory(PumpkingsCapeItem), position, velocity.RotatedBy(MathHelper.TwoPi / max * i) * 20f, ModContent.ProjectileType<SpookyScythe>(), 50, 2, Player.whoAmI);
                        }
                    }
                }
            }
        }
    }
}
