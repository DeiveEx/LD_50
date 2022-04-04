using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class RadialTest : MonoBehaviour
{
    [SerializeField] private Transform _cardPrefab;
    [SerializeField] private int _startAmount;
    [SerializeField] private RadialLayout _radialLayout;
    [SerializeField] private Transform _cardOrigin;

    private List<Transform> _cards = new List<Transform>();
    private int cardCount;

    private void Awake()
    {
        if (_cardOrigin == null)
            _cardOrigin = transform;
        
        _radialLayout.onItemAddedSuccess += (sender, card) => Debug.Log("Card added");
        _radialLayout.onItemAddedFailed += (sender, card) => Debug.Log("Card could not be added");
        _radialLayout.onItemRemoved += (sender, card) => Debug.Log("Card removed");
    }

    private void Start()
    {
        for (int i = 0; i < _startAmount; i++)
        {
            AddCard();
        }
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
        card.gameObject.SetActive(true);
        card.transform.position = _cardOrigin.position;

        if (_radialLayout.AddItem(card))
        {
            _cards.Add(card);
            
            //TODO remove later
            card.name = $"Card {cardCount++}";
            card.GetComponentInChildren<TMP_Text>().text = card.name;
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
