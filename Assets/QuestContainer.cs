using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
