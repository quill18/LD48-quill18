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
        cachedSuits = new List<SUIT>(CardData.suits).ToArray();
    }

    public CardData CardData;

    public SUIT[] cachedSuits;

    TextMeshProUGUI txtTitle;
    TextMeshProUGUI txtDescription;
    TextMeshProUGUI txtSuits;   // Is text?!?  Assuming we can do everything with unicode

    int origSiblingIndex=-1;

    bool actionSpent = false;

    public void Discard()
    {
        PlayerManager.Instance.Discard(this);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("CardGO " + gameObject.name + " was clicked.");

        if(actionSpent == true)
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
        actionSpent = true;
        PlayerManager.Instance.CardPlayed(this);
        UpdateCardInfo();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // TODO: Offset the card slightly?  Embiggen it?
        origSiblingIndex = transform.GetSiblingIndex();
        transform.SetSiblingIndex( transform.parent.childCount - 1 );
        transform.position = new Vector3(
            16,
            transform.position.y,
            transform.position.z
        );
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        StopHover();
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

        txtTitle.text = actionSpent ? "" : CardData.Name;
        txtDescription.text = actionSpent ? "" : CardData.Description;
        txtSuits.text = CardData.GetSuitString(cachedSuits);
    }
}
