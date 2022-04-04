using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private int _maxCards = 1;
    [SerializeField] private float _spacing;
    [SerializeField] private List<CardActor> _cards = new List<CardActor>();

    public bool TryAddCardToSlot(CardActor card)
    {
        if (_cards.Count >= _maxCards)
            return false;
        
        _cards.Add(card);
        card.transform.SetParent(transform);
        OnCardAdded(card);
        UpdateCardPositions();
        return true;
    }

    public CardActor GetCardFromSlot(int id = 0)
    {
        if (id >= _cards.Count)
            return null;

        var card = _cards[id];
        _cards.RemoveAt(id);
        OnCardRemoved(card);
        UpdateCardPositions();
        return card;
    }

    protected virtual void OnCardAdded(CardActor card)
    {
        Debug.Log($"Card [{card.name}] added to slot [{this.name}]");
        //Do something...
    }

    protected virtual void OnCardRemoved(CardActor card)
    {
        Debug.Log($"Card [{card.name}] removed to slot [{this.name}]");
        //Do something...
    }

    protected virtual void UpdateCardPositions()
    {
        float range = _cards.Count * _spacing;
        
        for (int i = 0; i < _cards.Count; i++)
        {
            var card = _cards[i];
            var offset = new Vector3(
                (_spacing * i) - (range / 2f),
                0,
                .1f * i);
            card.transform.position = transform.position + offset;
        }
    }
}