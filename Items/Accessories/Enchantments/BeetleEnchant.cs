using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class BeetleEnchant : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Enchantment");
            
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "甲虫魔石");
            
            string tooltip =
@"Beetles protect you from damage, up to 15% damage reduction only
Increases flight time by 25%
'The unseen life of dung courses through your veins'";
            Tooltip.SetDefault(tooltip);

            string tooltip_ch =
@"甲虫会保护你，减免下次受到的伤害，至多减免15%下次受到的伤害
延长25%飞行时间
'你的血管里流淌着看不见的粪便生命'";
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, tooltip_ch);
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(109, 92, 133);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoSoulsPlayer modPlayer = player.GetModPlayer<FargoSoulsPlayer>();
            //defense beetle bois
            modPlayer.BeetleEffect();
            modPlayer.WingTimeModifier += .25f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BeetleHelmet)
            .AddRecipeGroup("FargowiltasSouls:AnyBeetle")
            .AddIngredient(ItemID.BeetleLeggings)
            .AddIngredient(ItemID.BeetleWings)
            .AddIngredient(ItemID.BeeWings)
            .AddIngredient(ItemID.ButterflyWings)
            //.AddIngredient(ItemID.MothronWings);
            //breaker blade
            //amarok
            //beetle minecart

            .AddTile(TileID.CrystalBall)
            .Register();
        }
    }
}
