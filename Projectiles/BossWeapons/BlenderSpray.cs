﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    internal class BlenderSpray : DicerSpray
    {
        public override string Texture => "Terraria/Projectile_484";

        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.timeLeft = 60;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //target.immune[projectile.owner] = 6;
        }
    }
}