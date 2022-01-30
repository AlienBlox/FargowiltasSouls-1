using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class IvyVenom : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ivy Venom");
            Description.SetDefault("Losing life, will become Infested EX at 20 seconds");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "常春藤毒");
            Description.AddTranslation((int)GameCulture.CultureName.Chinese, "流失生命, 持续时间超过20秒时变为感染");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] > 1200)
            {
                player.AddBuff(ModContent.BuffType<InfestedEX>(), player.buffTime[buffIndex]);
                player.buffTime[buffIndex] = 1;
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Roar, (int)player.Center.X, (int)player.Center.Y, 0);
                Main.NewText("Your Ivy Venom has become an Infestation!", 175, 75, 255);
            }
            player.venom = true;
        }
    }
}