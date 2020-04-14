using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using Fargowiltas.Items.Tiles;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Shield)]
    public class ColossusSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Colossus Soul");

            string tooltip =
@"'Nothing can stop you'
Increases HP by 100
15% damage reduction
Increases life regeneration by 5
Grants immunity to knockback and several debuffs
Enemies are more likely to target you
Effects of Brain of Confusion, Star Veil, and Sweetheart Necklace
Effects of Bee Cloak, Spore Sac, Paladin's Shield, and Frozen Turtle Shell";
            string tooltip_ch =
@"'没有什么能阻止你'
增加100最大生命值
增加15%伤害减免
增加5点生命再生
免疫击退和诸多Debuff
敌人更有可能以你为目标
拥有混乱之脑,星辰项链和甜心项链的效果
拥有蜜蜂斗篷,孢子囊,圣骑士护盾和冰霜龟壳的效果";

            if (thorium != null)
            {
                tooltip += "\nEffects of Ocean's Retaliation and Terrarium Defender";
                tooltip_ch += "\n拥有海潮之噬和生存者披风的效果\n拥有爆炸盾和界元之庇护的效果";
            }

            if (calamity != null)
            {
                tooltip += "\nEffects of Rampart of Deities and Asgardian Aegis";
                tooltip_ch += "\n拥有阿斯加德之庇护的效果";
            }

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
            recipe.AddIngredient(ItemID.FleshKnuckles);
            recipe.AddIngredient(ItemID.SporeSac);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("OceanRetaliation"));
                recipe.AddIngredient(thorium.ItemType("TerrariumDefender"));
            }

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(calamity.ItemType("RampartofDeities"));
                recipe.AddIngredient(calamity.ItemType("AsgardianAegis"));
            }
            
            if(!Fargowiltas.Instance.ThoriumLoaded && !Fargowiltas.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(ItemID.FrozenTurtleShell);
                recipe.AddIngredient(ItemID.PaladinsShield);
                recipe.AddIngredient(ItemID.AnkhShield);
            }

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
