using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MatchRound
{
    public SORecipe roundRecipe;

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

        FillPlayerHand();
    }

    public void EndPlayerTurn()
    {
        if (amountOfTurnsLeft > 0)
            if (MatchManager.instance.Plate.IsDishCorrect())
            {
                EndRound();
                //Add points
            }
            else
            {
                StartPlayerTurn();
            }
        else
        {
            if (MatchManager.instance.Plate.IsDishCorrect())
            {
                //Add points
                Debug.Log("Congrats");
                EndRound();
            }
            else
            {
                GameplayMessage.instance.ShowMessageWithCallback("On no, your dish was cursed!", 1.0f, EndRound);
                MatchManager.instance.playerController.SickPeople(10);
            }
        }
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
