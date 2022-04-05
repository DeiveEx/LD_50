using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SimpleCallEvent();

/// <summary>
/// Match manager is a singleton which responsabilities are:
///     - To manage the match. 
///     - Manage the match phases (carry events, delegates, player locks), 
///     - Object spawning and pooling, 
///     - Handle turn management and end game
///     - Pause the game
///     - Spawn global messages feedbacks;
/// </summary>
public class MatchManager : Singleton<MatchManager>
{
    [SerializeField]
    public MatchStage currentStage;

    [Header("Gameplay Framework Templates")]
    public PlayerController playerController;
    public RadialLayout cardHandHolder;
    public Transform deckOrigin;
    public PlateSlot Plate;

    [Header("Gameplay Objects")]
    //TODO: This object references could be store in a data structure class. (GlobalSettings-like class?)
    public List<SORecipe> Recipes;

    public void Start()
    {
        playerController.cardHandHolder = cardHandHolder;
        StartMatch();
    }

    public bool CheckEndGame()
    {
        return false;
    }

    public void StartMatch()
    {
        currentStage = new MatchStage();
        currentStage.OnStageEnd += this.EndMatch;
        currentStage.StartStage();
    }

    public void EndMatch()
    {
        //Pick new stage? Go to next level? Switch Level? Go To menu?
    }

    public MatchStage GetCurrentStage()
    {
        return currentStage;
    }

    public MatchRound GetCurrentRound()
    {
       return currentStage.GetCurrentRound();
    }

    public void EndTurn()
    {
        currentStage.EndTurn();
    }

    public bool AddCardToPlayerHand()
    {
        if (playerController.cardHandHolder.Cards.Count >= playerController.cardHandHolder.MaxCards)
            return false;
        
        int cardId = Random.Range(0, playerController.Deck.Count);
        
        var card = Instantiate(playerController.Deck[cardId]);
        card.gameObject.SetActive(true);
        card.transform.position = deckOrigin.position;
        playerController.cardHandHolder.AddCard(card);
        
        return true;
    }
}
