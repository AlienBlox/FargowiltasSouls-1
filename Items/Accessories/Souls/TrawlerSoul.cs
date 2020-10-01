using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Back)]
    public class TrawlerSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trawler Soul"); 

            string tooltip =
@"Increases fishing skill substantially
All fishing rods will have 10 extra lures
You catch fish almost instantly
Permanent Sonar and Crate Buffs
Effects of Angler Tackle Bag
'The fish catch themselves'";
            string tooltip_ch =
@"'让鱼自己抓自己'
极大提升钓鱼能力
所有鱼竿额外增加10个鱼饵
钓鱼线永不破坏
减少鱼饵消耗几率
永久声呐和板条箱Buff";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "捕鱼之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(0, 238, 125));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.TrawlerSoul(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AnglerEnchantment");

            //add lavaproof fishing hook
            //engineer rod
            recipe.AddIngredient(ItemID.SittingDucksFishingRod);
            //hotline fishing
            recipe.AddIngredient(ItemID.GoldenFishingRod);
            recipe.AddIngredient(ItemID.GoldenCarp);
            recipe.AddIngredient(ItemID.ReaverShark);
            recipe.AddIngredient(ItemID.Bladetongue);
            recipe.AddIngredient(ItemID.ObsidianSwordfish);
            recipe.AddIngredient(ItemID.FuzzyCarrot);
            recipe.AddIngredient(ItemID.HardySaddle);
            recipe.AddIngredient(ItemID.ZephyrFish);

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
