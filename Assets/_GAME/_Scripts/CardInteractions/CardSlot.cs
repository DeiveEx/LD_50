using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSlot : MonoBehaviour, ICardHolder
{
    [SerializeField] private int _maxCards = 1;
    [SerializeField] private float _spacing;
    [SerializeField] private float _hoverScale = 1;
    [SerializeField] private Vector3 _hoverOffset = new Vector3(0, 0, -5);
    [SerializeField] private float cardZPosition;
    [SerializeField] private float _animDuration;
    [SerializeField] private Ease _animEase;
    [SerializeField] private bool _allowGrabbing;

    private List<CardActor> _cards = new List<CardActor>();
    
    public IList<CardActor> Cards => _cards;
    public float HoverScale => _hoverScale;
    public Vector3 HoverOffset => _hoverOffset;
    public bool AllowGrabbing => _allowGrabbing;
    public int MaxCards => _maxCards;

    public bool AddCard(CardActor card)
    {
        if (_cards.Count >= _maxCards)
            return false;
        
        _cards.Add(card);
        card.transform.SetParent(transform);
        card.currentHolder = this;
        UpdateCardPositions();
        OnCardAdded(card);
        return true;
    }

    public bool RemoveCard(CardActor card)
    {
        if (_cards.Remove(card))
        {
            UpdateCardPositions();
            OnCardRemoved(card);
        }

        return false;
    }

    public CardActor GetCardAtPosition(int position)
    {
        if (position >= _cards.Count)
            return null;

        var card = _cards[position];
        return card;
    }
    
    public virtual void UpdateCardPositions()
    {
        float range = (_cards.Count - 1) * _spacing;
        
        for (int i = 0; i < _cards.Count; i++)
        {
            var card = _cards[i];
            var offset = new Vector3(
                (_spacing * i) - (range / 2f),
                0,
                cardZPosition + (.1f * i));
            
            card.transform.DOMove(transform.position + offset, _animDuration)
                .SetEase(_animEase)
                .Play();
        }
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
}