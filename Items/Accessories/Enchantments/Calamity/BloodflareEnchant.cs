﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using System;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class BloodflareEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodflare Enchantment");
            Tooltip.SetDefault(
@"'The souls of the fallen are at your disposal...'
Enemies below 50% life have a chance to drop hearts when struck
Enemies above 50% life have a chance to drop mana stars when struck
Enemies killed during a Blood Moon have a much higher chance to drop Blood Orbs
True melee strikes will heal you
After striking an enemy 15 times with true melee you will enter a blood frenzy for 5 seconds
During this you will gain 25% increased melee damage, critical strike chance, and contact damage is halved
This effect has a 30 second cooldown
Press Y to unleash the lost souls of polterghast to destroy your enemies
This effect has a 30 second cooldown
Ranged weapons have a chance to fire bloodsplosion orbs
Magic weapons will sometimes fire ghostly bolts
Magic critical strikes cause flame explosions every 2 seconds
Summons polterghast mines to circle you
Rogue critical strikes have a 50% chance to heal you
Effects of the Core of the Blood God and Affliction");
            DisplayName.AddTranslation(GameCulture.Chinese, "血炎魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'堕落者的灵魂由你支配...'
攻击50%血以下的敌人时有几率掉落心
攻击50%血以上的敌人时有几率掉落法力星
在血月被杀死的敌人有更高的几率掉落血珠
近战攻击将会治愈你
近战攻击同一敌人15次后, 你将进入5秒的'血之狂乱'状态
'血之狂乱'状态下, 增加25%近战伤害和暴击率, 减免50%接触伤害
该效果冷却时间30秒
按'Y'键释放噬魂幽花的失落之魂摧毁你的敌人
该效果冷却时间30秒
远程武器有概率发射鲜血爆破
魔法武器有时会发射幽灵法球
召唤噬魂幽花雷环绕周围
盗贼暴击有50%治愈玩家
拥有血神核心和灾劫之尖啸的效果");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(0, 255, 0));
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 3000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();

            if (SoulConfig.Instance.GetValue("Bloodflare Effects"))
            {
                modPlayer.bloodflareSet = true;
                modPlayer.bloodflareMelee = true;
                modPlayer.bloodflareRanged = true;
                modPlayer.bloodflareMage = true;
                modPlayer.bloodflareThrowing = true;
            }
           
            if (SoulConfig.Instance.GetValue("Polterghast Mines"))
            {
                modPlayer.bloodflareSummon = true;
            }
            
            //core of the blood god
            modPlayer.coreOfTheBloodGod = true;
            modPlayer.fleshTotem = true;
            //affliction
            modPlayer.affliction = true;
            if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0)
            {
                int myPlayer = Main.myPlayer;
                if (Main.player[myPlayer].team == player.team && player.team != 0)
                {
                    float num = player.position.X - Main.player[myPlayer].position.X;
                    float num2 = player.position.Y - Main.player[myPlayer].position.Y;
                    if ((float)Math.Sqrt((num * num + num2 * num2)) < 2800f)
                    {
                        Main.player[myPlayer].AddBuff(calamity.BuffType("Afflicted"), 20, true);
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyBloodflareHelmet");
            recipe.AddIngredient(calamity.ItemType("BloodflareBodyArmor"));
            recipe.AddIngredient(calamity.ItemType("BloodflareCuisses"));
            recipe.AddIngredient(calamity.ItemType("CoreOfTheBloodGod"));
            recipe.AddIngredient(calamity.ItemType("EldritchSoulArtifact"));
            recipe.AddIngredient(calamity.ItemType("Affliction"));
            recipe.AddIngredient(calamity.ItemType("DevilsSunrise"));
            recipe.AddIngredient(calamity.ItemType("MolecularManipulator"));
            recipe.AddIngredient(calamity.ItemType("AethersWhisper"));
            recipe.AddIngredient(calamity.ItemType("DarkSpark"));
            recipe.AddIngredient(calamity.ItemType("DodusHandcannon"));
            recipe.AddIngredient(calamity.ItemType("TheLastMourning"));
            recipe.AddIngredient(calamity.ItemType("TimeBolt"));
            recipe.AddIngredient(calamity.ItemType("LightGodsBrilliance"));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
