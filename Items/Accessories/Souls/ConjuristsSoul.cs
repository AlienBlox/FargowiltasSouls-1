using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ConjuristsSoul : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conjurist's Soul");
            
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "召唤之魂");
            
            string tooltip =
@"30% increased summon damage
Increases your max number of minions by 3
Increases your max number of sentries by 3
Increased minion knockback
'An army at your disposal'";
            Tooltip.SetDefault(tooltip);

            string tooltip_ch =
@"增加30%召唤伤害
+4最大召唤栏
+2最大哨兵栏
增加召唤物击退
'一支听命于您的军队'";
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, tooltip_ch);

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Purple;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(0, 255, 255));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 0.3f;
            player.maxMinions += 3;
            player.maxTurrets += 3;
            player.minionKB += 3f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(null, "OccultistsEssence")
            .AddIngredient(ItemID.MonkBelt)
            .AddIngredient(ItemID.SquireShield)
            .AddIngredient(ItemID.HuntressBuckler)
            .AddIngredient(ItemID.ApprenticeScarf)
            .AddIngredient(ItemID.PygmyNecklace)
            .AddIngredient(ItemID.PapyrusScarab)

            
            .AddIngredient(ItemID.Smolstar) //blade staff
            .AddIngredient(ItemID.PirateStaff)
            .AddIngredient(ItemID.OpticStaff)
            .AddIngredient(ItemID.DeadlySphereStaff)
            .AddIngredient(ItemID.StormTigerStaff)
            .AddIngredient(ItemID.StaffoftheFrostHydra)
            //mourningstar?
            //recipe.AddIngredient(ItemID.DD2BallistraTowerT3Popper);
            //recipe.AddIngredient(ItemID.DD2ExplosiveTrapT3Popper);
            //recipe.AddIngredient(ItemID.DD2FlameburstTowerT3Popper);
            //recipe.AddIngredient(ItemID.DD2LightningAuraT3Popper);
            .AddIngredient(ItemID.TempestStaff)
            .AddIngredient(ItemID.RavenStaff)
            .AddIngredient(ItemID.XenoStaff)

            //.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"))
            .Register();
            

        }
    }
}
