using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
    public class CreeperMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brain Proj");
        }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minionSlots = 1f;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
            projectile.penetrate = -1;
            projectile.minion = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            if (player.dead) modPlayer.BrainMinion = false;
            if (modPlayer.BrainMinion) projectile.timeLeft = 2;

            int Brain = -1;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if(Main.projectile[i].type == mod.ProjectileType("BrainProj") && Main.projectile[i].active && Main.projectile[i].owner == projectile.owner)
                {
                    Brain = i;
                }
            }
            if (Brain == -1)
                projectile.Kill();
            else
            {
                for (int index = 0; index < 1000; ++index)
                {
                    if (index != projectile.whoAmI && Main.projectile[index].active && (Main.projectile[index].owner == projectile.owner && Main.projectile[index].type == projectile.type) && (double)Math.Abs((float)(projectile.position.X - Main.projectile[index].position.X)) + (double)Math.Abs((float)(projectile.position.Y - Main.projectile[index].position.Y)) < (double)projectile.width)
                    {
                        if (projectile.position.X < Main.projectile[index].position.X)
                        {
                            projectile.velocity.X -= 0.2f;
                        }
                        else
                        {
                            projectile.velocity.X += 0.2f;
                        }
                        if (projectile.position.Y < Main.projectile[index].position.Y)
                        {
                            projectile.velocity.Y -= 0.2f;
                        }
                        else
                        {
                            projectile.velocity.Y += 0.2f;
                        }
                    }
                }

                bool targetting = false;
                NPC targetnpc = null;
                NPC minionAttackTargetNpc = projectile.OwnerMinionAttackTargetNPC;
                if (minionAttackTargetNpc != null && minionAttackTargetNpc.CanBeChasedBy((object)this, false))
                {
                    Vector2 distancetotarget = minionAttackTargetNpc.Center - projectile.Center;
                    if (distancetotarget.Length() < 1000)
                    {
                        targetnpc = minionAttackTargetNpc;
                        targetting = true;
                    }
                }
                else if (!targetting)
                {
                    float distancemax = 1000;
                    for (int index = 0; index < 200; ++index)
                    {
                        if (Main.npc[index].CanBeChasedBy((object)this, false))
                        {
                            Vector2 distancetotarget = Main.npc[index].Center - projectile.Center;
                            if (distancetotarget.Length() < distancemax)
                            {
                                distancemax = distancetotarget.Length();
                                targetnpc = Main.npc[index];
                                targetting = true;
                            }
                        }
                    }
                }
                if (!targetting || projectile.ai[0] > 0)
                {
                    float movespeed = Math.Max(projectile.Distance(Main.projectile[Brain].Center) / 40f, 10f);

                    projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(Main.projectile[Brain].Center) * movespeed, 0.04f);
                    if (projectile.Hitbox.Intersects(Main.projectile[Brain].Hitbox))
                    {
                        projectile.ai[0] = 0;
                    }
                }
                if (targetting && projectile.ai[0] == 0)
                {
                    float movespeed = Math.Max(projectile.Distance(targetnpc.Center) / 40f, 14f);

                    projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(targetnpc.Center) * movespeed, 0.05f);
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0]++;
        }
    }
}