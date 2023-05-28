using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NIMod
{
    public class QuestJournalSystem : ModPlayer
    {
        public override void OnEnterWorld(Player player)
        {
            base.OnEnterWorld(player);

            Dictionary<int, int> fqList = new Dictionary<int, int>();
            fqList.Add(ItemID.Gel, 10);
            FetchQuest fq2 = new FetchQuest(NPCID.Guide, fqList);
            fqList.Add(ItemID.PinkGel, 1);
            FetchQuest fq3 = new FetchQuest(NPCID.Guide, fqList);
            QuestJournal.questList.Add(fq2);
            QuestJournal.questList.Add(fq3);
        }
    }
}
