using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadialTest : MonoBehaviour
{
    [SerializeField] private Transform _cardPrefab;
    [SerializeField] private RadialLayout _radialLayout;

    private List<Transform> _cards = new List<Transform>();
    private int cardCount;
    
    private void Update()
    {
        if(Keyboard.current[Key.A].wasPressedThisFrame)
            AddCard();
        
        if(Keyboard.current[Key.S].wasPressedThisFrame)
            RemoveCard();
    }

    private void AddCard()
    {
        Transform card = Instantiate(_cardPrefab);
        _radialLayout.AddItem(card);
        _cards.Add(card);
        
        //TODO remove later
        card.GetComponentInChildren<TMP_Text>().text = $"Card {cardCount++}";
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
