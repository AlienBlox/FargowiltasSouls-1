using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Shield)]
    public class ColossusSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Colossus Soul");

            string tooltip =
@"Increases HP by 100
15% damage reduction
Increases life regeneration by 5
Grants immunity to knockback and several debuffs
Enemies are more likely to target you
Effects of Brain of Confusion, Star Veil, and Sweetheart Necklace
Effects of Bee Cloak, Spore Sac, Paladin's Shield, and Frozen Turtle Shell
'Nothing can stop you'";
            string tooltip_ch =
@"'没有什么能阻止你'
增加100最大生命值
增加15%伤害减免
增加5点生命再生
免疫击退和诸多Debuff
敌人更有可能以你为目标
拥有混乱之脑,星辰项链和甜心项链的效果
拥有蜜蜂斗篷,孢子囊,圣骑士护盾和冰霜龟壳的效果";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "巨像之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.defense = 10;
            item.value = 1000000;
            item.rare = 11;
            item.shieldSlot = 4;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(252, 59, 0));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //any new effects, brain of confusion
            modPlayer.ColossusSoul(100, 0.15f, 5, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.HandWarmer);
            recipe.AddIngredient(ItemID.WormScarf);
            recipe.AddIngredient(ItemID.BrainOfConfusion);
            recipe.AddIngredient(ItemID.PocketMirror);
            recipe.AddIngredient(ItemID.CharmofMyths);
            recipe.AddIngredient(ItemID.BeeCloak);
            recipe.AddIngredient(ItemID.SweetheartNecklace);
            recipe.AddIngredient(ItemID.StarVeil);
            recipe.AddIngredient(ItemID.FleshKnuckles); //hero shield
            recipe.AddIngredient(ItemID.SporeSac);


            recipe.AddIngredient(ItemID.FrozenTurtleShell); //frozen shield
            recipe.AddIngredient(ItemID.PaladinsShield);
            recipe.AddIngredient(ItemID.AnkhShield);

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
