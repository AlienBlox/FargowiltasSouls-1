using FargowiltasSouls.Items.Consumables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items
{
    public class Masochist : SoulsItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant's Gift");
            Tooltip.SetDefault(@"World must be in Expert Mode
Toggles Eternity Mode
Deviantt provides a starter pack and progress-based advice
Cannot be used while a boss is alive
[i:1612][c/00ff00:Recommended to use Fargo's Mutant Mod Debuff Display (in config)]
[c/ff0000:NOT INTENDED FOR USE WITH OTHER CONTENT MODS OR MODDED DIFFICULTIES]");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "突变体的礼物");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "'用开/关受虐模式'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SafeModifyTooltips(List<TooltipLine> tooltips)
        {
            base.SafeModifyTooltips(tooltips);

            TooltipLine line = new TooltipLine(Mod, "tooltip", 
                $"[i:{ModContent.ItemType<MutantsPact>()}]Enables Masochist Mode when used in Master Mode");
            tooltips.Add(line);
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            bool canPlaymaso = FargoSoulsWorld.CanPlayMaso || (Main.LocalPlayer.active && Main.LocalPlayer.GetModPlayer<FargoSoulsPlayer>().Toggler.CanPlayMaso);
            if (canPlaymaso)
            {
                if ((line.mod == "Terraria" && line.Name == "ItemName") || (line.mod == Mod.Name && line.Name == "tooltip"))
                {
                    Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                    var lineshader = GameShaders.Misc["PulseUpwards"].UseColor(new Color(28, 222, 152)).UseSecondaryColor(new Color(168, 245, 228));
                    lineshader.Apply();
                    Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                    Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                    return false;
                }
            }
            return true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)
        {
            if (FargoSoulsUtil.WorldIsExpertOrHarder())
            {
                if (!FargoSoulsUtil.AnyBossAlive())
                {
                    FargoSoulsWorld.ShouldBeEternityMode = !FargoSoulsWorld.ShouldBeEternityMode;

                    if (Main.netMode != NetmodeID.MultiplayerClient && FargoSoulsWorld.ShouldBeEternityMode && !FargoSoulsWorld.spawnedDevi
                        && ModContent.TryFind("Fargowiltas", "Deviantt", out ModNPC deviantt) && !NPC.AnyNPCs(deviantt.Type))
                    {
                        FargoSoulsWorld.spawnedDevi = true;

                        if (ModContent.TryFind("Fargowiltas", "SpawnProj", out ModProjectile spawnProj))
                            Projectile.NewProjectile(player.GetProjectileSource_Item(Item), player.Center - 1000 * Vector2.UnitY, Vector2.Zero, spawnProj.Type, 0, 0, Main.myPlayer, deviantt.Type);

                        FargoSoulsUtil.PrintText("Deviantt has awoken!", new Color(175, 75, 255));
                    }

                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Roar, player.Center, 0);

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.WorldData); //sync world
                }
            }
            else
            {
                FargoSoulsUtil.PrintText("World must be Expert difficulty or harder!", new Color(175, 75, 255));
            }
            return true;
        }
    }
}