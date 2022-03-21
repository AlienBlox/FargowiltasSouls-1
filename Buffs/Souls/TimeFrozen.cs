﻿using FargowiltasSouls.NPCs;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
    public class TimeFrozen : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Frozen");
            Description.SetDefault("You are stopped in time");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "时间冻结");
            Description.AddTranslation((int)GameCulture.CultureName.Chinese, "你停止了时间");
        }

        //public override void Update(Player player, ref int buffIndex)
        //{
        //    if (player.mount.Active)
        //        player.mount.Dismount(player);

        //    player.controlLeft = false;
        //    player.controlRight = false;
        //    player.controlJump = false;
        //    player.controlDown = false;
        //    player.controlUseItem = false;
        //    player.controlUseTile = false;
        //    player.controlHook = false;
        //    player.controlMount = false;
        //    player.velocity = player.oldVelocity;
        //    player.position = player.oldPosition;

        //    player.GetModPlayer<FargoSoulsPlayer>().MutantNibble = true; //no heal

        //    FargowiltasSouls.Instance.ManageMusicTimestop(player.buffTime[buffIndex] < 5);

        //    if (Main.netMode != NetmodeID.Server)
        //    {
        //        if (!Filters.Scene["FargowiltasSouls:Invert"].IsActive() && player.buffTime[buffIndex] > 60)
        //        {
        //            Filters.Scene.Activate("FargowiltasSouls:Invert").GetShader().UseTargetPosition(FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<NPCs.Champions.CosmosChampion>()) ? Main.npc[EModeGlobalNPC.championBoss].Center : player.Center);
        //        }
        //    }

        //    if (player.buffTime[buffIndex] == 90)
        //    {
        //        if (!Main.dedServ)
        //            Terraria.Audio.SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(FargowiltasSouls.Instance, "Sounds/ZaWarudoResume").WithVolume(1f).WithPitchVariance(.5f), player.Center);
        //    }
        //}

        //public override void Update(NPC npc, ref int buffIndex)
        //{
        //    npc.GetGlobalNPC<FargoSoulsGlobalNPC>().TimeFrozen = true;
        //}
    }
}