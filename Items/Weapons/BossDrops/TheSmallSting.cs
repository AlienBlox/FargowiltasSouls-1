﻿using FargowiltasSouls.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class TheSmallSting : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Small Sting");
            Tooltip.SetDefault("Uses darts for ammo" +
                "\n50% chance to not consume ammo" +
                "\nStingers will stick to enemies, hitting the same spot again will deal extra damage" +
                "\n'Repurposed from the abdomen of a defeated foe..'");
        }

        public override void SetDefaults()
        {
            Item.damage = 39;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1.5f;
            Item.value = 50000;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SmallStinger>();
            Item.useAmmo = AmmoID.Dart;
            Item.UseSound = SoundID.Item97;
            Item.shootSpeed = 40f;
            Item.width = 44;
            Item.height = 16;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<SmallStinger>();

            return true;
        }

        // Remove the Crit Chance line because of a custom crit method
        public override void SafeModifyTooltips(List<TooltipLine> tooltips) => tooltips.Remove(tooltips.FirstOrDefault(line => line.Name == "CritChance" && line.mod == "Terraria"));

        //make them hold it different
        public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

        public override bool CanConsumeAmmo(Player player) => Main.rand.NextBool();
    }
}