using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        Debug.Log("New Round Started");
        //update dish board and stuff
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        Debug.Log("New Turn Started");
        amountOfTurnsLeft--;

        FillPlayerHand();
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

    private async void FillPlayerHand()
    {
        var playerHand = MatchManager.instance.playerController.cardHandHolder;
        while (playerHand.Cards.Count < playerHand.MaxCards)
        {
            Debug.Log("Adding card");
            MatchManager.instance.AddCardToPlayerHand();
            await Task.Delay(200);
        }
    }
}
