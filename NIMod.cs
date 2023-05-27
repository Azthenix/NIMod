using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

using NIMod.UI;

namespace NIMod
{
	public class NIMod : ModSystem
    {
        internal QuestUI questUI;
        public UserInterface questInterface;

        public override void Load()
        {
            // this makes sure that the UI doesn't get opened on the server
            // the server can't see UI, can it? it's just a command prompt
            if (!Main.dedServ)
            {
                questUI = new QuestUI();
                questUI.Initialize();
                questInterface = new UserInterface();
                questInterface.SetState(questUI);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            // it will only draw if the player is not on the main menu
            if (!Main.gameMenu
                && QuestUI.visible)
            {
                questInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Add(new LegacyGameInterfaceLayer("Cool Mod: Something UI", DrawSomethingUI, InterfaceScaleType.UI));
        }

        private bool DrawSomethingUI()
        {
            // it will only draw if the player is not on the main menu
            if (!Main.gameMenu
                && QuestUI.visible)
            {
                questInterface.Draw(Main.spriteBatch, new GameTime());
            }
            return true;
        }
    }
}