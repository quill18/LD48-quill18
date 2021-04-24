using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGO : MonoBehaviour
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

        turnsLeft = QuestData.MaxTurns;
    }

    public QuestData QuestData;
    int turnsLeft;

    // Update is called once per frame
    void Update()
    {
        
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

        if(turnsLeft <= 0)
        {
            QuestData.DoFail(this);

            // Destroy the GAMEOBJECT representation
            Destroy(gameObject);
        }
    }

}
