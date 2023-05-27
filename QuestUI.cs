﻿using IL.Terraria.GameContent.Events;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace NIMod.UI
{
    public class QuestUI : UIState
    {
        public static bool visible;
        public UIPanel panel;

        public int width = 175;
        public int height = 250;

        public override void OnInitialize()
        {
            // if you set this to true, it will show up in game
            visible = true;

            panel = new DragableUIPanel(); //initialize the panel
            // ignore these extra 0s
            panel.Left.Set(600, 0); //this makes the distance between the left of the screen and the left of the panel 500 pixels (somewhere by the middle)
            panel.Top.Set(100, 0); //this is the distance between the top of the screen and the top of the panel
            panel.Width.Set(width, 0);
            panel.Height.Set(height, 0);

            UIText title = new UIText("Quests", 0.6f, true);

            UIText qSample = new UIText("Sample");

            panel.Append(title); //bitch
            panel.Append(qSample);

            panel.OverflowHidden = true;

            Append(panel); //appends the panel to the UIState
        }
    }
}
