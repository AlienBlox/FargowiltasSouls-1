using Terraria;
using Terraria.Localization;
using Terraria.ID;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class AgitatingLens : SoulsItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Agitating Lens");
            Tooltip.SetDefault(@"Grants immunity to Berserked
10% increased damage when below half HP
While dashing or running quickly you will create a trail of demon scythes
'The irritable remnant of a defeated foe'");
            DisplayName.AddTranslation(GameCulture.Chinese, "躁动晶状体");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'被打败的敌人的躁动残渣'
免疫狂暴
生命低于50%时,增加10%伤害
冲刺或快速奔跑时发射一串恶魔之镰");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = ItemRarityID.Green;
            item.value = Item.sellPrice(0, 1);
            item.GetGlobalItem<EternityItem>().Eternity = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Berserked")] = true;

            if (player.statLife < player.statLifeMax2 / 2)
                player.GetModPlayer<FargoPlayer>().AllDamageUp(.10f);

            player.GetModPlayer<FargoPlayer>().AgitatingLens = true;
        }
    }
}