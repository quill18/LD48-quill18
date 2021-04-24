using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDataLibrary{

    public static List<CardData> AllPlayerCards;
    public static List<CardData> StartingPlayerCards;

    public static void Initialize()
    {
        // TODO:  Cards by...rarity?  Depth requirement?

        AllPlayerCards = new List<CardData>();
        StartingPlayerCards = new List<CardData>();
        CardData cd;

        /*
            The different card categories should have a general theme of effect (but not exclusively)
            Science = Card Draw
            Engineering = Card Modification
            "HR" = Morale & MP generation?
        */

        //////////////////////////////////////////  Starting Cards

        cd = new CardData("Overload the Turbines", "1MP: Add one temporary Power to your hand.", 
            new SUIT[] {SUIT.Engineering},
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 1; }, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Replacement Parts", "1MP: Discard this to draw 2 cards", 
            new SUIT[] {SUIT.Engineering}, 
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 1; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentMana -= 1; 
                cgo.Discard(); 
                PlayerManager.Instance.DrawCards(2); } );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Focus on Theory", "2MP: Change all Eng card suits to Sci", 
            new SUIT[] {SUIT.Engineering}, 
            null, 
            null );
        AllPlayerCards.Add( cd );
        //StartingPlayerCards.Add ( cd );

        cd = new CardData("Basic Research", "1MP: Draw Card", 
            new SUIT[] {SUIT.Science}, 
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 1; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentMana -= 1; 
                PlayerManager.Instance.DrawCards(1); } );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Human Experiments", "Discard, 5 Morale: Gain 2MP", 
            new SUIT[] {SUIT.Science}, 
            (cgo) => { return PlayerManager.Instance.CurrentHitpoints > 5; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentHitpoints -= 5; 
                PlayerManager.Instance.CurrentMana += 2; } );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        //StartingPlayerCards.Add ( cd );

        cd = new CardData("Focus on the Practical", "2MP: Change all Sci card suits to Eng", 
            new SUIT[] {SUIT.Science}, 
            null, 
            null );
        AllPlayerCards.Add( cd );
        //StartingPlayerCards.Add ( cd );

        cd = new CardData("Extended Shift", "Discard: Generate 1MP", 
            new SUIT[] {SUIT.Labour}, 
            null, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        //StartingPlayerCards.Add ( cd );

        cd = new CardData("Restructuring", "Discard all Unplayed Cards, draw that many", 
            new SUIT[] {SUIT.Labour}, 
            null, 
            null );
        AllPlayerCards.Add( cd );
        //StartingPlayerCards.Add ( cd );
        //StartingPlayerCards.Add ( cd );

        cd = new CardData("Cheap Birthday Cake", "X Manpower: Gain X*2 Morale", 
            new SUIT[] {SUIT.Labour}, 
            null, 
            null );
        AllPlayerCards.Add( cd );
        //StartingPlayerCards.Add ( cd );

        cd = new CardData("Power", "", 
            new SUIT[] {SUIT.Power}, 
            null, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

       /*cd = new CardData("Duct Tape", "That should fix it.", 
            new SUIT[] {SUIT.Wildcard}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );*/

        ///////////////////////////////////////////// Non-Starters

    }

}