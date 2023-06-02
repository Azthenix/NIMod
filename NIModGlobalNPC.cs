using IL.Terraria.Chat.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NIMod
{
    public class NIModGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);

            foreach(Quest q in QuestJournal.questList)
            {
                if(q.qType == QuestType.hunt)
                {
                    (q as HuntQuest).Tally(npc.netID);
                }
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            Random r = new Random();

            //complete quest
            foreach (Quest q in QuestJournal.questList)
            {
                if (q.completed && q.qNPCID == npc.netID)
                {
                    switch (q.qType)
                    {
                        case QuestType.fetch:
                            FetchQuest fq = (q as FetchQuest);

                            int froll = r.Next(3);

                            int reward = 0;
                            foreach (var item in fq.items)
                            {
                                reward += ContentSamples.ItemsByType[item.Key].value;
                            }

                            switch (froll)
                            {
                                case 1:
                                    chat = $"Great! These seem to be good. Here's your reward.";
                                    break;
                                case 2:
                                    chat = $"Thank you. Have this as compensation.";
                                    break;
                                default:
                                    chat = $"Thanks. This is for the trouble.";
                                    break;
                            }
                            QuestJournal.questList.Remove(q);

                            Item.NewItem(npc.GetSource_GiftOrReward(), npc.Center, ItemID.CopperCoin, reward * 5 * 10);
                            break;
                        case QuestType.hunt:
                            HuntQuest hq = (q as HuntQuest);

                            int hroll = r.Next(3);

                            switch (hroll)
                            {
                                case 1:
                                    chat = $"Thanks. That should keep them in check for a while. Here, take this.";
                                    Item.NewItem(npc.GetSource_GiftOrReward(), npc.Center, ItemID.CopperCoin, (int)ContentSamples.NpcsByNetId[hq.enemyID].value * hq.required * 10);
                                    break;
                                case 2:
                                    chat = $"Those {Lang.GetNPCNameValue(hq.enemyID)}s won't be causing noise anytime soon. Thank you, here's the reward.";
                                    Item.NewItem(npc.GetSource_GiftOrReward(), npc.Center, ItemID.CopperCoin, (int)ContentSamples.NpcsByNetId[hq.enemyID].value * hq.required * 10);
                                    break;
                                //default:
                                //    chat = $"It should be safer now at least. Take this, I've got a couple more left lying around.";
                                //    //List<int> itemList = (ContentSamples.ItemsByType.Where(item => (item.Value.rare == ItemRarityID.White && item.Value.maxStack >= 10)) as Dictionary<int, Item>).Keys.ToList();
                                //    var itemList = ContentSamples.ItemsByType.Values.Where(item => (item.rare == hq.rating && item.maxStack == 1));

                                //    Item.NewItem(npc.GetSource_GiftOrReward(), npc.Center, itemList.ElementAt(r.Next(itemList.Count())).netID, 1);
                                //    break;
                            }
                            QuestJournal.questList.Remove(q);
                            break;
                    }
                    return;
                }
            }

            foreach (Quest q in QuestJournal.availableQList)
            {
                if(q.qNPCID == npc.netID)
                {
                    switch (q.qType)
                    {
                        case QuestType.fetch:
                            FetchQuest fq = (q as FetchQuest);

                            int froll = r.Next(5);
                            switch (froll)
                            {
                                case 1:
                                    chat = $"I need some materials. Can you gather them for me?";
                                    break;
                                case 2:
                                    chat = $"Oh speak of the devil! I was about to call you for something.";
                                    break;
                                case 3:
                                    chat = $"I'm having trouble finding some stuff. Have you seen any?";
                                    break;
                                case 4:
                                    chat = $"These... get it... now.";
                                    break;
                                default:
                                    chat = $"Can you get some stuff for me? I'll get a reward ready.";
                                    break;
                            }
                            QuestJournal.questList.Add(new FetchQuest(fq.qNPCID, fq.items));
                            QuestJournal.availableQList.Remove(q);
                            break;
                        case QuestType.hunt:
                            HuntQuest hq = (q as HuntQuest);

                            int hroll = r.Next(4);

                            switch (hroll)
                            {
                                case 1:
                                    chat = $"{Lang.GetNPCNameValue(hq.enemyID)}s have been running wild recently. You might need to start clearing them soon.";
                                    break;
                                case 2:
                                    chat = $"I've been seeing a lot of {Lang.GetNPCNameValue(hq.enemyID)}s recently, I'm afraid they're starting to multiply too fast.";
                                    break;
                                case 3:
                                    chat = $"A {Lang.GetNPCNameValue(hq.enemyID)} jumped me earlier. It freaked me out. Can you take care of them?";
                                    break;
                                default:
                                    chat = $"You think {Lang.GetNPCNameValue(hq.enemyID)}s have been noisy these days? Well I think so, so take care of them for me.";
                                    break;
                            }
                            QuestJournal.questList.Add(new HuntQuest(hq.qNPCID, hq.enemyID, hq.required));
                            QuestJournal.availableQList.Remove(q);
                            break;
                    }
                    return;
                }
            }

            base.GetChat(npc, ref chat);
        }
    }
}
