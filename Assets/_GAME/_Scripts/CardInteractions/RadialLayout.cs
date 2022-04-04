using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RadialLayout : MonoBehaviour, ICardHolder
{
    [SerializeField] private float _anglePivot;
    [SerializeField] private float _maxRange;
    [SerializeField] private Vector2 _ellipseSize;
    [SerializeField] private int _maxItems;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _animDuration;
    [SerializeField] private Ease _animEase;
    
    private List<CardActor> _cards = new List<CardActor>();
    private float _startAngle;
    private float _endAngle;

    public IList<CardActor> Cards => _cards;
    public int MaxItems
    {
        get => _maxItems;
        set => _maxItems = value;
    }

    public event EventHandler<CardActor> onCardAddedSuccess;
    public event EventHandler<CardActor> onCardRemoved;
    public event EventHandler<CardActor> onCardAddedFailed;

    public bool AddCard(CardActor card)
    {
        if (_cards.Count >= _maxItems)
        {
            onCardAddedFailed?.Invoke(this, card);
            return false;
        }
        
        _cards.Add(card);
        card.currentHolder = this;
        card.transform.SetParent(transform);
        UpdateCardPositions();
        onCardAddedSuccess?.Invoke(this, card);
        return true;
    }

    public bool RemoveCard(CardActor card)
    {
        if (_cards.Remove(card))
        {
            card.DOKill(); //TODO we might want to remove this if we want to have some other animation when removing cards
            UpdateCardPositions();
            onCardRemoved?.Invoke(this, card);
            return true;
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

    public void UpdateCardPositions()
    {
        //We want a single card to be centered, but more than one should be spread
        int count = _cards.Count <= 1 ? 0 : _cards.Count;
        
        //here we redefine the range of the spread accordingly to the amount of cards
        float currentRange = Mathf.Lerp(0, _maxRange, count / (float) _maxItems);
        _startAngle = _anglePivot - (currentRange / 2f);
        _endAngle = _startAngle + currentRange;
        
        //If we only have one card, we can't subtract or else we'd be trying to divide by zero
        int correction = _cards.Count <= 1 ? 0 : 1;
        float step = (_endAngle - _startAngle) / (float)(_cards.Count - correction);
        
        for (int i = 0; i < _cards.Count; i++)
        {
            var item = _cards[i];
            
            float cardAngleDeg = _startAngle + (step * i);
            float cardAngleRad = cardAngleDeg * Mathf.Deg2Rad;
            var cardOffset = new Vector3();
            cardOffset.x = Mathf.Cos(cardAngleRad) * _ellipseSize.x;
            cardOffset.y = Mathf.Sin(cardAngleRad) * _ellipseSize.y;
            cardOffset.z = _zOffset * i;
            // item.position = transform.position + cardOffset;
            // item.up = cardOffset.normalized;

            item.transform.DOMove(transform.position + cardOffset, _animDuration)
                .SetEase(_animEase)
                .Play();
            item.transform.DORotate(Quaternion.AngleAxis(cardAngleDeg - 90, Vector3.forward).eulerAngles, _animDuration)
                .SetEase(_animEase)
                .Play();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            UpdateCardPositions();
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            float currentRange = Mathf.Lerp(0, _maxRange, _cards.Count / (float) _maxItems);
            DrawArc(currentRange, Color.white, 5);
        }
        else
        {
            DrawArc(_maxRange, Color.white, 2);
        }
    }

    private void DrawArc(float range, Color color, float thickness)
    {
        Handles.color = color;
        int arcResolution = 20;

        float currentRange = range;
        float arcStart = _anglePivot - (currentRange / 2f);
        float arcEnd = arcStart + currentRange;
        
        float step = (arcEnd - arcStart) / (float)arcResolution;
        float previousAngle = arcStart;
        
        for (float angle = arcStart + step; angle < arcEnd + step; angle += step)
        {
            float angleRad = angle * Mathf.Deg2Rad;
            float previousAngleRad = previousAngle * Mathf.Deg2Rad;
            
            var startPoint = new Vector3(Mathf.Cos(previousAngleRad) * _ellipseSize.x, Mathf.Sin(previousAngleRad) * _ellipseSize.y);
            var endPoint = new Vector3(Mathf.Cos(angleRad) * _ellipseSize.x, Mathf.Sin(angleRad) * _ellipseSize.y);
            
            Handles.DrawLine(transform.position + startPoint, transform.position + endPoint, thickness);
            previousAngle = angle;
        }
    }
#endif
}
