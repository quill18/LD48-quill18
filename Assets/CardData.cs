using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : CardBase
{
    public CardData(string name, string description, SUIT[] suits, CardActionValidatorDelegate cardActionValidator, CardActionDelegate cardAction)
    {
        this.Name = name;
        this.Description = description;
        this.suits = suits;
        this.cardAction = cardAction;
        this.cardActionValidator = cardActionValidator;

    }

    public delegate void CardActionDelegate( CardGO cgo );
    CardActionDelegate cardAction;

    public delegate bool CardActionValidatorDelegate( CardGO cgo );
    CardActionValidatorDelegate cardActionValidator;

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

    public void DoAction( CardGO cgo )
    {
        // Run cost validator, exit if needed
        if(cardActionValidator != null && cardActionValidator(cgo) == false)
        {
            return; // Can't afford
        }

        if(cardAction != null)
        {
            cardAction(cgo);
        }
        else
        {
            Debug.Log("Card has no action.");
        }
    }
}
