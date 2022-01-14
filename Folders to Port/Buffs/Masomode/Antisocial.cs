using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Antisocial : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Antisocial");
            Description.SetDefault("You have no friends");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "反社交");
            Description.AddTranslation((int)GameCulture.CultureName.Chinese, "你没有朋友");

        }

        public override void Update(Player player, ref int buffIndex)
        {
            //disables minions, disables pets, -50% minion dmg
            player.GetModPlayer<FargoSoulsPlayer>().Asocial = true;
        }
    }
}