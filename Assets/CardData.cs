using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : CardBase
{
    public CardData(int level, string name, string description, SUIT[] suits, CardActionValidatorDelegate cardActionValidator, CardActionDelegate cardAction)
    {
        this.Level = level;
        this.Name = name;
        this.Description = description;
        this.suits = suits;
        this.cardAction = cardAction;
        this.cardActionValidator = cardActionValidator;
    }

    public int Level { get; protected set; }

    public delegate void CardActionDelegate( CardGO cgo );
    CardActionDelegate cardAction;

    public CardActionDelegate otherCardPlayed;
    public CardActionDelegate otherCardDiscarded;

    public delegate bool CardActionValidatorDelegate( CardGO cgo );
    CardActionValidatorDelegate cardActionValidator;

    public bool IsRetained = false; // Doesn't get discarded at end of shift

    public bool HasSuit( SUIT target, SUIT[] cachedSuits )
    {
        if(cachedSuits == null)
            cachedSuits = suits;

        foreach(SUIT s in cachedSuits )
        {
            if(s == target)
                return true;
        }

        return false;
    }

    public bool DoAction( CardGO cgo )
    {
        // Run cost validator, exit if needed
        if(cardActionValidator != null && cardActionValidator(cgo) == false)
        {
            return false; // Can't afford
        }

        if(cardAction == null)
        {
            // We don't have an action
            Debug.Log("Card has no action.");
            return false;
        }

        cardAction(cgo);

        return true;
    }
}
