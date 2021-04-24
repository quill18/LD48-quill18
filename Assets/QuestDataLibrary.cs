using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class QuestDataLibrary
{
    public static List<QuestData> SurfaceQuestData;
    public static List<QuestData> DrillQuestData;

    static bool wasInitialized = false;

    // TODO:  Quest by...rarity?  Depth requirement?
    public static void Initialize()
    {
        if(wasInitialized)
            return;

        wasInitialized = true;

        InitDrill();
        InitSurface();
    }

    static void InitDrill()
    {
        DrillQuestData = new List<QuestData>();
        QuestData qd;

        qd = new QuestData(
            "Tough Granite",
            "",
            new SUIT[] { SUIT.Power, SUIT.Power, SUIT.Power }
        );
        DrillQuestData.Add(qd);

        qd = new QuestData(
            "Oil Deposit",
            "",
            new SUIT[] { SUIT.Science, SUIT.Power }
        );
        DrillQuestData.Add(qd);

        qd = new QuestData(
            "Ruby Deposit",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Labour, SUIT.Power }
        );
        DrillQuestData.Add(qd);

        qd = new QuestData(
            "Cracked Drill Casing",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Engineering }
        );
        DrillQuestData.Add(qd);
    }

    static void InitSurface()
    {
        // The Left quests 

        SurfaceQuestData = new List<QuestData>();
        QuestData qd;

        qd = new QuestData(
            "Out of snacks!",
            "Lose 1 Morale per shift until restocked.",
            new SUIT[] { SUIT.Labour, SUIT.Labour, SUIT.Labour }
        );
        qd.onShiftEnd += (cardGO) => { PlayerManager.Instance.CurrentHitpoints -= 1; };
        SurfaceQuestData.Add(qd);

        qd = new QuestData(
            "Rat Infestation",
            "Lose 1 Morale every time you play a card. Break out the flamethrowers!",
            new SUIT[] { SUIT.Engineering, SUIT.Power }
        );

        PlayerManager.CardPlayedDelegate d = (cardGO) => { DecreaseMorale(1); };
        qd.onEnter += (questGO) => { PlayerManager.Instance.onCardPlayed += d;};
        qd.onExit  += (QuestGO) => { PlayerManager.Instance.onCardPlayed -= d;};
        SurfaceQuestData.Add(qd);

        qd = new QuestData(
            "Worker Strike",
            "Unable to complete other quests.",
            new SUIT[] { SUIT.Labour, SUIT.Science, SUIT.Power }
        );
        qd.isQuestBlocker = true;
        SurfaceQuestData.Add(qd);

    }

    static void DecreaseMorale(int i)
    {
        PlayerManager.Instance.CurrentHitpoints -= i;
    }
}