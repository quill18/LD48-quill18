using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewCardSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Show();
    }

    public Transform CardHolder;

    public void Show()
    {
        // Populate the three card options
        ClearCards();

        List<CardData> validCards = new List<CardData>();

        foreach(CardData cd in CardDataLibrary.GainablePlayerCards)
        {
            if(cd.Level <= GameManager.Instance.CurrentLevel)
            {
                validCards.Add(cd);
            }
        }

        validCards = validCards.OrderBy( (cd) => { return Random.Range(0, 1000000); } ).ToList();

        CardGO cardGO;
        cardGO = PlayerManager.Instance.GenerateCardGOFromDeck(validCards);
        cardGO.IsCardPicking = true;
        cardGO.transform.SetParent(CardHolder);
        cardGO.transform.localScale = Vector3.one;

        cardGO = PlayerManager.Instance.GenerateCardGOFromDeck(validCards);
        cardGO.IsCardPicking = true;
        cardGO.transform.SetParent(CardHolder);
        cardGO.transform.localScale = Vector3.one;

        List<CardData> powerList = new List<CardData>();
        powerList.Add( CardDataLibrary.PowerCard );

        cardGO = PlayerManager.Instance.GenerateCardGOFromDeck( powerList );
        cardGO.IsCardPicking = true;
        cardGO.transform.SetParent(CardHolder);
        cardGO.transform.localScale = Vector3.one;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void ClearCards()
    {
        while(CardHolder.childCount > 0)
        {
            Transform c = CardHolder.GetChild(0);
            c.SetParent(null); // Literally every superhero and disney character
            Destroy(c.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
