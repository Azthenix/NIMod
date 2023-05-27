using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace NIMod
{
    public enum QuestType
    {
        fetch,
        hunt,
        interact
    }

    public class Quest
    {
        public int qNPCID;
        public QuestType? qType;

        public Quest()
        {

        }
    }

    public class FetchQuest : Quest
    {
        public Dictionary<int, int> items;

        public FetchQuest(int qNPCID, Dictionary<int, int> items)
        {
            this.qNPCID = qNPCID;
            this.qType = QuestType.fetch;
            this.items = items;
        }
    }

    public class HuntQuest : Quest
    {
        public Dictionary<int, int> enemies;

        public HuntQuest(int qNPCID, Dictionary<int, int> enemies)
        {
            this.qNPCID = qNPCID;
            this.qType = QuestType.fetch;
            this.enemies = enemies;
        }
    }

    public class InteractQuest : Quest
    {
        public int qNPCReceiverID;

        public InteractQuest(int qNPCID, int qNPCReceiverID)
        {
            this.qNPCID = qNPCID;
            this.qType = QuestType.fetch;
            this.qNPCReceiverID = qNPCReceiverID;
        }
    }
}
