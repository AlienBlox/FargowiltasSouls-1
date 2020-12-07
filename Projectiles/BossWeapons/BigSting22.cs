﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class BigSting22 : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/NPCs/Resprites/NPC_222";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("22");
            Main.projFrames[projectile.type] = Main.npcFrameCount[NPCID.QueenBee];
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 66;
            projectile.height = 66;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 240;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.scale = 0.5f;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            projectile.spriteDirection = -Math.Sign(projectile.velocity.X);
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.spriteDirection > 0)
                projectile.rotation += MathHelper.Pi;

            if (++projectile.frameCounter > 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 4)
                projectile.frame = 0;

            int ai0 = (int)projectile.ai[0];
            if (ai0 > -1 && ai0 < Main.maxNPCs && Main.npc[ai0].active && Main.npc[ai0].CanBeChasedBy())
            {
                if (projectile.Distance(Main.npc[ai0].Center) < Math.Max(Main.npc[ai0].width, Main.npc[ai0].height) / 2)
                {
                    projectile.ai[0] = -1;
                    projectile.netUpdate = true;
                }
                else
                {
                    projectile.velocity = projectile.velocity.Length() * projectile.DirectionTo(Main.npc[ai0].Center);
                }

                projectile.ai[1] = 1;
            }
            else if (projectile.ai[1] == 0 && ++projectile.localAI[0] == 22)
            {
                int possibleTarget = -1;
                float closestDistance = 1220f;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.active && npc.CanBeChasedBy())
                    {
                        float distance = Vector2.Distance(projectile.Center, npc.Center);

                        if (closestDistance > distance)
                        {
                            closestDistance = distance;
                            possibleTarget = i;
                        }
                    }
                }

                projectile.ai[0] = possibleTarget;
                projectile.ai[1] = 1;
                projectile.netUpdate = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = lightColor;
            color26 = projectile.GetAlpha(color26);

            SpriteEffects effects = projectile.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                Color color27 = color26 * 0.5f;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value4 = projectile.oldPos[i];
                float num165 = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture2D13, value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, projectile.scale, effects, 0f);
            }

            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, effects, 0f);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath1, projectile.Center);
            for (int i = 0; i < 20; i++)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 5);
                Main.dust[d].velocity *= 3f;
                Main.dust[d].scale += 0.75f;
            }
        }
    }
}