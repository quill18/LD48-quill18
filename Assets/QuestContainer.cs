using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestContainer : MonoBehaviour
{
    private void Awake() {
        GameManager.Instance.onNewShift += NewShift;
        GameManager.Instance.onNewLevel += NewLevel;
    }

    private void OnDestroy() {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.onNewShift -= NewShift;
            GameManager.Instance.onNewLevel -= NewLevel;
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        QuestDataLibrary.Initialize();

        if(QuestClass == QUESTCLASS.SURFACE)
        {
            myQuestPool = QuestDataLibrary.SurfaceQuestData;
        }
        else
        {
            //myQuestPool = QuestDataLibrary.DrillQuestData;
        }
    }

    public GameObject QuestOverflowWarning;

    public enum QUESTCLASS { SURFACE, DRILL }
    public QUESTCLASS QuestClass;

    List<QuestData> myQuestPool;

    public GameObject QuestGOPrefab;

    public int NumQuestsNewLevel;
    public int NumQuestsNewShift;

    int maxQuests = 4;

    void NewShift()
    {
        for(int i = 0; i < NumQuestsNewShift; i++)
        {
            AddQuest();
        }
    }

    void NewLevel()
    {
        if(QuestClass == QUESTCLASS.DRILL)
        {
            myQuestPool = QuestDataLibrary.GetDrillQuestsForLevel(GameManager.Instance.CurrentLevel);
        }

        for(int i = 0; i < NumQuestsNewLevel; i++)
        {
            AddQuest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(QuestOverflowWarning != null) 
        {
            if(transform.childCount >= 4)
            {
                QuestOverflowWarning.SetActive(true);
            }
            else
            {
                QuestOverflowWarning.SetActive(false);
            }
        }
    }

    public void AddQuest()
    {
        if(transform.childCount >= maxQuests)
        {
            Debug.Log("Full on quests.");
            PlayerManager.Instance.TakeQuestOverflowDamage();
            return;
        }

        QuestData qd;
        
        if(QuestClass == QUESTCLASS.SURFACE)
        {
            if(QuestDataLibrary.ForcedFirstQuest.Count > 0)
            {
                qd = QuestDataLibrary.ForcedFirstQuest[0];
                QuestDataLibrary.ForcedFirstQuest.RemoveAt(0);
            }
            else
            {
                List<QuestData> filteredQuests = myQuestPool.Where( 
                    (qd) => { return qd.Level <= GameManager.Instance.CurrentLevel; }  
                    ).ToList();
                qd = filteredQuests[ Random.Range(0, filteredQuests.Count) ];
            }
        }
        else
        {
            qd = myQuestPool[0];
            myQuestPool.RemoveAt(0);
        }

        QuestGO questGO = Instantiate(QuestGOPrefab, this.transform).GetComponent<QuestGO>();
        if(questGO == null)
        {
            Debug.LogError("da fuck?");
            return;
        }

        questGO.QuestData = qd;

        PlayerManager.Instance.ForceUpdateQuests();
    }
}
