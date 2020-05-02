using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs.Champions;

namespace FargowiltasSouls.Items.Summons
{
    public class SigilOfChampions : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sigil of Champions");
            Tooltip.SetDefault(@"Summons the Champions
Summons vary depending on time and biome
Right click to check for possible summons
Not consumed on use");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 11;
            item.maxStack = 1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = false;
            item.value = Item.buyPrice(1);
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < Main.maxNPCs; i++) //no using during another champ fight
            {
                if (Main.npc[i].active && i == NPCs.EModeGlobalNPC.championBoss)
                    return false;
            }
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            Color color = new Color(175, 75, 255);

            if (player.ZoneUndergroundDesert)
            {
                if (player.altFunctionUse == 2)
                    Main.NewText("A strong spirit stirs...", color);
                else
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<SpiritChampion>());
            }
            else if (player.ZoneUnderworldHeight)
            {
                if (player.altFunctionUse == 2)
                    Main.NewText("The core of the planet rumbles...", color);
                else
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<EarthChampion>());
            }
            else if (player.Center.Y >= Main.worldSurface * 16) //is underground
            {
                if (player.ZoneSnow)
                {
                    if (player.altFunctionUse == 2)
                        Main.NewText("A verdant wind is blowing...", color);
                    else
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NatureChampion>());
                }
                else
                {
                    if (player.altFunctionUse == 2)
                        Main.NewText("The stones tremble around you...", color);
                    else
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<TerraChampion>());
                }
            }
            else //above ground
            {
                /*if (player.ZoneSkyHeight)
                {
                    if (player.altFunctionUse == 2)
                        Main.NewText("The stars are aligning...", color);
                    else
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<CosmosChampion>());
                }
                else*/ if (player.ZoneBeach)
                {
                    if (player.altFunctionUse == 2)
                        Main.NewText("Metallic groans echo from the depths...", color);
                    else
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<WillChampion>());
                }
                else if (player.ZoneHoly)
                {
                    if (player.altFunctionUse == 2)
                        Main.NewText("A wave of warmth passes over you...", color);
                    else
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<LifeChampion>());
                }
                else if (!Main.dayTime) //night
                {
                    if (player.altFunctionUse == 2)
                        Main.NewText("The darkness of the night feels deeper...", color);
                    else
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<ShadowChampion>());
                }
                else if (!player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneDesert && !player.ZoneSnow && !player.ZoneJungle) //purity daytime
                {
                    if (player.altFunctionUse == 2)
                        Main.NewText("You are surrounded by the rustling of trees...", color);
                    else
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<TimberChampion>());
                }
                else //nothing to summon
                {
                    if (player.altFunctionUse == 2)
                        Main.NewText("Nothing seems to answer the call...", color);
                }
            }

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Main.DiscoColor;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Acorn, 5);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ItemID.FrostCore, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.DesertFossil, 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ItemID.LunarBar, 5);

            //recipe.AddTile(ModLoader.GetMod("Fargowiltas").TileType("CrucibleCosmosSheet"));
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
