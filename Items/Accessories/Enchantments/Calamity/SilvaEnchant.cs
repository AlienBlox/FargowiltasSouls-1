﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using System;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class SilvaEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public int dragonTimer = 60;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silva Enchantment");
            Tooltip.SetDefault(
@"'Boundless life energy cascades from you...'
You are immune to almost all debuffs
Reduces all damage taken by 5%, this is calculated separately from damage reduction
All projectiles spawn healing leaf orbs on enemy hits
Max run speed and acceleration boosted by 5%
If you are reduced to 0 HP you will not die from any further damage for 10 seconds
If you get reduced to 0 HP again while this effect is active you will lose 100 max life
This effect only triggers once per life
Your max life will return to normal if you die
True melee strikes have a 25% chance to do five times damage
Melee projectiles have a 25% chance to stun enemies for a very brief moment
Increases your rate of fire with all ranged weapons
Magic projectiles have a 10% chance to cause a massive explosion on enemy hits
Summons an ancient leaf prism to blast your enemies with life energy
Rogue weapons have a faster throwing rate while you are above 90% life
Effects of the The Amalgam, Godly Soul Artifact, and Yharim's Gift
Summons an Akato and Fox pet");
            DisplayName.AddTranslation(GameCulture.Chinese, "始源林海魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你身上流淌出无尽的生命能量'
免疫几乎所有Debuff
减少5%所有所受伤害, 该效果独立于伤害减免单独计算
所有抛射物击中敌人时生成治愈之叶
增加5%最大奔跑速度和加速度
生命值减为0时, 10秒内避免死亡并免疫所有伤害
此效果激活期间, 生命值又跌为0时, 失去100最大生命值
此效果每条命仅能触发一次
死亡后最大生命值复原
纯近战暴击有25%概率造成5倍伤害
增加所有远程武器攻速
魔法抛射物击中敌人时有10%概率造成大爆炸
召唤一个远古叶晶用生命之力轰炸敌人
生命值高于90%时, 提高盗贼武器攻速
拥有聚合之脑, 圣魂神物和魔君的礼物的效果");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(108, 45, 199));
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 20000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();

            if (SoulConfig.Instance.GetValue("Silva Effects"))
            {
                modPlayer.silvaSet = true;
                //melee
                modPlayer.silvaMelee = true;
                //range
                modPlayer.silvaRanged = true;
                //magic
                modPlayer.silvaMage = true;
                //throw
                modPlayer.silvaThrowing = true;
            }

            if (SoulConfig.Instance.GetValue("Silva Crystal Minion"))
            {
                //summon
                modPlayer.silvaSummon = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("SilvaCrystal")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("SilvaCrystal"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("SilvaCrystal")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("SilvaCrystal"), (int)(1500.0 * (double)player.minionDamage), 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            //THE AMALGAM
            modPlayer.aBrain = true;
            if (SoulConfig.Instance.GetValue("Fungal Clump Minion"))
            {
                modPlayer.fungalClump = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("FungalClump")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("FungalClump"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("FungalClump")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("FungalClump"), 250, 1f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            if (SoulConfig.Instance.GetValue("Godly Soul Artifact"))
            {
                //godly soul artifact
                modPlayer.gArtifact = true;
            }

            if (SoulConfig.Instance.GetValue("Yharim's Gift"))
            {
                //yharims gift
                if (player.velocity.X > 0.0 || player.velocity.Y > 0.0 || player.velocity.X < -0.1 || player.velocity.Y < -0.1)
                {
                    dragonTimer--;
                    if (dragonTimer <= 0)
                    {
                        if (player.whoAmI == Main.myPlayer)
                        {
                            int num = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, calamity.ProjectileType("DragonDust"), 350, 5f, player.whoAmI, 0f, 0f);
                            Main.projectile[num].timeLeft = 60;
                        }
                        dragonTimer = 60;
                    }
                }
                else
                {
                    dragonTimer = 60;
                }
                if (player.immune && Main.rand.Next(8) == 0 && player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        float num2 = player.position.X + Main.rand.Next(-400, 400);
                        float num3 = player.position.Y - Main.rand.Next(500, 800);
                        Vector2 vector = new Vector2(num2, num3);
                        float num4 = player.position.X + (player.width / 2) - vector.X;
                        float num5 = player.position.Y + (player.height / 2) - vector.Y;
                        num4 += Main.rand.Next(-100, 101);
                        int num6 = 22;
                        float num7 = (float)Math.Sqrt((num4 * num4 + num5 * num5));
                        num7 = num6 / num7;
                        num4 *= num7;
                        num5 *= num7;
                        int num8 = Projectile.NewProjectile(num2, num3, num4, num5, calamity.ProjectileType("SkyFlareFriendly"), 750, 9f, player.whoAmI, 0f, 0f);
                        Main.projectile[num8].ai[1] = player.position.Y;
                        Main.projectile[num8].hostile = false;
                        Main.projectile[num8].friendly = true;
                    }
                }
            }

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.SilvaEnchant = true;
            fargoPlayer.AddPet("Akato Pet", hideVisual, calamity.BuffType("AkatoYharonBuff"), calamity.ProjectileType("Akato"));
            fargoPlayer.AddPet("Fox Pet", hideVisual, calamity.BuffType("Fox"), calamity.ProjectileType("Fox"));
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnySilvaHelmet");
            recipe.AddIngredient(calamity.ItemType("SilvaArmor"));
            recipe.AddIngredient(calamity.ItemType("SilvaLeggings"));
            recipe.AddIngredient(calamity.ItemType("TheAmalgam"));
            recipe.AddIngredient(calamity.ItemType("GodlySoulArtifact"));
            recipe.AddIngredient(calamity.ItemType("YharimsGift"));
            recipe.AddIngredient(calamity.ItemType("AlphaRay"));
            recipe.AddIngredient(calamity.ItemType("ScourgeoftheCosmos"));
            recipe.AddIngredient(calamity.ItemType("Swordsplosion"));
            recipe.AddIngredient(calamity.ItemType("Climax"));
            recipe.AddIngredient(calamity.ItemType("VoidVortex"));
            recipe.AddIngredient(calamity.ItemType("YharimsCrystal"));
            recipe.AddIngredient(calamity.ItemType("ForgottenDragonEgg"));
            recipe.AddIngredient(calamity.ItemType("FoxDrive"));

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
