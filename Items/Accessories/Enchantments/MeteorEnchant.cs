using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MeteorEnchant : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Enchantment");
            
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "陨星魔石");
            
            string tooltip =
@"A meteor shower initiates every few seconds while attacking
'Cosmic power builds your destructive prowess'";
            Tooltip.SetDefault(tooltip);

            string tooltip_ch =
@"攻击时每过几秒便会释放一次流星雨
'宇宙之力构建你的毁灭力量'";
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, tooltip_ch);
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(95, 71, 82);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoSoulsPlayer modPlayer = player.GetModPlayer<FargoSoulsPlayer>();
            modPlayer.MeteorEffect();
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.MeteorHelmet)
            .AddIngredient(ItemID.MeteorSuit)
            .AddIngredient(ItemID.MeteorLeggings)
            //meteor hamaxe
            .AddIngredient(ItemID.SpaceGun)
            //orange zapinator, add recipe
            //.AddIngredient(ItemID.StarCannon); //super star shooter, add recipe
            .AddIngredient(ItemID.MeteorStaff)
            .AddIngredient(ItemID.PlaceAbovetheClouds)
            //harpy pet

            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}
