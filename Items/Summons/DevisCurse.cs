using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Summons
{
    public class DevisCurse : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deviantt's Curse");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 7));

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        /*public override bool Autoload(ref string name)
        {
            return false;
        }*/

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 999;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 2);
            Item.noUseGraphic = true;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override bool? UseItem(Player player)
        {
            int deviantt = ModContent.TryFind("Fargowiltas", "Deviantt", out ModNPC modNPC) ? NPC.FindFirstNPC(modNPC.Type) : -1;
            if (deviantt > -1 && Main.npc[deviantt].active)
            {
                Main.npc[deviantt].Transform(ModContent.NPCType<NPCs.DeviBoss.DeviBoss>());
                FargoSoulsUtil.PrintText("Deviantt has awoken!", new Color(175, 75, 255));
            }
            else
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.DeviBoss.DeviBoss>());
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Gel)
            .AddIngredient(ItemID.Lens)
            .AddIngredient(ItemID.RottenChunk)
            .AddIngredient(ItemID.Stinger)
            //.AddIngredient(ItemID.Bone);
            .AddIngredient(ItemID.HellstoneBar)
            //.AddIngredient(ModContent.ItemType<CrackedGem>(), 5);
            .AddTile(TileID.DemonAltar)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.Gel)
            .AddIngredient(ItemID.Lens)
            .AddIngredient(ItemID.Vertebrae)
            .AddIngredient(ItemID.Stinger)
            //.AddIngredient(ItemID.Bone);
            .AddIngredient(ItemID.HellstoneBar)
            //.AddIngredient(ModContent.ItemType<CrackedGem>(), 5);
            .AddTile(TileID.DemonAltar)
            .Register();
        }
    }
}