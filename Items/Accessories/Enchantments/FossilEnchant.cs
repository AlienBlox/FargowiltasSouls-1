using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Toggler;
using FargowiltasSouls.Projectiles.Souls;
using FargowiltasSouls.Buffs.Souls;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class FossilEnchant : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Fossil Enchantment");
            Tooltip.SetDefault(
@"If you reach zero HP you will revive with 50 HP and spawn several bones
You will also spawn a few bones on every hit
Collect the bones to heal for 20 HP each
'Beyond a forgotten age'");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "化石魔石");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese,
@"受到致死伤害时会以1生命值重生并爆出几根骨头
你攻击敌人时也会扔出骨头
每根骨头会回复15点生命值
'被遗忘已久的记忆'");
        }

        public override void SafeModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(140, 92, 59);
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FossilEffect(player);
        }

        public static void FossilEffect(Player player)
        {
            //bone zone
            player.GetModPlayer<FargoSoulsPlayer>().FossilEnchantActive = true;
        }

        public static void FossilHurt(FargoSoulsPlayer modPlayer, int damage)
        {
            Player player = modPlayer.Player;

            player.immune = true;
            player.immuneTime = 60;

            if (player.GetToggleValue("Fossil"))
            {
                //spawn bones
                int damageCopy = damage;
                for (int i = 0; i < 5; i++)
                {
                    if (damageCopy < 30)
                        break;
                    damageCopy -= 30;

                    float velX = Main.rand.Next(-5, 6) * 3f;
                    float velY = Main.rand.Next(-5, 6) * 3f;
                    Projectile.NewProjectile(player.GetProjectileSource_Misc(0), player.position.X + velX, player.position.Y + velY, velX, velY, ModContent.ProjectileType<FossilBone>(), 0, 0f, player.whoAmI);
                }
            }
        }

        public static void FossilRevive(FargoSoulsPlayer modPlayer)
        {
            Player player = modPlayer.Player;

            void Revive(int healAmount, int reviveCooldown)
            {
                player.statLife = healAmount;
                player.HealEffect(healAmount);

                player.immune = true;
                player.immuneTime = 120;
                player.hurtCooldowns[0] = 120;
                player.hurtCooldowns[1] = 120;

                CombatText.NewText(player.Hitbox, Color.SandyBrown, "You've been revived!", true);
                Main.NewText("You've been revived!", Color.SandyBrown);

                player.AddBuff(ModContent.BuffType<FossilReviveCD>(), reviveCooldown);
            };

            //if (Eternity)
            //{
            //    Revive(player.statLifeMax2 / 2 > 200 ? player.statLifeMax2 / 2 : 200, 10800);
            //    FargoSoulsUtil.XWay(30, player.Center, ModContent.ProjectileType<FossilBone>(), 15, 0, 0);
            //}
            //else if (TerrariaSoul)
            //{
            //    Revive(200, 14400);
            //    FargoSoulsUtil.XWay(25, player.Center, ModContent.ProjectileType<FossilBone>(), 15, 0, 0);
            //}
            //else
            //{
                Revive(modPlayer.SpiritForce ? 200 : 50, 18000);
                FargoSoulsUtil.XWay(modPlayer.SpiritForce ? 20 : 10, player.GetProjectileSource_Misc(0), player.Center, ModContent.ProjectileType<FossilBone>(), 15, 0, 0);
            //}
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FossilHelm)
                .AddIngredient(ItemID.FossilShirt)
                .AddIngredient(ItemID.FossilPants)
                .AddIngredient(ItemID.BoneDagger, 100)
                .AddIngredient(ItemID.AmberStaff)
                .AddIngredient(ItemID.AntlionClaw)

                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
