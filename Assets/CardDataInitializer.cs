using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDataInitializer{

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

        cd = new CardData("Starter Engineering 1", "1MP: ???", 
            new SUIT[] {SUIT.Engineering}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Replacement Parts", "1MP: Discard this to draw 2 cards", 
            new SUIT[] {SUIT.Engineering}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Focus on Theory", "2MP: Change all Eng card suits to Sci", 
            new SUIT[] {SUIT.Engineering}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Starter Science 1", "1MP: Draw Card", 
            new SUIT[] {SUIT.Science}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Human Experiments", "Discard, 5 Morale: Gain 2MP", 
            new SUIT[] {SUIT.Science}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Focus on the Practical", "2MP: Change all Sci card suits to Eng", 
            new SUIT[] {SUIT.Science}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Extended Shift", "Discard: Generate 1MP", 
            new SUIT[] {SUIT.HR}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Restructuring", "Discard all Unplayed Cards, draw that many", 
            new SUIT[] {SUIT.HR}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Cheap Birthday Cake", "X Manpower: Gain X*2 Morale", 
            new SUIT[] {SUIT.HR}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData("Duct Tape", "That should fix it.", 
            new SUIT[] {SUIT.Wildcard}, 
            null );
        AllPlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        ///////////////////////////////////////////// Non-Starters

    }

}