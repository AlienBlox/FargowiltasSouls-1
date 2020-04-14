using Terraria;
using Terraria.ModLoader;
using static Terraria.ID.ItemID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Fargowiltas.Items.Tiles;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Back)]
    public class WorldShaperSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("World Shaper Soul");
            Tooltip.SetDefault(
@"'Limitless possibilities'
Increased block and wall placement speed by 50% 
Near infinite block placement and mining reach
Mining speed tripled 
Shows the location of enemies, traps, and treasures
Auto paint and actuator effect 
Provides light and allows gravity control
Grants the ability to enable Builder Mode:
Anything that creates a tile will not be consumed and can be used much faster
No enemies can spawn
Effect can be disabled in Soul Toggles menu
Effects of the Cell Phone and Royal Gel
Summons a pet Magic Lantern");
            DisplayName.AddTranslation(GameCulture.Chinese, "铸世者之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'无限的可能性'
增加50%放置物块及墙壁的速度
近乎无限的放置和采掘距离
四倍采掘速度
显示敌人,陷阱和宝藏
自动喷漆和制动器效果
提供光照和重力控制
获得开启建造模式的能力:
放置方块不会消耗
没有敌人生成
效果可以在灵魂切换菜单中禁用
拥有手机的效果
召唤一个魔法灯笼");
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
                    tooltipLine.overrideColor = new Color?(new Color(255, 239, 2));
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            //cell phone
            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accDreamCatcher = true;
            player.accOreFinder = true;
            player.accStopwatch = true;
            player.accCritterGuide = true;
            player.accJarOfSouls = true;
            player.accThirdEye = true;
            player.accCalendar = true;
            player.accWeatherRadio = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.WorldShaperSoul(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, Fargowiltas.Instance.ThoriumLoaded ? "GeodeEnchant" : "MinerEnchant");
            recipe.AddIngredient(Toolbelt);
            recipe.AddIngredient(Toolbox);
            recipe.AddIngredient(ArchitectGizmoPack);
            recipe.AddIngredient(ActuationAccessory);
            recipe.AddIngredient(LaserRuler);
            recipe.AddIngredient(RoyalGel);
            recipe.AddIngredient(PutridScent);
            recipe.AddIngredient(CellPhone);
            recipe.AddIngredient(GravityGlobe);
            recipe.AddIngredient(BonePickaxe);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyDrax");
            recipe.AddIngredient(ShroomiteDiggingClaw);
            recipe.AddIngredient(DrillContainmentUnit);

            recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
