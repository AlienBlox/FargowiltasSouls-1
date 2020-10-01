using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class MutantSlimeBall : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Projectiles/BossWeapons/SlimeBall";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Rain");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            //projectile.aiStyle = 14;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.hostile = true;
            projectile.timeLeft = 90;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 59, projectile.velocity.X * 0.2f,
                  projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void Kill(int timeleft)
        {
            for (int i = 0; i < 20; i++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 59, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 59, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Slimed, 240);
            if (FargoSoulsWorld.MasochistMode)
                target.AddBuff(mod.BuffType("MutantFang"), 180);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}