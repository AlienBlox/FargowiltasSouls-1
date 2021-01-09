﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.AbomBoss
{
    public class AbomSickleSplit1 : MutantBoss.MutantScythe1
    {
        public override string Texture => "FargowiltasSouls/Projectiles/AbomBoss/AbomSickle";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abominationn Sickle");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void AI()
        {
            /*NPC mutant = Main.npc[(int)projectile.ai[0]];
            if (!mutant.active || mutant.type != mod.NPCType("MutantBoss"))
            {
                projectile.Kill();
                return;
            }*/
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0] = 1;
                Main.PlaySound(SoundID.Item8, projectile.Center);
            }
            projectile.rotation += 0.8f;
            for (int i = 0; i < 6; i++)
            {
                Vector2 offset = new Vector2(0, -20).RotatedBy(projectile.rotation);
                offset = offset.RotatedByRandom(MathHelper.Pi / 6);
                int d = Dust.NewDust(projectile.Center, 0, 0, 87, 0f, 0f, 150);
                Main.dust[d].position += offset;
                float velrando = Main.rand.Next(20, 31) / 10;
                Main.dust[d].velocity = projectile.velocity / velrando;
                Main.dust[d].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 8; i++)
                    Projectile.NewProjectile(projectile.Center, Vector2.Normalize(projectile.velocity).RotatedBy(Math.PI / 4 * i),
                        ModContent.ProjectileType<AbomSickleSplit2>(), projectile.damage, 0f, projectile.owner);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (FargoSoulsWorld.MasochistMode)
            {
                target.AddBuff(mod.BuffType("AbomFang"), 300);
                target.AddBuff(mod.BuffType("Berserked"), 120);
            }
            target.AddBuff(BuffID.Bleeding, 600);
        }
    }
}