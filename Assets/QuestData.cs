

public class QuestData 
{
    public QuestData(string name, string description, SUIT[] suits, int maxTurns = -1, QuestEventDelegate onSuccess=null, QuestEventDelegate onFail=null)
    {
        this.Name = name;
        this.Description = description;
        this.baseCost = suits;
        this.MaxTurns = maxTurns;

        this.onSuccess += onSuccess;
        this.onFail += onFail;
    }

    public string Name {get; private set;}
    public string Description {get; private set;} // Explain any ongoing / failure / success effects

    public int MaxTurns {get; private set;}

    public delegate void QuestEventDelegate(QuestGO questGO);

    event QuestEventDelegate onSuccess;
    event QuestEventDelegate onFail;      // i.e. timed out

    public delegate SUIT[] ModifyQuestCostDelegate(SUIT[] suits);
    ModifyQuestCostDelegate costModifier;

    SUIT[] baseCost;
    SUIT[] currentCost; // recalced whenever a quest is added/removed

    // Need a delegate that other quests call to see if their completability is affected by this ongoing

    bool isQuestBlocker = false; // This prevents other quests from being completed

    public bool IsCompletable( /* ref to player hand, ref to other active quests */ ) 
    {
        // Check other active quests for ongoing effects that modify/forbid this
        // For example, calculate a current cost (different from base suits)

        if(isQuestBlocker == false)
        {
            // Check if any other quest is a blocker, and if so return false

            // Note that if we are a blocker, we are always completable because this check is skipped
        }

        return true;
    }

    public void UpdateCurrentCost( QuestData[] quests )
    {
        // Recalc current cost based on ongoing quests and RELICS?!?!?!?
        // and cache result
        currentCost = baseCost;

        // Loop through each quest and see if it modifies our cost
        foreach(QuestData quest in quests)
        {
            if(quest.costModifier != null)
            {
                currentCost = quest.costModifier(currentCost); 
            }
        }
    }

    public SUIT[] GetCurrentCost()
    {
        return currentCost;
    }

    public void DoFail(QuestGO questGO)
    {
        if(onFail != null)
        {
            onFail(questGO);
        }
    }

    public void DoSuccess(QuestGO questGO)
    {
        if(onSuccess != null)
        {
            onSuccess(questGO);
        }
    }
}
