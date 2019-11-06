using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class JungleEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Enchantment");

            string tooltip =
@"'The wrath of the jungle dwells within'
Taking damage will release a lingering spore explosion
Spore damage scales with magic damage
All herb collection is doubled
";
            string tooltip_ch =
@"'丛林之怒深藏其中'
受到伤害会释放出有毒的孢子爆炸
所有草药收获翻倍";

            if(thorium != null)
            {
                tooltip += "Effects of Toxic Subwoofer";
                tooltip_ch += "拥有剧毒音响的效果";
            }
            else
            {
                tooltip += "Effects of Guide to Plant Fiber Cordage";
                tooltip_ch += "拥有植物纤维绳索指南的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "丛林魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(113, 151, 31);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().JungleEffect();

            /*if (player.jump)
            {

            }*/

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            /*ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerPoison = true;
                }
            }*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleHat);
            recipe.AddIngredient(ItemID.JungleShirt);
            recipe.AddIngredient(ItemID.JunglePants);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("PoisonSubwoofer"));
                recipe.AddIngredient(ItemID.JungleRose);
                recipe.AddIngredient(ItemID.ThornChakram);
                recipe.AddIngredient(ItemID.Boomstick);
                recipe.AddIngredient(ItemID.PoisonedKnife, 300);
                recipe.AddIngredient(thorium.ItemType("WeirdMud"));
                recipe.AddIngredient(ItemID.Buggy);
            }
            else
            {
                recipe.AddIngredient(ItemID.CordageGuide);
                recipe.AddIngredient(ItemID.JungleRose);
                recipe.AddIngredient(ItemID.ThornChakram);
                recipe.AddIngredient(ItemID.Buggy);
            }
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
