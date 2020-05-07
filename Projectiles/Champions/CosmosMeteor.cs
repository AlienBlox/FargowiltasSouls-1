using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.Champions
{
    public class CosmosMeteor : ModProjectile
    {
        private bool spawned;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Meteor");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Meteor1);
            aiType = ProjectileID.Meteor1;
            
            projectile.magic = false;
            projectile.friendly = false;
            projectile.hostile = true;
            
            cooldownSlot = 1;
        }

        public override void AI()
        {
            if (!spawned)
            {
                spawned = true;
                projectile.frame = Main.rand.Next(3);
            }
        }

        public override void Kill(int timeLeft) //vanilla explosion code echhhhhhhhhhh
        {
            Main.PlaySound(SoundID.Item89, projectile.position);
            projectile.position.X += (float)(projectile.width / 2);
            projectile.position.Y += (float)(projectile.height / 2);
            projectile.width = (int)(128.0 * (double)projectile.scale);
            projectile.height = (int)(128.0 * (double)projectile.scale);
            projectile.position.X -= (float)(projectile.width / 2);
            projectile.position.Y -= (float)(projectile.height / 2);
            for (int index = 0; index < 8; ++index)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
            for (int index1 = 0; index1 < 32; ++index1)
            {
                int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
                Main.dust[index2].noGravity = true;
                Dust dust1 = Main.dust[index2];
                dust1.velocity = dust1.velocity * 3f;
                int index3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Dust dust2 = Main.dust[index3];
                dust2.velocity = dust2.velocity * 2f;
                Main.dust[index3].noGravity = true;
            }
            for (int index1 = 0; index1 < 2; ++index1)
            {
                int index2 = Gore.NewGore(projectile.position + new Vector2((float)(projectile.width * Main.rand.Next(100)) / 100f, (float)(projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, new Vector2(), Main.rand.Next(61, 64), 1f);
                Gore gore = Main.gore[index2];
                gore.velocity = gore.velocity * 0.3f;
                Main.gore[index2].velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
                Main.gore[index2].velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
            }
            
            for (int index1 = 0; index1 < 5; ++index1)
            {
                int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, Utils.SelectRandom<int>(Main.rand, new int[3] { 6, 259, 158 }), 2.5f * (float)projectile.direction, -2.5f, 0, new Color(), 1f);
                Main.dust[index2].alpha = 200;
                Dust dust1 = Main.dust[index2];
                dust1.velocity = dust1.velocity * 2.4f;
                Dust dust2 = Main.dust[index2];
                dust2.scale = dust2.scale + Main.rand.NextFloat();
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (FargoSoulsWorld.MasochistMode)
                target.AddBuff(BuffID.BrokenArmor, 300);
            target.AddBuff(BuffID.OnFire, 300);
            projectile.timeLeft = 0;
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
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}