using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardGO : MonoBehaviour, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("CardGO " + gameObject.name + " was clicked.");
    }

    // Update is called once per frame
    void Update()
    {
        // Setup text etc... based on cardData

        if(CardData == null)
        {
            Debug.LogError("CardGO " + gameObject.name + " has null card data.");
            return;
        }

        txtTitle.text = CardData.Name;
        txtDescription.text = CardData.ActionDescription;
        txtSuits.text = CardData.GetSuitString();
    }
}
