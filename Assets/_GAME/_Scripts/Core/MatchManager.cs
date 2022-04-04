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

    private PlayerController _player;

    private MatchStage currentStage;

    [Header("Gameplay Framework Templates")]
    public PlayerController PlayerPrefab;

    [Header("Gameplay Objects")]
    //TODO: This object references could be store in a data structure class. (GlobalSettings-like class?)
    public List<DishActor> Dishes;
    public List<CardActor> Cards;

    public PlayerController GetPlayerController()
    {
        return _player;
    }

    public void Start()
    {
        _player = Instantiate(PlayerPrefab);
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





}
