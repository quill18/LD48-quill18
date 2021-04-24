using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDataInitializer{

    public static CardData[] Initialize()
    {
        // TODO:  Cards by...rarity?  Depth requirement?

        List<CardData> cards = new List<CardData>();

        cards.Add( new CardData("Test Card", "Should have Eng and Sci", new SUIT[] {SUIT.Engineering, SUIT.Science}, null ) );
        /*cards.Add( new CardData("Foo", "Foo's Action Text", new SUIT[] {SUIT.Wildcard}, null ) );
        cards.Add( new CardData("Foo", "Foo's Action Text", new SUIT[] {SUIT.Wildcard}, null ) );
        cards.Add( new CardData("Foo", "Foo's Action Text", new SUIT[] {SUIT.Wildcard}, null ) );
        cards.Add( new CardData("Foo", "Foo's Action Text", new SUIT[] {SUIT.Wildcard}, null ) );
        cards.Add( new CardData("Foo", "Foo's Action Text", new SUIT[] {SUIT.Wildcard}, null ) );*/

        return cards.ToArray();
    }

}