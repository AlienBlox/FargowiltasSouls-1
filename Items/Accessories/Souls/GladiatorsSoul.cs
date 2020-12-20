using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.ID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Waist)]
    public class GladiatorsSoul : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker's Soul");

            string tooltip =
@"30% increased melee damage
20% increased melee speed
15% increased melee crit chance
Increased melee knockback
Effects of the Fire Gauntlet and Yoyo Bag
'None shall live to tell the tale'";
            string tooltip_ch =
@"'不留活口'
增加30%近战伤害
增加30%近战速度
增加15%近战暴击率
增加近战击退";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "狂战士之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(255, 111, 6));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += .3f;
            player.meleeSpeed += .2f;
            player.meleeCrit += 15;

            //gauntlet
            if (SoulConfig.Instance.MagmaStone)
            {
                player.magmaStone = true;
            }

            player.kbGlove = true;

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.YoyoBag))
            {
                player.counterWeight = 556 + Main.rand.Next(6);
                player.yoyoGlove = true;
                player.yoyoString = true;
            }

            //berserker glove effect, auto swing thing
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "BarbariansEssence");
            recipe.AddIngredient(ItemID.FireGauntlet);
            //berserkers glove
            recipe.AddIngredient(ItemID.YoyoBag);
            recipe.AddIngredient(ItemID.KOCannon);
            recipe.AddIngredient(ItemID.IceSickle);
            //drippler crippler
            //
            recipe.AddIngredient(ItemID.ScourgeoftheCorruptor);
            recipe.AddIngredient(ItemID.Kraken);
            recipe.AddIngredient(ItemID.Flairon);
            recipe.AddIngredient(ItemID.MonkStaffT3);
            recipe.AddIngredient(ItemID.NorthPole);
            //zenith

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}