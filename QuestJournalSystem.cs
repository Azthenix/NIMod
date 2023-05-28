﻿using System;
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

            HuntQuest hq = new HuntQuest(NPCID.Guide, NPCID.GreenSlime, 3);
            QuestJournal.availableQList.Add(hq);
        }
    }
}
