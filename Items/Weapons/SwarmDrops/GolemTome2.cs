﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using FargowiltasSouls.Projectiles.BossWeapons;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class GolemTome2 : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("The Landslide");
            Tooltip.SetDefault("'The reward for slaughtering many...'");

            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "山崩 EX");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "'屠戮众多的奖励'");
        }

        public override void SetDefaults()
        {
            Item.damage = 220;
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 25);
            Item.rare = ItemRarityID.Purple;
            Item.mana = 24;
            Item.UseSound = SoundID.Item21;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GolemHeadProj>();
            Item.shootSpeed = 20f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(null, "RockSlide")
            .AddIngredient(null, "AbomEnergy", 10)
            .AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerGolem"))
            .AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"))
            
            .Register();
        }
    }
}