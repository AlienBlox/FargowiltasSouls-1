﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.EternityMode
{
    public class NewEModeGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public List<EModeNPCBehaviour> EModeNpcBehaviours = new List<EModeNPCBehaviour>();

        public bool FirstTick = true;

        public override void SetDefaults(NPC npc)
        {
            base.SetDefaults(npc);

            if (!FargoSoulsWorld.EternityMode)
                return;

            InitBehaviourList(npc);

            foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
            {
                behaviour.SetDefaults(npc);
            }

            bool recolor = SoulConfig.Instance.BossRecolors && FargoSoulsWorld.EternityMode;
            if (recolor || FargowiltasSouls.Instance.LoadedNewSprites)
            {
                FargowiltasSouls.Instance.LoadedNewSprites = true;
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    behaviour.LoadSprites(npc, recolor);
                }
            }
        }

        private void InitBehaviourList(NPC npc)
        {
            // TODO Try caching this again? Last attempt caused major fails
            IEnumerable<EModeNPCBehaviour> behaviours = EModeNPCBehaviour.AllEModeNpcBehaviours
                .Where(m => m.Matcher.Satisfies(npc.type));

            // To make sure they're always in the same order
            // TODO is ordering needed? Do they always have the same order?
            behaviours.OrderBy(m => m.GetType().FullName, StringComparer.InvariantCulture);

            EModeNpcBehaviours = behaviours.Select(m => m.NewInstance()).ToList();
        }

        #region Behaviour Hooks
        public override bool PreAI(NPC npc)
        {
            if (!FargoSoulsWorld.EternityMode)
                return true;

            bool result = true;

            foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
            {
                if (FirstTick)
                    behaviour.OnSpawn(npc);

                result &= behaviour.PreAI(npc);
            }

            FirstTick = false;

            return result;
        }

        public override void AI(NPC npc)
        {
            base.AI(npc);

            if (!FargoSoulsWorld.EternityMode)
                return;

            foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
            {
                behaviour.AI(npc);
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);

            foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                behaviour.ModifyNPCLoot(npc, npcLoot);
        }

        public override bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
        {
            bool result = true;

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    result &= behaviour.CanHitPlayer(npc, target, ref CooldownSlot);
                }
            }

            return result;
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            bool? result = base.CanBeHitByItem(npc, player, item);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    result &= behaviour.CanBeHitByItem(npc, player, item);
                }
            }

            return result;
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            bool? result = base.CanBeHitByProjectile(npc, projectile);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    result &= behaviour.CanBeHitByProjectile(npc, projectile);
                }
            }

            return result;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            base.OnHitPlayer(npc, target, damage, crit);

            if (!FargoSoulsWorld.EternityMode)
                return;

            foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
            {
                behaviour.OnHitPlayer(npc, target, damage, crit);
            }
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            base.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    behaviour.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
                }
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    behaviour.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);
                }
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            base.OnHitByItem(npc, player, item, damage, knockback, crit);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    behaviour.OnHitByItem(npc, player, item, damage, knockback, crit);
                }
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            base.OnHitByProjectile(npc, projectile, damage, knockback, crit);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    behaviour.OnHitByProjectile(npc, projectile, damage, knockback, crit);
                }
            }
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            bool result = base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    result &= behaviour.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
                }
            }

            return result;
        }

        public override void HitEffect(NPC npc, int hitDirection, double damage)
        {
            base.HitEffect(npc, hitDirection, damage);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    behaviour.HitEffect(npc, hitDirection, damage);
                }
            }
        }

        public override bool CheckDead(NPC npc)
        {
            bool result = base.CheckDead(npc);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    result &= behaviour.CheckDead(npc);
                }
            }

            return result;
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            Color? result = base.GetAlpha(npc, drawColor);

            if (FargoSoulsWorld.EternityMode)
            {
                foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
                {
                    result = behaviour.GetAlpha(npc, drawColor);
                }
            }

            return result;
        }

        public void NetSync(int whoAmI)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;

            ModPacket packet = FargowiltasSouls.Instance.GetPacket();
            packet.Write((byte)22); // New maso sync packet id
            packet.Write(whoAmI);

            foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
            {
                behaviour.NetSend(packet);
            }

            packet.Send();
        }

        public void NetRecieve(BinaryReader reader)
        {
            foreach (EModeNPCBehaviour behaviour in EModeNpcBehaviours)
            {
                behaviour.NetRecieve(reader);
            }
        }
        #endregion
    }
}
