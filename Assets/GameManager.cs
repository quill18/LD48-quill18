using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NewLevel();
    }

    private void OnDestroy() {
        _Instance = null;
    }

    static GameManager _Instance;
    public static GameManager Instance {
        get {
            if(_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _Instance;
        }
    }

    public delegate void GenericAction();
    public event GenericAction onNewLevel;
    public event GenericAction onNewShift;

    readonly public int MaxLevel = 18;
    public int CurrentLevel { get; protected set; }

    public QuestContainer SurfaceQuestContainer;
    public QuestContainer DrillQuestContainer;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewSurfaceQuest()
    {
        SurfaceQuestContainer.AddQuest();
    }

    public void QuestWasCompleted()
    {
        // Update the drill graphic

        // Did we finish the last drilling quest and need to go to the next level?
        if( DrillQuestContainer.transform.childCount == 0 )
        {
            // No more drilling quests!
            NewLevel();
        }
    }

    public void NewLevel()
    {
        // Update scores, trigger visuals, etc...

        CurrentLevel++;

        if(onNewLevel != null)
            onNewLevel();
    }

    public void EndShift()
    {
        Debug.Log("End Shift");
        
        PlayerManager.Instance.NewTurn();

        QuestGO[] qgos = GameObject.FindObjectsOfType<QuestGO>();
        foreach(QuestGO qgo in qgos)
        {
            // Advance quest timers
            qgo.AdvanceTime();
            // Trigger all onShiftEnd
            qgo.QuestData.DoShiftEnd(qgo);
        }

        if(onNewShift!= null)
            onNewShift();

    }


}
