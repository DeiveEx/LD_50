using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRound
{
    SORecipe roundRecipe;

    int amountOfTurnsLeft = 3;

    public SimpleCallEvent OnRoundEnd;

    public List<FAttributeSetup> ingredientsUsed;

    //CaldronSlot dishSlot // Uncomment here;
    public MatchRound(SORecipe dish, SimpleCallEvent onRoundEnd)
    {
        OnRoundEnd = onRoundEnd;
        if (dish != null)
        {
            roundRecipe = dish;
            amountOfTurnsLeft = dish.turns;
        }
    }

    public void StartRound()
    {   
        //update dish board and stuff
        GameplayMessage.instance.ShowMessageWithCallback("New Round Started", 1.0f, StartPlayerTurn);
    }

    public void StartPlayerTurn()
    {
        //Debug.Log();
        GameplayMessage.instance.ShowMessage("New Turn Started", 1.0f);
        amountOfTurnsLeft--;

        //TODO: Remove:
        //EndPlayerTurn();
    }

    public void EndPlayerTurn()
    {
        if (amountOfTurnsLeft > 0)
            StartPlayerTurn();
        else
            EndRound();

    }

    public void EndRound()
    {
        Debug.Log("Round ended");
        OnRoundEnd();
    }
}
