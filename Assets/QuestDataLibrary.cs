using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class QuestDataLibrary
{
    public static List<QuestData> SurfaceQuestData;
    public static List<QuestData> ForcedFirstQuest;
    public static List<QuestData> DrillQuestData_Dirt;
    public static List<QuestData> DrillQuestData_Treasure;

    static bool wasInitialized = false;

    // TODO:  Quest by...rarity?  Depth requirement?
    public static void Initialize()
    {
        if(wasInitialized)
            return;

        wasInitialized = true;

        InitDrill_Dirt();
        InitDrill_Treasure();
        InitSurface();
    }

    static void InitDrill_Dirt()
    {
        DrillQuestData_Dirt = new List<QuestData>();
        QuestData qd;

        // sand silt clay, sandstone, limestone, granite

        qd = new QuestData(
            1,
            "Sandy Layer",
            "",
            new SUIT[] { SUIT.Science, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            1,
            "Pumice Layer",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            1,
            "Andesite Layer",
            "",
            new SUIT[] { SUIT.Labour, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        /////////////////////////////////////////////

        qd = new QuestData(
            2,
            "Basalt Layer",
            "",
            new SUIT[] { SUIT.Science, SUIT.Engineering, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            3,
            "Granite Layer",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Labour, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            4,
            "Tough Granite",
            "",
            new SUIT[] { SUIT.Labour, SUIT.Science, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        /////////////////////////////////////////////

        qd = new QuestData(
            5,
            "Carbonatite Layer",
            "",
            new SUIT[] { SUIT.Science, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            6,
            "Diorite Layer",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            7,
            "Gabbro Layer",
            "",
            new SUIT[] { SUIT.Labour, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        ///////////////////////////////////////////////////////////////

        qd = new QuestData(
            8,
            "Obsidian Layer",
            "",
            new SUIT[] { SUIT.Science, SUIT.Engineering, SUIT.Labour, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        ///////////////////////////////////////////////////////////////

        qd = new QuestData(
            9,
            "Rhyolite Layer",
            "",
            new SUIT[] { SUIT.Science, SUIT.Engineering, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            10,
            "Cheese Layer",
            "Wait...what?",
            new SUIT[] { SUIT.Science, SUIT.Labour, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            11,
            "Monzonite Layer",
            "",
            new SUIT[] { SUIT.Labour, SUIT.Engineering, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);
        
        ///////////////////////////////////////////////////////////////

        qd = new QuestData(
            12,
            "Batholith Layer",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Power, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);
        
        qd = new QuestData(
            13,
            "Kimberlite Layer",
            "",
            new SUIT[] { SUIT.Science, SUIT.Power, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        qd = new QuestData(
            14,
            "Redstone Layer",
            "",
            new SUIT[] { SUIT.Labour, SUIT.Power, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);

        ///////////////////////////////////////////////////////////////

        qd = new QuestData(
            15,
            "Belgium",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Science, SUIT.Labour, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Dirt.Add(qd);


    }

    public static List<QuestData> GetDrillQuestsForLevel( int level )
    {
        List<QuestData> legalDirt = new List<QuestData>();
        // First, make a list of all the DIRT quests up to level
        // Randomize list
        // Grab first 3.
        foreach(QuestData qd in DrillQuestData_Dirt)
        {
            if(qd.Level <= level)
            {
                legalDirt.Add(qd);
            }
        }

        List<QuestData> sortedThreeDirt = legalDirt.OrderBy( a => Random.Range(0, 1000000) ).Take(3).ToList();

        // Then, grab the levelth TREASURE quest and append it.
        sortedThreeDirt.Add( DrillQuestData_Treasure[level-1] );

        return sortedThreeDirt;
    }


    static void InitDrill_Treasure()    /////////////////////////////////////////////////////////////////////////
    {
        DrillQuestData_Treasure = new List<QuestData>();
        QuestData qd;

        // quarts, mithral, diamond, oil, ruby, sapphire

        qd = new QuestData(
            1,
            "Quartz Crystals",
            "",
            new SUIT[] { SUIT.Science, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        qd = new QuestData(
            1,
            "Alexandrite",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        qd = new QuestData(
            1,
            "Amber",
            "Any dinosaur blood?",
            new SUIT[] { SUIT.Labour, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        /////////////////////////////////////////

        qd = new QuestData(
            1,
            "Amethyst",
            "",
            new SUIT[] { SUIT.Science, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);


        qd = new QuestData(
            1,
            "Andradite",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);


        qd = new QuestData(
            1,
            "Garnet",
            "",
            new SUIT[] { SUIT.Labour, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        /////////////////////////////////////////

        qd = new QuestData(
            1,
            "Aquamarine",
            "",
            new SUIT[] { SUIT.Science, SUIT.Engineering, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);


        qd = new QuestData(
            1,
            "Diamond",
            "Birthstone for the best month!",
            new SUIT[] { SUIT.Science, SUIT.Labour, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);


        qd = new QuestData(
            1,
            "Ruby",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Labour, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        /////////////////////////////////////////

        qd = new QuestData(
            1,
            "Emerald",
            "",
            new SUIT[] { SUIT.Science, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);


        qd = new QuestData(
            1,
            "Peridot",
            "",
            new SUIT[] { SUIT.Engineering, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);


        qd = new QuestData(
            1,
            "Sapphire",
            "",
            new SUIT[] { SUIT.Labour, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        /////////////////////////////////////////

        qd = new QuestData(
            1,
            "Tourmaline",
            "",
            new SUIT[] { SUIT.Science, SUIT.Engineering, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);


        qd = new QuestData(
            1,
            "Topaz",
            "",
            new SUIT[] { SUIT.Science, SUIT.Labour, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        qd = new QuestData(
            1,
            "Tanzanite",
            "",
            new SUIT[] { SUIT.Labour, SUIT.Engineering, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        /////////////////////////////////////////


        qd = new QuestData(
            1,
            "Petra-fied Wood",
            "",
            new SUIT[] { SUIT.Science, SUIT.Engineering, SUIT.Power, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);

        
        qd = new QuestData(
            1,
            "Unobtanium",
            "Surprisingly obtainable.",
            new SUIT[] { SUIT.Science, SUIT.Labour, SUIT.Power, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);


        qd = new QuestData(
            18,
            "Whiskichocolatium",
            "FINAL LEVEL!",
            new SUIT[] { SUIT.Labour, SUIT.Engineering, SUIT.Power, SUIT.Power, SUIT.Power }
        );
        DrillQuestData_Treasure.Add(qd);





    }



    static void InitSurface()
    {
        // The Left quests 

        SurfaceQuestData = new List<QuestData>();
        ForcedFirstQuest = new List<QuestData>();
        QuestData qd;

        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            1,
            "Out of snacks!",
            "Lose 1 Morale at beginning of shift until restocked.",
            new SUIT[] { SUIT.Labour, SUIT.Labour }
        );
        qd.onShiftEnd += (cardGO) => { PlayerManager.Instance.CurrentHitpoints -= 1; };
        SurfaceQuestData.Add(qd);

        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            1,
            "Rat Infestation",
            "Lose 1 Morale every time you play a card. Break out the flamethrowers!",
            new SUIT[] { SUIT.Science, SUIT.Science }
        );

        PlayerManager.CardPlayedDelegate d = (cardGO) => { DecreaseMorale(1); };
        qd.onEnter += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.onCardPlayed += d;}};
        qd.onExit  += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.onCardPlayed -= d;}};
        SurfaceQuestData.Add(qd);

        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            1,
            "Gremlins in the System",
            "All tasks cost +1 Power. (Including this one.)",
            new SUIT[] { SUIT.Engineering }
        );
        qd.onEnter += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.AddExtraSuit(SUIT.Power);}};
        qd.onExit  += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.RemoveExtraSuit(SUIT.Power);}};
        SurfaceQuestData.Add(qd);

        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            1,
            "Computer Virus",
            "Draw one fewer card per shift.",
            new SUIT[] { SUIT.Science, SUIT.Science }
        );
        qd.onEnter += (cardGO) => { PlayerManager.Instance.CardDrawAmount--; };
        qd.onExit += (cardGO) => { if(PlayerManager.Instance != null) { PlayerManager.Instance.CardDrawAmount++;} };
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            1,
            "QUILLVID-18 Outbreak",
            "-1 Workforce per shift.",
            new SUIT[] { SUIT.Labour, SUIT.Labour }
        );
        qd.onEnter += (cardGO) => { PlayerManager.Instance.MaxMana--; };
        qd.onExit += (cardGO) => { if(PlayerManager.Instance != null) { PlayerManager.Instance.MaxMana++;} };
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            2,
            "Safety Inspection",
            "Unable to complete other quests.",
            new SUIT[] { SUIT.Science, SUIT.Engineering }
        );
        qd.isQuestBlocker = true;
        SurfaceQuestData.Add(qd);
        ForcedFirstQuest.Add(qd);

        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            3,
            "Coffee Break",
            "-1 Workforce per Shift, but gain 5 morale when completed",
            new SUIT[] { SUIT.Labour, SUIT.Labour }
        );
        qd.onEnter += (cardGO) => { PlayerManager.Instance.MaxMana--; };
        qd.onExit += (cardGO) => { if(PlayerManager.Instance != null) { PlayerManager.Instance.MaxMana++;} };
        qd.onSuccess += (cardGO) => { PlayerManager.Instance.CurrentHitpoints += 5; };
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            3,
            "Rounding Error",
            "All tasks cost +1 Science. (Including this one.)",
            new SUIT[] { SUIT.Engineering }
        );
        qd.onEnter += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.AddExtraSuit(SUIT.Science);}};
        qd.onExit  += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.RemoveExtraSuit(SUIT.Science);}};
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            4,
            "Cracked Drill Shaft",
            "Draw one fewer card per shift.",
            new SUIT[] { SUIT.Engineering, SUIT.Engineering }
        );
        qd.onEnter += (cardGO) => { PlayerManager.Instance.CardDrawAmount--; };
        qd.onExit += (cardGO) => { if(PlayerManager.Instance != null) { PlayerManager.Instance.CardDrawAmount++;} };
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            4,
            "Out of Duct Tape",
            "All tasks cost +1 Engineering. (Including this one.)",
            new SUIT[] { SUIT.Engineering }
        );
        qd.onEnter += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.AddExtraSuit(SUIT.Engineering);}};
        qd.onExit  += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.RemoveExtraSuit(SUIT.Engineering);}};
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            4,
            "Seismic Instability",
            "Draw +1 Task each Shift",
            new SUIT[] { SUIT.Engineering, SUIT.Science }
        );
        qd.onShiftEnd += (cardGO) => { GameManager.Instance.AddNewSurfaceQuest(); };
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            5,
            "Worker Strike",
            "Unable to complete other quests (until you strike the workers with a cattle prod.)",
            new SUIT[] { SUIT.Labour, SUIT.Labour, SUIT.Power }
        );
        qd.isQuestBlocker = true;
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            5,
            "Internet Outage",
            "Lose 5 morale per shift, but gain 1 Workforce per shift.",
            new SUIT[] { SUIT.Engineering, SUIT.Engineering }
        );
        qd.onEnter += (cardGO) => { PlayerManager.Instance.MaxMana++; };
        qd.onExit += (cardGO) => { if(PlayerManager.Instance != null) { PlayerManager.Instance.MaxMana--;} };
        qd.onShiftEnd += (cardGO) => { PlayerManager.Instance.CurrentHitpoints -= 5; };
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            6,
            "Labour Union",
            "All tasks cost +1 Labour. (Including this one.)",
            new SUIT[] { SUIT.Engineering }
        );
        qd.onEnter += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.AddExtraSuit(SUIT.Labour);}};
        qd.onExit  += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.RemoveExtraSuit(SUIT.Labour);}}; 
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            6,
            "Karaoke Night",
            "-1 Workforce per shift, but gain 10 Morale when resolved.",
            new SUIT[] { SUIT.Labour, SUIT.Power, SUIT.Power }
        );
        qd.onEnter += (cardGO) => { PlayerManager.Instance.MaxMana--; };
        qd.onExit += (cardGO) => { if(PlayerManager.Instance != null) { PlayerManager.Instance.MaxMana++;} };
        qd.onSuccess += (cardGO) => { PlayerManager.Instance.CurrentHitpoints += 10; };
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            7,
            "Visit from the EPA",
            "Unable to complete other quests (until you bribe the officials).",
            new SUIT[] { SUIT.Labour, SUIT.Power, SUIT.Power }
        );
        qd.isQuestBlocker = true;
        SurfaceQuestData.Add(qd);


        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            8,
            "Mandatory Meetings",
            "Maximum Workforce is reduced by one.",
            new SUIT[] { SUIT.Science, SUIT.Engineering, SUIT.Labour }
        );
        qd.onEnter += (cardGO) => { PlayerManager.Instance.MaxMana--; };
        qd.onExit += (cardGO) => { if(PlayerManager.Instance != null) { PlayerManager.Instance.MaxMana++;} };
        SurfaceQuestData.Add(qd);

        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            10,
            "Comet Sighted",
            "Lose 2 morale when you play a card.",
            new SUIT[] { SUIT.Science, SUIT.Science, SUIT.Science }
        );
        PlayerManager.CardPlayedDelegate d5 = (cardGO) => { DecreaseMorale(2); };
        qd.onEnter += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.onCardPlayed += d5;}};
        qd.onExit  += (questGO) => { if(PlayerManager.Instance != null) {PlayerManager.Instance.onCardPlayed -= d5;}};

        SurfaceQuestData.Add(qd);

        /////////////////////////////////////////////////////////////////////////////////////////
        qd = new QuestData(
            10,
            "Found the circus!",
            "Lose 5 morale at beginning of shift.",
            new SUIT[] { SUIT.Engineering, SUIT.Engineering, SUIT.Labour, SUIT.Labour }
        );
        qd.onShiftEnd += (cardGO) => { PlayerManager.Instance.CurrentHitpoints -= 5; };
        SurfaceQuestData.Add(qd);


    }

    static void DecreaseMorale(int i)
    {
        PlayerManager.Instance.CurrentHitpoints -= i;
    }
}