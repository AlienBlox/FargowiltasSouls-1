﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class RainEnchant : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Rain Enchantment");
            Tooltip.SetDefault(
@"Grants immunity to Wet
Spawns a miniature storm to follow you around
Shooting it will make it grow
At maximum size, attacks will turn into lightning bolts
'Come again some other day'");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "雨云魔石");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, 
@"使你免疫潮湿减益
召唤一个微型风暴跟着你
向其射击会使其变大
尺寸达到最大时攻击会转化为闪电
'改日再来'");
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(255, 236, 0);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoSoulsPlayer>().RainEffect(Item);
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.RainHat)
            .AddIngredient(ItemID.RainCoat)
            .AddIngredient(ItemID.UmbrellaHat)
            //inner tube
            .AddIngredient(ItemID.Umbrella)
            //tragic umbrella
            .AddIngredient(ItemID.NimbusRod)
            .AddIngredient(ItemID.WaterGun)
            //.AddIngredient(ItemID.RainbowBrick, 50);
            //volt bunny pet

            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}
