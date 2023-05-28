using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

using NIMod.UI;
using System;
using Terraria.ID;
using System.Linq;
using Terraria.Chat;
using Terraria.Localization;

namespace NIMod
{
    public class NIMod : ModSystem
    {
        internal QuestUI questUI;
        public UserInterface questInterface;

        private int counter = 0;

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
            layers.Insert(layers.FindIndex(layer => layer.Name == "Vanilla: Inventory"), new LegacyGameInterfaceLayer("NIMod: Quest UI", DrawSomethingUI, InterfaceScaleType.UI));
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

        public override void PostUpdateEverything()
        {
            base.PostUpdateEverything();

            if (!Main.gameMenu)
            {
                counter++;
                if (counter % 60 == 0)
                {
                    foreach (Quest q in QuestJournal.questList)
                    {
                        if (q.qType == QuestType.fetch)
                        {
                            (q as FetchQuest).UpdateStatus();
                        }
                    }

                    if(counter >= 60)
                    {
                        Random r = new Random();

                        int roll = r.Next((int)Math.Pow(10, QuestJournal.questList.Count + QuestJournal.availableQList.Count + 2));

                        if(roll <= (QuestJournal.questList.Count + QuestJournal.availableQList.Count + 1) * 10)
                        {
                            NIModel.ModelInput sampleData = new NIModel.ModelInput()
                            {
                                DownedEOC = (NPC.downedBoss1 ? 1F : 0F),
                                DownedSkel = (NPC.downedBoss3 ? 1F : 0F),
                                DownedWOF = (Main.hardMode ? 1F : 0F),
                            };

                            NIModel.ModelOutput output = NIModel.Predict(sampleData);
                            int rating = (int)output.Score;

                            roll = r.Next(2);

                            if(roll == 0)
                            {
                                Dictionary<int, int> dict = new Dictionary<int, int>();
                                var itemList = ContentSamples.ItemsByType.Values.Where(item => (item.rare == rating && item.maxStack >= 10));
                                dict.Add(itemList.ElementAt(r.Next(itemList.Count())).netID, 10);

                                FetchQuest fq = new FetchQuest(NPCID.Guide, dict, rating);
                                QuestJournal.availableQList.Add(fq);
                            }
                            else
                            {
                                var npcList = ContentSamples.NpcsByNetId.Values.Where(npc => (npc.lifeMax >= Math.Pow(3, rating) && npc.lifeMax <= Math.Pow(3, rating + 1) && !npc.friendly));

                                HuntQuest hq = new HuntQuest(NPCID.Guide, npcList.ElementAt(r.Next(npcList.Count())).netID, 10, rating);
                                QuestJournal.availableQList.Add(hq);
                            }

                            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The quide seems to be looking for help."), Microsoft.Xna.Framework.Color.Blue);
                        }
                    }

                    counter %= 60;
                }
            }
        }
    }
}