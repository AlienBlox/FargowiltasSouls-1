﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.DeviBoss
{
    public class DeviEnergyHeart : ModProjectile
    {   
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Heart");
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            cooldownSlot = 1;

            projectile.alpha = 125;
            projectile.timeLeft = 90;
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0] = 1;
                Main.PlaySound(SoundID.Item44, projectile.Center);
            }

            projectile.rotation = projectile.ai[0];

            float speed = projectile.velocity.Length();
            speed += projectile.ai[1];
            projectile.velocity = Vector2.Normalize(projectile.velocity) * speed;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 86, 0f, 0f, 0, default(Color), 2f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 8f;
            }

            if (EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.deviBoss, mod.NPCType("DeviBoss")))
            {
                if (Main.netMode != 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(projectile.Center, Vector2.UnitX.RotatedBy(projectile.rotation + (float)Math.PI / 2 * i),
                            mod.ProjectileType("DeviDeathray"), projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Lovestruck"), 240);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity;
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

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += 2)
            {
                Color color27 = color26;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value4 = projectile.oldPos[i];
                float num165 = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture2D13, value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, projectile.scale, SpriteEffects.None, 0f);
            }

            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}