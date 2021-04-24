using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SUIT { Engineering, Science, HR, Wildcard };

public class CardData
{
    public CardData(string name, string actionDescription, SUIT[] suits, CardActionDelegate cardActionDelegate)
    {
        this.Name = name;
        this.ActionDescription = actionDescription;
        this.suits = suits;
        this.cardActionDelegate = cardActionDelegate;
    }

    public delegate void CardActionDelegate();

    CardActionDelegate cardActionDelegate;

    public string Name { get; private set; }
    public string ActionDescription { get; private set; }

    SUIT[] suits;

    public int GetSuitCount( SUIT s )
    {
        // Return number of suits on this card that matchs s (including wildcards)
        return 0;
    }

    readonly string[] SuitIcons = new string[] {"🔧", "🔬", "H", "*"};

    public string GetSuitString()
    {
        string s = "";
        foreach(SUIT suit in suits)
        {
            s += SuitIcons[(int)suit];
        }
        return s;
    }

    public void DoAction()
    {
        if(cardActionDelegate != null)
        {
            cardActionDelegate();
        }
        else
        {
            Debug.Log("Card has no action.");
        }
    }
}
