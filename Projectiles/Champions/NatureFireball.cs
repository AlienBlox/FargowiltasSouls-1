using Terraria;
using Terraria.ID;

namespace FargowiltasSouls.Projectiles.Champions
{
    public class NatureFireball : WillFireball
    {
        public override string Texture => "Terraria/Projectile_711";

        public override void SetDefaults()
        {
            base.SetDefaults();
            cooldownSlot = 1;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            base.AI();
            if (!projectile.tileCollide && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                projectile.tileCollide = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (FargoSoulsWorld.MasochistMode)
                target.AddBuff(BuffID.Burning, 300);
            target.AddBuff(BuffID.OnFire, 300);
        }
    }
}