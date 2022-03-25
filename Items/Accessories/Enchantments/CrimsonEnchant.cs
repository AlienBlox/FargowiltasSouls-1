using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class CrimsonEnchant : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Enchantment");
            Tooltip.SetDefault(
@"After taking a hit, regen is greatly increased until the half the hit is healed off
If you take another hit before it's healed, you lose the heal in addition to normal damage
'The blood of your enemy is your rebirth'");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "猩红魔石");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, 
@"在你受到伤害后大幅增加你的生命恢复速度，直至你恢复的生命量等同于这次受到的伤害量的一半
如果你在恢复前再次受伤则不会触发增加生命恢复的效果
'你从敌人的血中重生'");
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(200, 54, 75);
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
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoSoulsPlayer>().CrimsonEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimsonHelmet)
            .AddIngredient(ItemID.CrimsonScalemail)
            .AddIngredient(ItemID.CrimsonGreaves)
            //blood axe tging
            .AddIngredient(ItemID.TheUndertaker)
            .AddIngredient(ItemID.TheMeatball)
            .AddIngredient(ItemID.CrimsonHeart)

            //blood rain bow
            //flesh catcher rod
            //.AddIngredient(ItemID.BoneRattle);
            //.AddIngredient(ItemID.CrimsonHeart);

            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}
