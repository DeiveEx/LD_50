using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player controller is the class that represents the player in the world. 
/// 
/// It should keep all the specific player related data needed by the game logic,
/// Handle all base input functionality.
/// 
/// Player Controller should manage itself UI as well
/// </summary>
public class PlayerController : MonoBehaviour
{
    public List<CardActor> Deck;

    public int Cash;

    public int Popularity;

    public ICardHolder cardHandHolder;

    private bool bLocked;

    public void DrawCard()
    {
        //Draw Card Logic
    }

    public void PassTurn()
    {
        //Pass turn logic
    }

    public void BuyCard(CardActor card)
    {
        //buy card logic;
    }

}
