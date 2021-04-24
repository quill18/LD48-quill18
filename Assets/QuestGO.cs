using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QuestGO : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        if(QuestData == null)
        {
            Debug.LogError("QuestData not set before first update.");
            this.enabled = false;
            return;
        }

        cardBackground = GetComponent<Image>();

        PlayerManager = GameObject.FindObjectOfType<PlayerManager>();

        PlayerManager.onPlayerHandCountChanged += UpdateCompletabilityness;
        PlayerManager.onTotalSuitsChanged += UpdateCompletabilityness;
        UpdateCompletabilityness();


        turnsLeft = QuestData.MaxTurns;

        txtTitle        = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        txtDescription  = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        txtSuits        = transform.Find("Suits").GetComponent<TextMeshProUGUI>();

        UpdateCardInfo();

        QuestData.DoEnter(this);
    }

    public QuestData QuestData;
    int turnsLeft;

    TextMeshProUGUI txtTitle;
    TextMeshProUGUI txtDescription;
    TextMeshProUGUI txtSuits;   // Is text?!?  Assuming we can do everything with unicode

    PlayerManager PlayerManager;
    Image cardBackground;

    private void OnDestroy() {
        QuestData.DoExit(this);

        PlayerManager.onPlayerHandCountChanged -= UpdateCompletabilityness;
        PlayerManager.onTotalSuitsChanged -= UpdateCompletabilityness;

        foreach(QuestGO questGO in GameObject.FindObjectsOfType<QuestGO>())
        {
            questGO.UpdateCompletabilityness();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateCompletabilityness()
    {
        if( PlayerManager.CanCompleteQuest(QuestData) )
        {
            cardBackground.color = Color.green;
        }
        else
        {
            cardBackground.color = Color.white; //new Color32(166, 166, 166, 255);
        }
    }

    void UpdateCardInfo()
    {
        // Setup text etc... based on cardData

        if(QuestData == null)
        {
            Debug.LogError("QuestGO " + gameObject.name + " has null card data.");
            return;
        }

        txtTitle.text = QuestData.Name;
        txtDescription.text = QuestData.Description;
        txtSuits.text = QuestData.GetSuitString(null);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Debug.Log("QuestGO " + gameObject.name + " was clicked.");

        if( PlayerManager.CanCompleteQuest(QuestData) == false )
        {
            return;
        }

        QuestData.DoSuccess( this );

        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale = Vector3.one;
    }

    public void AdvanceTime() 
    {
        // The instance (GameObject) of this quest 

        if(QuestData.MaxTurns < 0)
        {
            // Not a timed quest
            return;
        }

        turnsLeft--;
        UpdateCardInfo();

        if(turnsLeft <= 0)
        {
            QuestData.DoFail(this);

            // Destroy the GAMEOBJECT representation
            Destroy(gameObject);
        }

    }

}
