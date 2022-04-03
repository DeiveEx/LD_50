using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class RadialTest : MonoBehaviour
{
    [SerializeField] private Transform _cardPrefab;
    [SerializeField] private RadialLayout _radialLayout;

    private List<Transform> _cards = new List<Transform>();
    private int cardCount;

    private void Awake()
    {
        _radialLayout.onItemAddedSuccess += (sender, card) => Debug.Log("Card added");
        _radialLayout.onItemAddedFailed += (sender, card) => Debug.Log("Card could not be added");
        _radialLayout.onItemRemoved += (sender, card) => Debug.Log("Card removed");
    }

    private void Update()
    {
        if(Keyboard.current[Key.A].wasPressedThisFrame)
            AddCard();
        
        if(Keyboard.current[Key.S].wasPressedThisFrame)
            RemoveCard();
    }

    private void AddCard()
    {
        //Ideally we would check if the amount of cards already reached the limit, but I wanna test the events
        Transform card = Instantiate(_cardPrefab);

        if (_radialLayout.AddItem(card))
        {
            _cards.Add(card);
            
            //TODO remove later
            card.GetComponentInChildren<TMP_Text>().text = $"Card {cardCount++}";
        }
        else
        {
            Destroy(card.gameObject);
        }
    }

    private void RemoveCard()
    {
        if(_cards.Count == 0)
            return;

        int cardID = Random.Range(0, _cards.Count);
        _radialLayout.RemoveItem(_cards[cardID]);
        Destroy(_cards[cardID].gameObject);
        _cards.RemoveAt(cardID);
    }
}
