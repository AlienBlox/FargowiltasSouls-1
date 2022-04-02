﻿using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.EternityMode.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.EternityMode.Content.Enemy
{
    public class Pigrons : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() =>  new NPCMatcher().MatchTypeRange(
            NPCID.PigronCorruption,
            NPCID.PigronCrimson,
            NPCID.PigronHallow
        );

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            base.OnHitPlayer(npc, target, damage, crit);

            target.AddBuff(ModContent.BuffType<SqueakyToy>(), 120);
            target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 50;
            target.AddBuff(ModContent.BuffType<OceanicMaul>(), 1800);
        }

        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);

            if (Main.netMode != NetmodeID.MultiplayerClient)
                Projectile.NewProjectile(npc.GetSpawnSource_ForProjectile(), npc.Center, Vector2.Zero, ProjectileID.Cthulunado, npc.damage / 4, 0f, Main.myPlayer, 16, 11);
        }
    }
}
