using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.NPCs.MutantBoss;

namespace FargowiltasSouls.Sky
{
    public class MutantSky : CustomSky
    {
        private bool isActive = false;
        private float intensity = 0f;
        private float lifeIntensity = 0f;
        private int delay = 0;
        private int[] xPos = new int[50];
        private int[] yPos = new int[50];

        public override void Update(GameTime gameTime)
        {
            const float increment = 0.01f;
            if (EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>())
                && (Main.npc[EModeGlobalNPC.mutantBoss].ai[0] < 0 || Main.npc[EModeGlobalNPC.mutantBoss].ai[0] > 10 
                || (Main.npc[EModeGlobalNPC.mutantBoss].ai[0] == 10 && Main.npc[EModeGlobalNPC.mutantBoss].ai[1] > 120)))
            {
                intensity += increment;
                if (intensity > 1f)
                {
                    intensity = 1f;
                }

                if (Main.npc[EModeGlobalNPC.mutantBoss].ai[0] != 10)
                {
                    lifeIntensity = 1f - (float)Main.npc[EModeGlobalNPC.mutantBoss].life / Main.npc[EModeGlobalNPC.mutantBoss].lifeMax;
                    if (!FargoSoulsWorld.MasochistMode)
                    {
                        lifeIntensity -= 0.5f;
                        if (lifeIntensity < 0)
                            lifeIntensity = 0;
                    }
                }
            }
            else
            {
                intensity -= increment;
                lifeIntensity -= increment;
                if (lifeIntensity < 0f)
                    lifeIntensity = 0f;
                if (intensity < 0f)
                {
                    intensity = 0f;
                    lifeIntensity = 0f;
                    delay = 0;
                    Deactivate();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0 && minDepth < 0)
            {
                Color color = /*SoulConfig.Instance.GetValue(SoulConfig.Instance.MutantBackground, false) ? Color.White :*/ new Color(180, 180, 180);

                spriteBatch.Draw(ModContent.GetTexture("FargowiltasSouls/Sky/MutantSky"),
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color * (intensity * 0.5f + lifeIntensity * 0.5f));

                if (--delay < 0)
                {
                    delay = Main.rand.Next(5 + (int)(85f * (1f - lifeIntensity)));
                    for (int i = 0; i < 50; i++) //update positions
                    {
                        xPos[i] = Main.rand.Next(Main.screenWidth);
                        yPos[i] = Main.rand.Next(Main.screenHeight);
                    }
                }

                for (int i = 0; i < 50; i++) //static on screen
                {
                    int width = Main.rand.Next(3, 251);
                    spriteBatch.Draw(ModContent.GetTexture("FargowiltasSouls/Sky/MutantStatic"),
                    new Rectangle(xPos[i] - width / 2, yPos[i], width, 3),
                    color * lifeIntensity * 0.75f);
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - intensity;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            isActive = false;
        }

        public override bool IsActive()
        {
            return isActive;
        }

        public override Color OnTileColor(Color inColor)
        {
            return new Color(Vector4.Lerp(new Vector4(0.6f, 0.9f, 1f, 1f), inColor.ToVector4(), 1f - intensity));
        }
    }
}