﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class ShadowForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Force");

            string tooltip = @"'Dark, Darker, Yet Darker'
Your attacks may inflict Darkness on enemies
Darkened enemies occasionally fire shadowflame tentacles at other enemies
A Dungeon Guardian will occasionally annihilate a foe when struck
All of your minions may occasionally spew massive scythes everywhere
Throw a smoke bomb to teleport to it and gain the First Strike Buff
Using the Rod of Discord will also grant this buff
Dash into any walls, to teleport through them to the next opening
While attacking, Flameburst shots manifest themselves from your shadows
Greatly enhances Flameburst and Lightning Aura effectiveness
";

            string tooltip_ch =@"'Dark, Darker, Yet Darker'
攻击概率造成黑暗
陷入黑暗的敌人偶尔会向其他敌人发射暗影烈焰触手
地牢守卫者偶尔会在你受到攻击时消灭敌人
所有召唤物偶尔会发射巨大镰刀
投掷烟雾弹进行传送,并获得先发制人Buff
使用裂位法杖也会获得该Buff
冲进墙壁时,会直接穿过
攻击时,焰爆炮塔的射击会从你的阴影中显现出来
大大增强焰爆炮塔和闪电光环能力
";

            if (thorium != null)
            {
                tooltip +=
@"50% of the damage you take is staggered over the next 10 seconds
";
                tooltip_ch +=
@"所受伤害的50%将被分摊到接下来的10秒内
";
            }

            tooltip +=
@"Effects of Master Ninja Gear
Summons several pets";
            tooltip_ch +=
@"拥有忍者极意的效果
召唤数个宠物";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "暗影之力");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //warlock, shade, plague accessory effect for all
            modPlayer.ShadowForce = true;
            //shoot from where you were meme, pet
            modPlayer.DarkArtistEffect(hideVisual);
            //DG meme, pet
            modPlayer.NecroEffect(hideVisual);
            //darkness debuff, pets
            modPlayer.ShadowEffect(hideVisual);
            //ninja gear
            player.blackBelt = true;
            player.spikedBoots = 2;
            if (SoulConfig.Instance.GetValue("Tabi Dash", false))
                player.dash = 1;
            //tele thru walls, pet
            modPlayer.ShinobiEffect(hideVisual);
            //smoke bomb nonsense, pet
            modPlayer.NinjaEffect(hideVisual);
            //scythe doom, pets
            modPlayer.SpookyEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //shade
            thoriumPlayer.shadeSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "ShadowEnchant");
            recipe.AddIngredient(null, "NecroEnchant");
            recipe.AddIngredient(null, "SpookyEnchant");
            recipe.AddIngredient(null, "ShinobiEnchant");
            recipe.AddIngredient(null, "DarkArtistEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}