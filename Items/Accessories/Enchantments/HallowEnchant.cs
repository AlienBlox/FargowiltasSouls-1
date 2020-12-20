using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class HallowEnchant : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Enchantment");

            Tooltip.SetDefault(
@"You gain a shield that can reflect projectiles
Summons an Enchanted Sword familiar that scales with minion damage
Summons a magical fairy
'Hallowed be your sword and shield'");
            DisplayName.AddTranslation(GameCulture.Chinese, "神圣魔石");
            Tooltip.AddTranslation(GameCulture.Chinese,
@"'愿人都尊你的剑与盾为圣'
获得一个可以反射抛射物的护盾
召唤一柄附魔剑
召唤魔法妖精");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(150, 133, 100);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = ItemRarityID.LightPurple;
            item.value = 180000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().HallowEffect(hideVisual); //new effect
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyHallowHead"); //add summon helm here
            recipe.AddIngredient(ItemID.HallowedPlateMail);
            recipe.AddIngredient(ItemID.HallowedGreaves);
            recipe.AddIngredient(ModContent.ItemType<SilverEnchant>());
            recipe.AddIngredient(ItemID.DyeTradersScimitar);
            recipe.AddIngredient(ItemID.Gungnir);
            //hallow lance
            //hallowed repeater
            //any caught fairy
            //any horse mount
            recipe.AddIngredient(ItemID.FairyBell);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}