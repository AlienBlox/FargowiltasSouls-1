﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.AbomBoss
{
    public class AbomRitual : BaseArena
    {
        public override string Texture => "Terraria/Projectile_274";

        public AbomRitual() : base(MathHelper.Pi / 180f, 1400f, ModContent.NPCType<NPCs.AbomBoss.AbomBoss>(), 87) { }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abominationn Seal");
        }

        protected override void Movement(NPC npc)
        {
            if (npc.ai[0] < 9)
            {
                projectile.velocity = npc.Center - projectile.Center;
                if (npc.ai[0] != 8) //snaps directly to abom when preparing for p2 attack
                    projectile.velocity /= 40f;
            }
            else //remains still in higher AIs
            {
                projectile.velocity = Vector2.Zero;
            }
        }

        public override void AI()
        {
            base.AI();
            projectile.rotation += 1f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (FargoSoulsWorld.MasochistMode)
            {
                player.AddBuff(mod.BuffType("AbomFang"), 300);
                player.AddBuff(mod.BuffType("Unstable"), 240);
                player.AddBuff(mod.BuffType("Berserked"), 120);
            }
            player.AddBuff(BuffID.Bleeding, 600);
        }
    }
}