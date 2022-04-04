using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Struct to match Attributes with values.
/// </summary>
[System.Serializable]
public struct FAttributeSetup
{
    public ECardAttribute Attribute;
    public int Value;
}

/// <summary>
/// Card Actor is a class that carries all card setup information. It should
/// be used as main component to all cards that exists in the game. 
/// </summary>
public class CardActor : MonoBehaviour, IHoverable, IGrabable
{
     
    [Header("Prefab References")]

    //Reference to the UI Image Object
    public Image _cardImageObj;

    //Reference to the Card's Name's UI Text Object
    public TMP_Text _nameTextObj;

    //Reference to the Card's Descr�ption's UI Text Object
    public TMP_Text _descTextObj;
    
    public GameObject _visual;
    
    public Collider _collider;

    [Header("Card Setup")]
    public string CardName;

    public Sprite CardImage;

    public string Description;
    public float hoverAnimDuration;
    public float grabAnimDuration;
    public ICardHolder currentHolder;

    public List<FAttributeSetup> Attributes;

    //Reference the the player this Card belongs.
    private PlayerController _owner;

    private Tween _hoverTween;
    private bool _isBeingHovered;
    private Tween _grabTween;

    /// <summary>
    /// Return the player that owns this card. (This can be used to create special effects that requires player actions like draw a card.
    /// </summary>
    /// <returns>Owning Player if any</returns>
    public PlayerController GetOwner()
    {
        return _owner;
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if(CardImage != null)
                _cardImageObj.sprite = CardImage;
            
            if(!string.IsNullOrEmpty(CardName))
                _nameTextObj.text = CardName;
            
            if(!string.IsNullOrEmpty(Description))
                _descTextObj.text = Description;
        }
    }

    public void OnStartHover()
    {
        if(_isBeingHovered)
            return;
        
        if (_hoverTween != null)
            _hoverTween.Kill();

        _isBeingHovered = true;
        var sequence = DOTween.Sequence();
        sequence
            .Append(_visual.transform.DOScale(Vector3.one * currentHolder.HoverScale, hoverAnimDuration));
            // .Insert(0, _visual.transform.DOLocalMove(new Vector3(0, 1, -5), hoverAnimDuration));
            
        _hoverTween = sequence.Play();
    }

    public void OnEndHover()
    {
        if (_hoverTween != null)
        {
            _hoverTween.Kill();
        }

        _isBeingHovered = false;
        var sequence = DOTween.Sequence();
        sequence
            .Append(_visual.transform.DOScale(Vector3.one, hoverAnimDuration));
            // .Insert(0, _visual.transform.DOLocalMove(Vector3.zero, hoverAnimDuration));
            
        _hoverTween = sequence.Play();
    }

    public void OnGrab()
    {
        OnEndHover();
        
        _grabTween = transform
            .DORotate(Quaternion.identity.eulerAngles, grabAnimDuration)
            .Play();

        _collider.enabled = false;
    }

    public void OnRelease()
    {
        _grabTween.Kill();
        _collider.enabled = true;
    }
}
