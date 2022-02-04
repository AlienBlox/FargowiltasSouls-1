﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PearlwoodEnchant : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pearlwood Enchantment");
            Tooltip.SetDefault(
@"Attacks may spawn a homing star when they hit something
'Too little, too late…'");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "珍珠木魔石");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, 
@"弹幕在击中敌人或物块时有几率生成一颗星星
'既渺小无力，又慢人一步...'");
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(173, 154, 95);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PearlwoodEffect(player);
        }

        public static void PearlwoodEffect(Player player)
        {
            FargoSoulsPlayer modPlayer = player.GetModPlayer<FargoSoulsPlayer>();
            modPlayer.PearlwoodEnchantActive = true;

            if (modPlayer.PearlwoodCD > 0)
                modPlayer.PearlwoodCD--;
        }

        public static void PearlwoodStarDrop(FargoSoulsPlayer modPlayer, NPC target, int damage)
        {
            Player player = modPlayer.Player;
            //holy star spawn code funny
            float x = target.position.X + (float)Main.rand.Next(-400, 400);
            float y = target.position.Y - (float)Main.rand.Next(600, 900);
            Vector2 vector12 = new Vector2(x, y);
            float num483 = target.position.X + (float)(target.width / 2) - vector12.X;
            float num484 = target.position.Y + (float)(target.height / 2) - vector12.Y;
            int num485 = 22;
            float num486 = (float)Math.Sqrt((double)(num483 * num483 + num484 * num484));
            num486 = (float)num485 / num486;
            num483 *= num486;
            num484 *= num486;
            int num487 = damage;
            int num488 = Projectile.NewProjectile(player.GetProjectileSource_Misc(0), x, y, num483, num484, ProjectileID.HallowBossRainbowStreak, num487, 0, player.whoAmI, 0f, 0);
            if (num488 != 1000)
            {
                Main.projectile[num488].hostile = false;
                Main.projectile[num488].friendly = true;
            } 

            modPlayer.PearlwoodCD = (modPlayer.WoodForce) ? 15 : 30;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.PearlwoodHelmet)
            .AddIngredient(ItemID.PearlwoodBreastplate)
            .AddIngredient(ItemID.PearlwoodGreaves)
            .AddIngredient(ItemID.PearlwoodSword)
            .AddIngredient(ItemID.LightningBug)
            .AddIngredient(ItemID.Starfruit)

            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}
