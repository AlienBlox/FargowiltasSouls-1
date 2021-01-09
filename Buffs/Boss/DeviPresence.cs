using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Boss
{
    public class DeviPresence : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Deviant Presence");
            Description.SetDefault("Friendly NPCs take massively increased damage");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>().DevianttPresence = true;
        }
    }
}