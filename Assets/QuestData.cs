

public class QuestData : CardBase
{
    public QuestData(string name, string description, SUIT[] suits, int maxTurns = -1)
    {
        this.Name = name;
        this.Description = description;
        this.baseCost = suits;
        this.MaxTurns = maxTurns;

        this.suits = suits;

    }

    public int MaxTurns {get; private set;}

    public delegate void QuestEventDelegate(QuestGO questGO);

    public event QuestEventDelegate onEnter;
    public event QuestEventDelegate onExit;

    public event QuestEventDelegate onSuccess;
    public event QuestEventDelegate onShiftEnd;
    public event QuestEventDelegate onFail;      // i.e. timed out

    public delegate SUIT[] ModifyQuestCostDelegate(SUIT[] suits);
    ModifyQuestCostDelegate costModifier;

    SUIT[] baseCost;
    SUIT[] currentCost; // recalced whenever a quest is added/removed

    // Need a delegate that other quests call to see if their completability is affected by this ongoing

    public bool isQuestBlocker = false; // This prevents other quests from being completed

    public bool IsBlocked( /* ref to player hand, ref to other active quests */ ) 
    {
        // Check other active quests for ongoing effects that modify/forbid this
        // For example, calculate a current cost (different from base suits)

        if(isQuestBlocker == false)
        {
            // Check if any other quest is a blocker, and if so return false

            // Note that if we are a blocker, we are always completable because this check is skipped
        }

        return false;
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

    public void DoEnter(QuestGO questGO)
    {
        if(onEnter != null)
            onEnter(questGO);
    }

    public void DoExit(QuestGO questGO)
    {
        if(onExit != null)
            onExit(questGO);
    }

    public void DoFail(QuestGO questGO)
    {
        if(onFail != null)
        {
            onFail(questGO);
        }
    }

    public void DoShiftEnd(QuestGO questGO)
    {
        if(onShiftEnd != null)
        {
            onShiftEnd(questGO);
        }
    }

    public void DoSuccess(QuestGO questGO)
    {
        if(onSuccess != null)
        {
            onSuccess(questGO);
        }

        // Remove any power we used.
        foreach(SUIT s in suits)
        {
            if(s == SUIT.Power)
            {
                PlayerManager.Instance.DiscardOneWithSuit( s );
            }
        }

        GameManager.Instance.QuestWasCompleted();
    }


}
