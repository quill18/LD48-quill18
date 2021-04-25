using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    private void Awake() {
        extraSuitCost = new List<SUIT>();
    }

    // Start is called before the first frame update
    void Start()
    {
        onPlayerHandCountChanged += UpdateTotalSuitsInHand;

        CardDataLibrary.Initialize();
        playerDeck = CardDataLibrary.StartingPlayerCards;

        ResetPlayer();
        NewTurn();
    }

    private void OnDestroy() {
        _Instance = null;
    }

    public Color[] CardTints;

    static PlayerManager _Instance;
    public static PlayerManager Instance {
        get {
            if(_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<PlayerManager>();
            }
            return _Instance;
        }
    }

    public int CurrentHitpoints {get; set;}
    public int MaxHitpoints;

    public int CurrentMana {get; set;}
    public int MaxMana;

    List<CardData> playerDeck;  // The cards in a player's deck in total
    List<CardData> playerDrawDeck;
    List<CardData> playerDiscardDeck;
    public List<CardGO> playerHand;

    public GameObject CardGOPrefab;
    public GameObject CardGOPrefabPower;
    public Transform  PlayerHandParent;

    public int CardDrawAmount = 5;

    public int IgnoreNextBlocker = 0;

    List<SUIT> extraSuitCost;   // An extra cost for all quests
    List<SUIT> suitDiscount;   // An extra cost for all quests

    Dictionary<SUIT, int> totalSuitsInHand;

    public delegate void PlayerHandCountChangedDelegate();
    public event PlayerHandCountChangedDelegate onPlayerHandCountChanged;
    public event PlayerHandCountChangedDelegate onTotalSuitsChanged;


    public delegate void CardPlayedDelegate(CardGO cardGO);
    public event CardPlayedDelegate onCardPlayed;

    public void AddExtraSuit( SUIT s )
    {
        extraSuitCost.Add(s);
        if(onTotalSuitsChanged != null)
            onTotalSuitsChanged();
    }

    public void RemoveExtraSuit( SUIT s )
    {
        for(int i = 0; i < extraSuitCost.Count; i++)
        {
            if(extraSuitCost[i] == s)
            {
                extraSuitCost.RemoveAt(i);
                return;
            }
        }
        if(onTotalSuitsChanged != null)
            onTotalSuitsChanged();
    }

    public SUIT[] ModifiedSuitCost( SUIT[] cardCost )
    {
        List<SUIT> modSuits = extraSuitCost.Concat(cardCost).ToList();

        /*foreach(SUIT d in suitDiscount)
        {

        }*/

        return modSuits.ToArray();
    }

    public void CardPlayed(CardGO cardGO)
    {
        if(onCardPlayed != null)
        {
            onCardPlayed(cardGO);
        }

        // Loop through other cards in hand
        foreach(CardGO c in playerHand)
        {
            if(c != cardGO)
            {
                if(c.CardData.otherCardPlayed != null)
                {
                    c.CardData.otherCardPlayed(cardGO);
                }
            }
        }
    }

    void ResetPlayer()
    {
        CurrentHitpoints = MaxHitpoints;
        CurrentMana = MaxMana;
        playerDrawDeck = playerDeck.ToList();
        playerDiscardDeck = new List<CardData>();
        playerHand = new List<CardGO>();
        ShuffleDrawDeck();
    }

    void ShuffleDiscardIntoDrawDeck()
    {
        playerDrawDeck.AddRange(playerDiscardDeck);
        playerDiscardDeck.Clear();
        ShuffleDrawDeck();
    }

    void ShuffleDrawDeck()
    {
        List<CardData> newPlayerDrawDeck = new List<CardData>();

        while(playerDrawDeck.Count > 0)
        {
            int r = Random.Range(0, playerDrawDeck.Count);
            CardData cd = playerDrawDeck[r];
            playerDrawDeck.RemoveAt(r);
            newPlayerDrawDeck.Add(cd);
        }

        playerDrawDeck = newPlayerDrawDeck;
    }

    public void NewTurn()
    {
        CurrentMana = MaxMana;
        // Discard
        DiscardHand( true );
        // Draw
        DrawCards(CardDrawAmount);

        Debug.Log("NewTurn - Deck: " + playerDrawDeck.Count + " Discard: " + playerDiscardDeck.Count);

    }

    public void DiscardHand( bool keepRetained = false )
    {
        int handSize = playerHand.Count;
        for( int i = handSize-1; i >= 0; i-- )
        {
            DiscardAt(i, true, keepRetained);
        }

        Debug.Log("DiscardHand - Deck: " + playerDrawDeck.Count + " Discard: " + playerDiscardDeck.Count);
    }

    void DiscardAt(int num, bool discardingHand = false, bool keepRetained = false)
    {
        CardGO cardGO = playerHand[num];

        // Only retain cards if they are unspent.
        if(cardGO.ActionSpent == false && cardGO.CardData.IsRetained == true && keepRetained == true)
        {
            return;
        }

        playerHand.RemoveAt(num);

        if(cardGO.IsTemporary == false)
        {
            playerDiscardDeck.Add(cardGO.CardData);
        }

        cardGO.transform.SetParent(null); // Become Batman
        Destroy(cardGO.gameObject);

        if(onPlayerHandCountChanged != null)
            onPlayerHandCountChanged();

        if(discardingHand == false)
        {
            // Loop through other cards in hand
            foreach(CardGO c in playerHand)
            {
                if(c != cardGO)
                {
                    if(c.CardData.otherCardDiscarded != null)
                    {
                        c.CardData.otherCardDiscarded(cardGO);
                    }
                }
            }
        }
            
    }

    public void Discard(CardGO cgo)
    {
        for(int i = 0; i < playerHand.Count; i++)
        {
            if(playerHand[i] == cgo)
            {
                DiscardAt(i);
                return;
            }
        }
    }

    public void DiscardOneWithSuit( SUIT s )
    {
        for(int i = 0; i < playerHand.Count; i++)
        {
            if(playerHand[i].CardData.HasSuit(s, playerHand[i].cachedSuits.ToArray()))
            {
                DiscardAt(i);
                return;
            }
        }

    }

    public void CreateTemporaryCard(CardData cd)
    {
        if(PlayerHandParent.transform.childCount >= 10)
        {
            Debug.Log("Hand is full!");
            return;
        }

        // Instantiate a new CardGO and link to the data
        GameObject pf = CardGOPrefab;
        if(cd.suits[0] == SUIT.Power)
            pf = CardGOPrefabPower; // use the power card art

        GameObject cardGO = Instantiate(pf, PlayerHandParent);

        cardGO.GetComponent<CardGO>().IsTemporary = true;

        Image img = cardGO.GetComponent<Image>();
        img.color = CardTints[ (int)cd.suits[0] ];  // Tint the card frame based on icon.

        cardGO.GetComponent<CardGO>().CardData = cd;
        cardGO.GetComponent<CardGO>().UpdateCachedSuits();
        playerHand.Add(cardGO.GetComponent<CardGO>());

        if(onPlayerHandCountChanged != null)
            onPlayerHandCountChanged();
    }

    public List<CardGO> DrawCards( int num )
    {
        List<CardGO> newCards = new List<CardGO>();

        for(int i = 0; i < num; i++)
        {
            if(PlayerHandParent.transform.childCount >= 10)
            {
                Debug.Log("Hand is full!");
                return newCards;
            }

            if(playerDrawDeck.Count <= 0)
            {
                ShuffleDiscardIntoDrawDeck();

                if(playerDrawDeck.Count <= 0)
                {
                    Debug.Log("Player literally has no more cards.");
                    if(onPlayerHandCountChanged != null)
                        onPlayerHandCountChanged();

                    return newCards;
                }
            }

            CardData cd = playerDrawDeck[0];
            playerDrawDeck.RemoveAt(0);

            // Instantiate a new CardGO and link to the data
            GameObject pf = CardGOPrefab;
            if(cd.suits[0] == SUIT.Power)
                pf = CardGOPrefabPower; // use the power card art

            GameObject cardGO = Instantiate(pf, PlayerHandParent);

            //Image img = cardGO.transform.Find("Frame").GetComponent<Image>();
            Image img = cardGO.transform.GetComponent<Image>();
            img.color = CardTints[ (int)cd.suits[0] ];  // Tint the card frame based on icon.

            cardGO.GetComponent<CardGO>().CardData = cd;
            cardGO.GetComponent<CardGO>().UpdateCachedSuits();
            playerHand.Add(cardGO.GetComponent<CardGO>());
        }

        if(onPlayerHandCountChanged != null)
            onPlayerHandCountChanged();

        return newCards;
    }

    public void TakeQuestOverflowDamage()
    {
        // TODO: Visual damage indicator

        CurrentHitpoints -= GameManager.Instance.CurrentLevel;
    }

    public void UpdateTotalSuitsInHand()
    {
        //Debug.Log("UpdateTotalSuitsInHand");
        totalSuitsInHand = new Dictionary<SUIT, int>();

        foreach(CardGO c in playerHand)
        {
            foreach(SUIT s in c.cachedSuits)
            {
                if(totalSuitsInHand.ContainsKey(s) == false)
                {
                    totalSuitsInHand.Add(s, 0);
                }

                totalSuitsInHand[s]++;
            }
        }

        if(onTotalSuitsChanged != null)
            onTotalSuitsChanged();
    }

    public bool QuestIsBlocked(QuestData questData)
    {
        if(IgnoreNextBlocker > 0)
        {
            return false;
        }

        // If this quest is not a blocker, but there is a blocker quest in play, return false.
        if(questData.isQuestBlocker == true)
        {
            return false; // Blockers can't be blocked
        }

        QuestGO[] qgos = GameObject.FindObjectsOfType<QuestGO>();
        foreach(QuestGO qgo in qgos)
        {
            QuestData qd = qgo.QuestData;
            if(qd.isQuestBlocker == true)
            {
                return true;   // cannot complete this quest due to a blocker
            }
        }

        return false;
    }

    public bool CanCompleteQuest(QuestGO questGO, bool ignoreBlockers = false)
    {
        QuestData questData = questGO.QuestData;

        Dictionary<SUIT, int> suitsRequired = new Dictionary<SUIT, int>();

        // If this quest is not a blocker, but there is a blocker quest in play, return false.
        if(ignoreBlockers == false && questData.isQuestBlocker == false)
        {
            if( questGO.QuestIsStackBlocked() )
            {
                return false;  // We are stack blocked, can't complete
            }

            // Loop through all the quests to find blockers
            QuestGO[] qgos = GameObject.FindObjectsOfType<QuestGO>();
            foreach(QuestGO qgo in qgos)
            {

                QuestData qd = qgo.QuestData;
                if(qd.isQuestBlocker == true)
                {
                    return false;   // cannot complete this quest due to a blocker
                }
            }
        }

        foreach(SUIT s in ModifiedSuitCost(questData.suits))
        {
            if(suitsRequired.ContainsKey(s) == false)
                suitsRequired[s] = 0;

            suitsRequired[s]++;
        }

        int neededWildcards = 0;
        foreach(SUIT s in suitsRequired.Keys)
        {
            if(suitsRequired[s] > 0)
            {
                if( totalSuitsInHand.ContainsKey(s) == false )
                {
                    neededWildcards += suitsRequired[s];
                }
                else if (suitsRequired[s] > totalSuitsInHand[s])
                {
                    neededWildcards += suitsRequired[s] - totalSuitsInHand[s];
                }
            }
        }

        if(neededWildcards > 0)
        {
            // Right now, we aren't implenting wildcards -- so this just means the quest can't be completed
            return false;
        }

        return true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
