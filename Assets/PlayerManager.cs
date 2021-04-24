using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CardDataInitializer.Initialize();
        playerDeck = CardDataInitializer.StartingPlayerCards;

        ResetPlayer();
        NewTurn();
    }

    [System.NonSerialized]
    public int CurrentHitpoints;
    public int MaxHitpoints;

    [System.NonSerialized]
    public int CurrentMana;
    public int MaxMana;

    List<CardData> playerDeck;  // The cards in a player's deck in total
    List<CardData> playerDrawDeck;
    List<CardData> playerDiscardDeck;
    List<CardGO> playerHand;

    public GameObject CardGOPrefab;
    public Transform  PlayerHandParent;

    int CardDrawAmount = 5;

    void ResetPlayer()
    {
        CurrentHitpoints = MaxHitpoints;
        CurrentMana = MaxMana;
        playerDrawDeck = playerDeck.ToList();
        playerDiscardDeck = new List<CardData>();
        playerHand = new List<CardGO>();
    }

    void NewTurn()
    {
        CurrentMana = MaxMana;
        // Discard
        DiscardHand();
        // Draw
        DrawCards(CardDrawAmount);
    }

    void DiscardHand()
    {
        while(playerHand.Count > 0)
        {
            DiscardAt(0);
        }
    }

    void DiscardAt(int num)
    {
        CardGO cardgo = playerHand[num];
        playerDiscardDeck.Add(cardgo.CardData);

        cardgo.transform.SetParent(null); // Become Batman
        Destroy(cardgo.gameObject);
    }

    void DrawCards( int num )
    {
        for(int i = 0; i < num; i++)
        {
            CardData cd = playerDrawDeck[0];
            playerDrawDeck.RemoveAt(0);

            // Instantiate a new CardGO and link to the data
            GameObject cardGO = Instantiate(CardGOPrefab, Vector3.zero, Quaternion.identity, PlayerHandParent);
            cardGO.GetComponent<CardGO>().CardData = cd;
            playerHand.Add(cardGO.GetComponent<CardGO>());
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
