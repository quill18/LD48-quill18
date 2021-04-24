using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SUIT { Engineering, Science, Labour, Power, Wildcard };

abstract public class CardBase
{
    readonly string[] SuitIcons = new string[] {"🔧", "🔬", "👨", "⚡", "*"};


    public string Name { get; protected set; }
    public string Description { get; protected set; }

    public SUIT[] suits;

    /*public int GetSuitCount( SUIT s, SUIT[] cachedSuits=null )
    {
        if(cachedSuits == null)
            cachedSuits = suits;

        // Return number of suits on this card that matchs s (including wildcards)
        // It's the PLAYER cards that will usually? always? have the wildcard...

        // Maybe this function isn't actually useful

        int c = 0;
        foreach(SUIT suit in cachedSuits)
        {
            if(suit == s || suit == SUIT.Wildcard)
            {
                c++;
            }
        }

        return c;
    }*/


    public string GetSuitString( SUIT[] cachedSuits )
    {
        if(cachedSuits == null)
            cachedSuits = suits;

        if(cachedSuits == null)
        {
            Debug.LogError(Name + ": Null suits?");
            return "ERR";
        }

        string s = "";
        foreach(SUIT suit in cachedSuits)
        {
            s += SuitIcons[(int)suit];
        }
        return s;
    }
}
