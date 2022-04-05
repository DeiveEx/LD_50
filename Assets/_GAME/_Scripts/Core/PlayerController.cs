using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
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

    public TMP_Text text;

    private int _sickness = 0;

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

    public void SetSickness(int Value)
    {
        _sickness = Value;
        text.text = "SICKNESS: 100" + _sickness.ToString() + "/100";
    }
    public void SickPeople(int Amount)
    {
        _sickness += Amount;
        _sickness = Mathf.Min(_sickness, 100);
        text.text = "SICKNESS: " + _sickness.ToString() + "/100";
        if(_sickness == 100)
        {
            GameplayMessage.instance.ShowMessageWithCallback("Everybody is sick now :c", 2.0f, EndGame);
        }
    }
    
    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}
