using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardGO : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        txtTitle        = transform.Find("Frame/Title").GetComponent<TextMeshProUGUI>();
        txtDescription  = transform.Find("Frame/Action Description").GetComponent<TextMeshProUGUI>();
        txtSuits        = transform.Find("Frame/Suits").GetComponent<TextMeshProUGUI>();

        UpdateCardInfo();
    }

    public void UpdateCachedSuits()
    {
        cachedSuits = new List<SUIT>(CardData.suits);
    }

    public CardData CardData;

    public List<SUIT> cachedSuits;

    TextMeshProUGUI txtTitle;
    TextMeshProUGUI txtDescription;
    TextMeshProUGUI txtSuits;   // Is text?!?  Assuming we can do everything with unicode

    int origSiblingIndex=-1;

    public bool ActionSpent {get; protected set;} = false;

    public bool IsTemporary = false;

    [System.NonSerialized]
    public bool IsCardPicking = false;

    public void Discard()
    {
        PlayerManager.Instance.Discard(this);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("CardGO " + gameObject.name + " was clicked.");

        if(IsCardPicking)
        {
            DoCardPick();            
            return;
        }

        if(ActionSpent == true)
        {
            Debug.Log("Already spent!");
            return;
        }

        // Can we afford this action?

        // Zhu-li, do the thing!
        if( CardData.DoAction( this ) == false )
        {
            // Action didn't happen
            return;
        }
        ActionSpent = true;
        PlayerManager.Instance.CardPlayed(this);
        UpdateCardInfo();
    }

    void DoCardPick()
    {
        // This card was selected during the end of level card-adder phase
        PlayerManager.Instance.AddToDiscard(CardData);

        GameObject.FindObjectOfType<NewCardSelector>().Hide();
        GameManager.Instance.NewLevel();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // TODO: Offset the card slightly?  Embiggen it?
        if(IsCardPicking)
        {
            // Embiggen
            transform.localScale = Vector3.one * 1.1f;
        }
        else
        {
            origSiblingIndex = transform.GetSiblingIndex();
            transform.SetSiblingIndex( transform.parent.childCount - 1 );
            transform.position = new Vector3(
                16,
                transform.position.y,
                transform.position.z
            );
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(IsCardPicking)
        {
            transform.localScale = Vector3.one;
        }
        else {
            StopHover();
        }
    }

    public void StopHover()
    {
        if(origSiblingIndex < 0)
            return;
        transform.SetSiblingIndex( origSiblingIndex );
        origSiblingIndex = -1;
        transform.position = new Vector3(
            0,
            transform.position.y,
            transform.position.z
        );

    }

    public void UpdateCardInfo()
    {
        // Setup text etc... based on cardData

        if(CardData == null)
        {
            Debug.LogError("CardGO " + gameObject.name + " has null card data.");
            return;
        }

        txtTitle.text = ActionSpent ? "USED" : CardData.Name;
        txtDescription.text = ( IsTemporary ? "*Temporary*\n" : "" ) + (ActionSpent ? "USED" : CardData.Description);
        txtSuits.text = CardData.GetSuitString(cachedSuits.ToArray());
    }
}
