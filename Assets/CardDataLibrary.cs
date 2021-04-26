using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDataLibrary{

    public static List<CardData> GainablePlayerCards;
    public static List<CardData> StartingPlayerCards;
    public static CardData PowerCard;

    public static void Initialize()
    {
        // TODO:  Cards by...rarity?  Depth requirement?

        GainablePlayerCards = new List<CardData>();
        StartingPlayerCards = new List<CardData>();
        CardData cd;

        /*
            The different card categories should have a general theme of effect (but not exclusively)
            Science = Card Draw
            Engineering = Card Modification
            "HR" = Morale & MP generation?
        */

        //////////////////////////////////////////  Starting Cards

        cd = new CardData(
            1,
            "Overload the Turbines", 
            "1 Workforce: Add one temporary Power to your hand.", 
            new SUIT[] {SUIT.Engineering},
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 1; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentMana -= 1; 
                PlayerManager.Instance.CreateTemporaryCard( PowerCard ); } 
            );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add( cd );

        cd = new CardData(
            1,
            "Replacement Parts", 
            "1 Workforce, Discard: Draw 2 cards", 
            new SUIT[] {SUIT.Engineering}, 
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 1; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentMana -= 1; 
                cgo.Discard(); 
                PlayerManager.Instance.DrawCards(2); } );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData(
            1,
            "Basic Research", 
            "1 Workforce: Draw Card", 
            new SUIT[] {SUIT.Science}, 
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 1; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentMana -= 1; 
                PlayerManager.Instance.DrawCards(1); } );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData(
            1,
            "Power", 
            "", 
            new SUIT[] {SUIT.Power}, 
            null, 
            null );
        PowerCard = cd;
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );
        StartingPlayerCards.Add ( cd );


        cd = new CardData(
            1,
            "Creative Workaround", 
            "Ignore blockers until you solve a task.", 
            new SUIT[] {SUIT.Engineering}, 
            null,
            (cardGO) => { PlayerManager.Instance.IgnoreNextBlocker++; }
        );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData(
            1,
            "Human Experiments", 
            "5 Morale: Gain 2 Workforce", 
            new SUIT[] {SUIT.Science}, 
            (cgo) => { return PlayerManager.Instance.CurrentHitpoints > 5; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentHitpoints -= 5; 
                PlayerManager.Instance.CurrentMana += 2; } );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData(
            1,
            "Focus on the Practical", 
            "Change all Science card suits to Engineering", 
            new SUIT[] {SUIT.Science}, 
            null,
            (cgo) => { 
                //PlayerManager.Instance.CurrentMana -= 2; 
                ChangeSuits(SUIT.Science, SUIT.Engineering); } );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData(
            1,
            "Unpaid Interns", 
            "Discard: Generate 1 Workforce", 
            new SUIT[] {SUIT.Labour}, 
            null, 
            (cardGO) => { 
                cardGO.Discard();
                PlayerManager.Instance.CurrentMana += 1;
             } );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData(
            1,
            "Restructuring", 
            "5 Morale: Discard this and all Unused Cards, then draw that many cards", 
            new SUIT[] {SUIT.Labour}, 
            (cgo) => { return PlayerManager.Instance.CurrentHitpoints > 5; }, 
            (cardGO) => { 
                PlayerManager.Instance.CurrentHitpoints -= 5;
                DiscardUnusedAndDraw(cardGO);
             } );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        cd = new CardData(
            1,
            "IT IS YOUR BIRTHDAY", 
            "Spend all Workforce: Gain twice that much Morale", 
            new SUIT[] {SUIT.Labour}, 
            null, 
            (cardGO) => { 
                PlayerManager.Instance.CurrentHitpoints += PlayerManager.Instance.CurrentMana * 2;
                PlayerManager.Instance.CurrentMana = 0;
             }
        );
        GainablePlayerCards.Add( cd );
        StartingPlayerCards.Add ( cd );

        ///////////////////////////////////////////// Non-Starters

        cd = new CardData(
            1,
            "Focus on Theory", 
            "Change all Engineering card suits to Science", 
            new SUIT[] {SUIT.Engineering}, 
            null, //(cgo) => { return PlayerManager.Instance.CurrentMana >= 2; }, 
            (cgo) => { 
                //PlayerManager.Instance.CurrentMana -= 2; 
                ChangeSuits(SUIT.Engineering, SUIT.Science); } );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            1,
            "Overqualified", 
            "2 Workforce: Change all Science card suits to Labour", 
            new SUIT[] {SUIT.Science}, 
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 2; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentMana -= 2; 
                ChangeSuits(SUIT.Science, SUIT.Labour); } );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            1,
            "Cheap Diplomas", 
            "2 Workforce: Change all Labour card suits to Science", 
            new SUIT[] {SUIT.Science}, 
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 2; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentMana -= 2; 
                ChangeSuits(SUIT.Labour, SUIT.Science); } );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            5,
            "Acquire Startup", 
            "Discard Hand: Solve one random task.", 
            new SUIT[] { }, 
            null,
            (cardGO) => { 
                PlayerManager.Instance.DiscardHand();
                SolveRandomTask();
             }
        );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            5,
            "Super Capacitors", 
            "Discard: Add 2 Temporary Power Cards", 
            new SUIT[] {SUIT.Science}, 
            null,
            (cardGO) => { 
                cardGO.Discard();
                PlayerManager.Instance.CreateTemporaryCard( PowerCard ); 
                PlayerManager.Instance.CreateTemporaryCard( PowerCard );                 
            }
        );
        //cd.IsRetained = true;
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            5,
            "Temporary Hires", 
            "Retained.\n5 Morale: Add 2 Workforce", 
            new SUIT[] {SUIT.Labour}, 
            (cgo) => { return PlayerManager.Instance.CurrentHitpoints > 5; }, 
            (cardGO) => { 
                PlayerManager.Instance.CurrentHitpoints -= 5;
                PlayerManager.Instance.CurrentMana += 2;
            }
        );
        cd.IsRetained = true;
        GainablePlayerCards.Add( cd );


        cd = new CardData(
            1,
            "Contingency Plan", 
            "Retained.\n2 Workforce: Draw 2 Cards", 
            new SUIT[] {SUIT.Labour}, 
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 2; }, 
            (cardGO) => { 
                PlayerManager.Instance.CurrentMana -= 2;
                PlayerManager.Instance.DrawCards(2);
            }
        );
        cd.IsRetained = true;
        GainablePlayerCards.Add( cd );


        /*cd = new CardData(
            10,
            "Perpetuate Motion", 
            "While in hand, power costs are decreased by 1", 
            new SUIT[] {SUIT.Science}, 
            null,
            null
        );
        GainablePlayerCards.Add( cd );*/

        cd = new CardData(
            1,
            "Automate Workforce", 
            "Discard: Draw a card, if card is Engineering, gain 2 Workforce", 
            new SUIT[] {SUIT.Labour}, 
            null,
            (cardGO) => {
                cardGO.Discard();
                List<CardGO> newCards = PlayerManager.Instance.DrawCards(1);
                if(newCards.Count > 0 && newCards[0].CardData.HasSuit(SUIT.Engineering, null))
                {
                    PlayerManager.Instance.CurrentMana += 2;
                }
            }
        );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            1,
            "Hiring Surge", 
            "Discard: Draw a card, if card is Labour, draw one more.", 
            new SUIT[] {SUIT.Labour}, 
            null,
            (cardGO) => {
                cardGO.Discard();
                List<CardGO> newCards = PlayerManager.Instance.DrawCards(1);
                //Debug.Log( newCards[0].CardData.Name + " " +  newCards[0].CardData.suits[0]);
                if(newCards.Count > 0 && newCards[0].CardData.HasSuit(SUIT.Labour, null))
                {
                    PlayerManager.Instance.DrawCards(1);
                }
            }
        );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            1,
            "Recycling", 
            "While in hand: Gain 1 Morale for each card you discard.", 
            new SUIT[] {SUIT.Engineering}, 
            null,
            null
        );
        cd.otherCardDiscarded = (cardGO) => { PlayerManager.Instance.CurrentHitpoints += 1; };
        GainablePlayerCards.Add( cd );


        cd = new CardData(
            1,
            "Sustainable Workflow", 
            "While in hand: Gain 1 Morale for each card you play.", 
            new SUIT[] {SUIT.Labour}, 
            null,
            null
        );
        cd.otherCardPlayed = (cardGO) => { PlayerManager.Instance.CurrentHitpoints += 1; };
        GainablePlayerCards.Add( cd );


        cd = new CardData(
            5,
            "Interdepartmental Synergy", 
            "3 Workforce: This card becomes Science, Engineering, and Labour", 
            new SUIT[] {  }, 
            (cgo) => { return PlayerManager.Instance.CurrentMana >= 3; }, 
            (cgo) => { 
                PlayerManager.Instance.CurrentMana -= 3; 
                AddSuit(cgo, SUIT.Science); AddSuit(cgo, SUIT.Engineering); AddSuit(cgo, SUIT.Labour); 
                }
        );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            1,
            "Unstable Battery", 
            "Add 2 Power to a random card in hand.", 
            new SUIT[] {  }, 
            null,
            (cgo) => { 
                CardGO c = RandomCardInHand();
                AddSuit(c, SUIT.Power);  
                AddSuit(c, SUIT.Power);  
                }
        );
        GainablePlayerCards.Add( cd );


        cd = new CardData(
            1,
            "Massage Chairs", 
            "Gain a Morale for each Engineering card in hand.", 
            new SUIT[] { SUIT.Labour }, 
            null,
            (cgo) => { 
                //cgo.Discard();
                PlayerManager.Instance.CurrentHitpoints += NumCardsWithSuit(SUIT.Engineering);
            } 
        );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            1,
            "Goobleboxes", 
            "Discard: Gain a temporary Power card for each Labour card in hand", 
            new SUIT[] { SUIT.Science }, 
            null,
            (cgo) => { 
                cgo.Discard();
                for(int i=0; i < NumCardsWithSuit(SUIT.Labour); i++)
                {
                    PlayerManager.Instance.CreateTemporaryCard( PowerCard ); 
                }
            } 
        );
        GainablePlayerCards.Add( cd );


        cd = new CardData(
            5,
            "Advanced Composites", 
            "Marketing decided that this sounded better than 'plastic'.", 
            new SUIT[] { SUIT.Engineering, SUIT.Science }, 
            null,
            null
        );
        GainablePlayerCards.Add( cd );


        cd = new CardData(
            5,
            "Power Loader", 
            "Great for moving boxes, or fighting alien queens.", 
            new SUIT[] { SUIT.Engineering, SUIT.Labour }, 
            null,
            null
        );
        GainablePlayerCards.Add( cd );


        cd = new CardData(
            5,
            "Work Smarter, Not Harder", 
            "", 
            new SUIT[] { SUIT.Labour, SUIT.Science }, 
            null,
            null
        );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            5,
            "VP of Science", 
            "", 
            new SUIT[] { SUIT.Science, SUIT.Science }, 
            null,
            null
        );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            5,
            "Engineering Director", 
            "", 
            new SUIT[] { SUIT.Engineering, SUIT.Engineering }, 
            null,
            null
        );
        GainablePlayerCards.Add( cd );

        cd = new CardData(
            5,
            "Head of HR", 
            "", 
            new SUIT[] { SUIT.Labour, SUIT.Labour }, 
            null,
            null
        );
        GainablePlayerCards.Add( cd );
    }

    static void ChangeSuits(SUIT from, SUIT to)
    {
        // Loop through all cards in hand, replacing "from" suit with "to" suit
        foreach(CardGO cardGO in PlayerManager.Instance.playerHand)
        {
            for(int i = 0; i < cardGO.cachedSuits.Count; i++)
            {
                if(cardGO.cachedSuits[i] == from)
                {
                    cardGO.cachedSuits[i] = to;
                }

                cardGO.UpdateCardInfo();
            }
        }

        PlayerManager.Instance.UpdateTotalSuitsInHand();
    }

    static void AddSuit(CardGO cardGO, SUIT newSuit)
    {
        cardGO.cachedSuits.Add(newSuit);
        cardGO.UpdateCardInfo();

        PlayerManager.Instance.UpdateTotalSuitsInHand();
    }

    static CardGO RandomCardInHand()
    {
        return PlayerManager.Instance.playerHand[ Random.Range(0, PlayerManager.Instance.playerHand.Count) ];
    }

    static int NumCardsWithSuit(SUIT s)
    {
        int c = 0;
        foreach(CardGO cardGO in PlayerManager.Instance.playerHand)
        {
            foreach(SUIT cardSuit in cardGO.cachedSuits)
            {
                if(cardSuit == s)
                {
                    c++;
                    break;  // Stop checking suits on this card
                }
            }
        }

        return c;
    }

    static void DiscardUnusedAndDraw( CardGO activeCard )
    {
        List<CardGO> cardsToDiscard = new List<CardGO>();
        foreach(CardGO card in PlayerManager.Instance.playerHand)
        {
            if(/*card != activeCard &&*/ card.ActionSpent == false)
            {
                cardsToDiscard.Add(card);
            }
        }

        foreach(CardGO card in cardsToDiscard)
        {
            card.Discard();
        }

        PlayerManager.Instance.DrawCards(cardsToDiscard.Count);
    }

    static void SolveRandomTask()
    {
        QuestGO[] quests = GameObject.FindObjectsOfType<QuestGO>();
        quests[ Random.Range(0, quests.Length)].ForceCompleteQuest( true );
    }
}