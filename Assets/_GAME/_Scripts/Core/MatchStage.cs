using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStage
{
    List<DishActor> dishes;

    MatchRound currentRound;

    public SimpleCallEvent OnStageEnd;

    //TODO: This props are redundant, could be only one
    int roundCount;
    int amountOfRoundsLeft = 3;

    public void StartStage()
    {
        Debug.Log("A new day comes, it's sunny outside, time to cook");
        if(amountOfRoundsLeft > 0)
        {
            StartNewRound();
        }
    }

    public void StartNewRound()
    {
        amountOfRoundsLeft--;
        currentRound = new MatchRound(null, EndRound);
        currentRound.StartRound();
        roundCount++;
    }

    public void EndRound()
    {
        if(amountOfRoundsLeft > 0)
            StartNewRound();
        else
            EndStage();
    }

    public void StartShop()
    {
        //Show Shop UI
    }

    public void EndShop()
    {
        //Hide Shop UI
    }

    public void EndStage()
    {
        Debug.Log("Stage Ended");
        //Call Owner EndStage
        //OnStageEnd();
        OnStageEnd.Invoke();
    }

    public MatchRound GetCurrentRound()
    {
        return currentRound;
    }


}
