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
        public static UIList questList = new UIList();
        public static UIList availableQList = new UIList();
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
        public int rating;

        internal UIList container;
        internal UIText title;

        internal bool completed = false;

        public Quest()
        {
            this.Width.Set(0, 1.0f);
            this.Height.Set(0, 1.0f);
            container = new UIList();
            this.Append(container);
            container.Width.Set(0, 1.0f);
            container.Height.Set(0, 1.0f);
        }
    }

    public class FetchQuest : Quest
    {
        public Dictionary<int, int> items;

        public FetchQuest(int qNPCID, Dictionary<int, int> items, int rating)
        {
            this.qNPCID = qNPCID;
            this.qType = QuestType.fetch;
            this.items = items;
            this.rating = rating;

            this.title = new UIText("Gather", 0.8f);
            this.container.Add(this.title);

            foreach (KeyValuePair<int, int> item in items)
            {
                int itemCount = Main.LocalPlayer.CountItem(item.Key, item.Value);
                this.container.Add(new UIText($" - {Lang.GetItemNameValue(item.Key)} {itemCount}/{item.Value}", 0.6f));
            }
            this.container.Height.Set((items.Count * 15) + 40, 0);

            this.Height.Set(title.Height.Pixels + this.container.Height.Pixels, 0);
        }

        public void UpdateStatus()
        {
            //update stuff
            this.container.Clear();
            this.container.Add(this.title);

            bool finished = true;
            List<UIText> list = new List<UIText>();

            foreach (KeyValuePair<int, int> item in items)
            {
                int itemCount = Main.LocalPlayer.CountItem(item.Key, item.Value);
                list.Add(new UIText($" - {Lang.GetItemNameValue(item.Key)} {itemCount}/{item.Value}", 0.6f));

                if(finished && (itemCount < item.Value))
                {
                    finished = false;
                }
            }

            if (finished)
            {
                completed = true;

                this.container.Add(new UIText($" - Talk to {Lang.GetNPCNameValue(qNPCID)}", 0.6f));

                this.container.Height.Set(55, 0);

                this.Height.Set(title.Height.Pixels + this.container.Height.Pixels, 0);
            }
            else
            {
                this.container.AddRange(list);
                this.container.Height.Set((items.Count * 15) + 40, 0);

                this.Height.Set(title.Height.Pixels + this.container.Height.Pixels, 0);
            }
        }
    }

    public class HuntQuest : Quest
    {
        public int enemyID;
        public int count;
        public int required;

        public HuntQuest(int qNPCID, int enemy, int required, int rating)
        {
            this.qNPCID = qNPCID;
            this.qType = QuestType.hunt;
            this.enemyID = enemy;
            this.rating = rating;

            this.title = new UIText("Hunt", 0.8f);
            this.container.Add(this.title);

            this.container.Add(new UIText($" - {Lang.GetNPCNameValue(enemyID)} {count}/{required}", 0.6f));

            this.container.Height.Set(55, 0);

            this.Height.Set(title.Height.Pixels + this.container.Height.Pixels, 0);
            this.count = 0;
            this.required = required;
        }

        public void Tally(int enemy)
        {
            //update stuff
            if (completed)
                return;

            if(enemy == enemyID)
            {
                this.container.Clear();
                this.container.Add(this.title);
                count++;

                if(count >= required)
                {
                    completed = true;
                    this.container.Add(new UIText($" - Talk to {Lang.GetNPCNameValue(qNPCID)}", 0.6f));
                }
                else
                {
                    this.container.Add(new UIText($" - {Lang.GetNPCNameValue(enemyID)} {count}/{required}", 0.6f));
                }
            }
        }
    }

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
