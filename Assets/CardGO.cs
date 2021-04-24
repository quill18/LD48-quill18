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
        txtTitle        = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        txtDescription  = transform.Find("Action Description").GetComponent<TextMeshProUGUI>();
        txtSuits        = transform.Find("Suits").GetComponent<TextMeshProUGUI>();
    }

    public CardData CardData;

    TextMeshProUGUI txtTitle;
    TextMeshProUGUI txtDescription;
    TextMeshProUGUI txtSuits;   // Is text?!?  Assuming we can do everything with unicode

    int origSiblingIndex=-1;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("CardGO " + gameObject.name + " was clicked.");
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
        transform.SetSiblingIndex( origSiblingIndex );
        transform.position = new Vector3(
            0,
            transform.position.y,
            transform.position.z
        );

    }

    // Update is called once per frame
    void Update()
    {
        // Setup text etc... based on cardData

        if(CardData == null)
        {
//            Debug.LogError("CardGO " + gameObject.name + " has null card data.");
            return;
        }

        txtTitle.text = CardData.Name;
        txtDescription.text = CardData.ActionDescription;
        txtSuits.text = CardData.GetSuitString();
    }
}
