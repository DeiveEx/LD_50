using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRound
{
    DishActor roundDish;

    int amountOfRoundsLeft = 3;

    public SimpleCallEvent OnRoundEnd;

    //CaldronSlot dishSlot // Uncomment here;
    public MatchRound(DishActor dish, SimpleCallEvent onRoundEnd)
    {
        OnRoundEnd = onRoundEnd;
        if (dish != null)
        {
            roundDish = dish;
            amountOfRoundsLeft = dish.TurnsToCook;
        }
    }

    public void StartRound()
    {
        Debug.Log("New Round Started");
        //update dish board and stuff
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        Debug.Log("New Turn Started");
        amountOfRoundsLeft--;

        //TODO: Remove:
        EndPlayerTurn();
    }

    public void EndPlayerTurn()
    {
        if (amountOfRoundsLeft > 0)
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
