using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using FargowiltasSouls.EternityMode;
using FargowiltasSouls.EternityMode.Content.Boss.PHM;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Buffs.Masomode;

namespace FargowiltasSouls.NPCs.EternityMode
{
    public class RoyalSubject : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_222";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Subject");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.QueenBee];
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "皇家工蜂");
        }

        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 66;
            NPC.aiStyle = 43;
            AIType = NPCID.QueenBee;
            NPC.damage = 25;
            NPC.defense = 8;
            NPC.lifeMax = 750;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.timeLeft = NPC.activeTime * 30;
            NPC.npcSlots = 7f;
            NPC.scale = 0.5f;
            NPC.buffImmune[BuffID.Poisoned] = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.7 * System.Math.Max(1.0, bossLifeScale / 2));
            NPC.damage = (int)(NPC.damage * 0.9);
        }

        public override void AI()
        {
            if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.beeBoss, NPCID.QueenBee)
                && !NPC.AnyNPCs(NPCID.QueenBee))
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.checkDead();
                return;
            }

            //tries to stinger, force into dash
            if (NPC.ai[0] != 0)
            {
                NPC.ai[0] = 0f;
                NPC.netUpdate = true;
            }

            if (NPC.ai[1] != 2f && NPC.ai[1] != 3f)
            {
                NPC.ai[1] = 2f;
                NPC.netUpdate = true;
            }

            NPC.position -= NPC.velocity / 3;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, Main.rand.Next(60, 180));
            target.AddBuff(ModContent.BuffType<Infested>(), 300);
            target.AddBuff(ModContent.BuffType<Swarming>(), 600);
        }

        public override bool CheckDead()
        {
            NPC queenBee = FargoSoulsUtil.NPCExists(EModeGlobalNPC.beeBoss, NPCID.QueenBee);
            if (queenBee != null && Main.netMode != NetmodeID.MultiplayerClient
                && queenBee.GetEModeNPCMod<QueenBee>().BeeSwarmTimer < 600) //dont change qb ai during bee swarm attack
            {
                queenBee.ai[0] = 0f;
                queenBee.ai[1] = 4f; //trigger dashes, but skip the first one
                queenBee.ai[2] = -44f;
                queenBee.ai[3] = 0f;
                queenBee.netUpdate = true;
            }

            Terraria.Audio.SoundEngine.PlaySound(NPC.DeathSound, NPC.Center);
            NPC.active = false;

            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                //SoundEngine.PlaySound(NPC.DeathSound, NPC.Center);
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 5);
                    Main.dust[d].velocity *= 3f;
                    Main.dust[d].scale += 0.75f;
                }

                for (int i = 303; i <= 308; i++)
                    Gore.NewGore(NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height)), NPC.velocity / 2, i, NPC.scale);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.localAI[0] == 1)
            {
                if (NPC.frameCounter > 4)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                }
                if (NPC.frame.Y >= 4 * frameHeight)
                    NPC.frame.Y = 0;
            }
            else
            {
                if (NPC.frameCounter > 4)
                {
                    NPC.frame.Y += frameHeight;
                    NPC.frameCounter = 0;
                }
                if (NPC.frame.Y < 4 * frameHeight)
                    NPC.frame.Y = 4 * frameHeight;
                if (NPC.frame.Y >= 12 * frameHeight)
                    NPC.frame.Y = 4 * frameHeight;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!Terraria.GameContent.TextureAssets.Npc[NPCID.QueenBee].IsLoaded)
                return false;

            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Npc[NPCID.QueenBee].Value;
            Rectangle rectangle = NPC.frame;
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = drawColor;
            color26 = NPC.GetAlpha(color26);

            SpriteEffects effects = NPC.spriteDirection < 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.EntitySpriteDraw(texture2D13, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, NPC.rotation, origin2, NPC.scale, effects, 0);
            return false;
        }
    }
}