using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
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

    int CardDrawAmount = 5;

    Dictionary<SUIT, int> totalSuitsInHand;

    public delegate void PlayerHandCountChangedDelegate();
    public event PlayerHandCountChangedDelegate onPlayerHandCountChanged;
    public event PlayerHandCountChangedDelegate onTotalSuitsChanged;


    public delegate void CardPlayedDelegate(CardGO cardGO);
    public event CardPlayedDelegate onCardPlayed;

    public void CardPlayed(CardGO cardGO)
    {
        if(onCardPlayed != null)
        {
            onCardPlayed(cardGO);
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
        DiscardHand();
        // Draw
        DrawCards(CardDrawAmount);

        Debug.Log("NewTurn - Deck: " + playerDrawDeck.Count + " Discard: " + playerDiscardDeck.Count);

    }

    void DiscardHand()
    {
        while(playerHand.Count > 0)
        {
            DiscardAt(0);
        }

        Debug.Log("DiscardHand - Deck: " + playerDrawDeck.Count + " Discard: " + playerDiscardDeck.Count);
    }

    void DiscardAt(int num)
    {
        CardGO cardgo = playerHand[num];
        playerHand.RemoveAt(num);
        playerDiscardDeck.Add(cardgo.CardData);

        cardgo.transform.SetParent(null); // Become Batman
        Destroy(cardgo.gameObject);

        if(onPlayerHandCountChanged != null)
            onPlayerHandCountChanged();
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
            if(playerHand[i].CardData.HasSuit(s, playerHand[i].cachedSuits))
            {
                DiscardAt(i);
                return;
            }
        }

    }

    public void DrawCards( int num )
    {
        for(int i = 0; i < num; i++)
        {
            if(playerDrawDeck.Count <= 0)
            {
                ShuffleDiscardIntoDrawDeck();

                if(playerDrawDeck.Count <= 0)
                {
                    Debug.Log("Player literally has no more cards.");
                    if(onPlayerHandCountChanged != null)
                        onPlayerHandCountChanged();

                    return;
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

    public bool CanCompleteQuest(QuestData questData, bool ignoreBlockers = false)
    {
        Dictionary<SUIT, int> suitsRequired = new Dictionary<SUIT, int>();

        // If this quest is not a blocker, but there is a blocker quest in play, return false.
        if(ignoreBlockers == false && questData.isQuestBlocker == false)
        {
            // TODO: Loop through all the quests to find blockers
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

        foreach(SUIT s in questData.suits)
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
