using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class MutantScale : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mutant Scale");
            DisplayName.AddTranslation(GameCulture.Chinese, "突变鳞片");
        }

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.rare = 11;
            item.value = Item.sellPrice(0, 4, 0, 0);
        }
    }
}
