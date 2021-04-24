using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class QuestDataLibrary
{
    public static List<QuestData> SurfaceQuestData;
    public static List<QuestData> DrillQuestData;

    // TODO:  Quest by...rarity?  Depth requirement?
    public static void Initialize()
    {
        InitDrill();
        InitSurface();
    }

    static void InitDrill()
    {
        DrillQuestData = new List<QuestData>();
        QuestData qd;

        qd = new QuestData(
            "Cracked Drill Casing",
            "",
            new SUIT[] { SUIT.Engineering }
        );
        DrillQuestData.Add(qd);
    }

    static void InitSurface()
    {
        // The Left quests 

        SurfaceQuestData = new List<QuestData>();
        QuestData qd;

        qd = new QuestData(
            "Empty Vending Machines",
            "",
            new SUIT[] { SUIT.Engineering }
        );
        SurfaceQuestData.Add(qd);

        qd = new QuestData(
            "Rat Infestation",
            "",
            new SUIT[] { SUIT.Engineering }
        );
        SurfaceQuestData.Add(qd);

        qd = new QuestData(
            "Worker Strike",
            "",
            new SUIT[] { SUIT.Engineering }
        );
        SurfaceQuestData.Add(qd);

    }
}