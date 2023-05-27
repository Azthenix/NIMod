using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;

namespace NIMod
{
    public static class QuestJournal
    {
        public static List<Quest> questList = new List<Quest>();
    }

    public enum QuestType
    {
        fetch,
        hunt,
        interact
    }

    public class Quest : UIPanel
    {
        public int qNPCID;
        public QuestType qType;

        internal UIList container;
        internal UIText title;

        internal bool completed = false;

        public Quest()
        {
            this.Width.Set(0, 1.0f);
            this.Height.Set(100, 0);
            container = new UIList();
            this.Append(container);
            container.Width.Set(0, 1.0f);
            container.Height.Set(0, 1.0f);
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

            this.title = new UIText("Gather");
            this.container.Add(this.title);

            foreach (KeyValuePair<int, int> item in items)
            {
                int itemCount = Main.player[0].CountItem(item.Key, item.Value);
                this.container.Add(new UIText($" - {Lang.GetItemNameValue(item.Key)} {itemCount}/{item.Value}"));
            }
        }

        public void UpdateStatus()
        {
            //update stuff
        }
    }

    //public class HuntQuest : Quest
    //{
    //    public Dictionary<int, int> enemies;

    //    public HuntQuest(int qNPCID, Dictionary<int, int> enemies)
    //    {
    //        this.qNPCID = qNPCID;
    //        this.qType = QuestType.fetch;
    //        this.enemies = enemies;
    //    }
    //}

    //public class InteractQuest : Quest
    //{
    //    public int qNPCReceiverID;

    //    public InteractQuest(int qNPCID, int qNPCReceiverID)
    //    {
    //        this.qNPCID = qNPCID;
    //        this.qType = QuestType.fetch;
    //        this.qNPCReceiverID = qNPCReceiverID;
    //    }
    //}
}
