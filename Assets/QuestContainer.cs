using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            myQuestPool = QuestDataLibrary.DrillQuestData;
        }
    }

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
        for(int i = 0; i < NumQuestsNewLevel; i++)
        {
            AddQuest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddQuest()
    {
        if(transform.childCount >= maxQuests)
        {
            Debug.Log("Full on quests.");
            return;
        }

        QuestData qd = myQuestPool[ Random.Range(0, myQuestPool.Count) ];

        QuestGO questGO = Instantiate(QuestGOPrefab, this.transform).GetComponent<QuestGO>();
        if(questGO == null)
        {
            Debug.LogError("da fuck?");
            return;
        }

        questGO.QuestData = qd;
    }
}
