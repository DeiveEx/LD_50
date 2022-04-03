using System.Collections;
using System.Collections.Generic;
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
[ExecuteInEditMode]
public class CardActor : MonoBehaviour
{
     
    [Header("Prefab References")]

    //Reference to the UI Image Object
    public Image _cardImageObj;

    //Reference to the Card's Name's UI Text Object
    public TMP_Text _nameTextObj;

    //Reference to the Card's Descríption's UI Text Object
    public TMP_Text _descTextObj;

    [Header("Card Setup")]
    public string CardName;

    public Sprite CardImage;

    public string Description;

    public List<FAttributeSetup> Attributes;

    //Reference the the player this Card belongs.
    private PlayerController _owner;

    /// <summary>
    /// Return the player that owns this card. (This can be used to create special effects that requires player actions like draw a card.
    /// </summary>
    /// <returns>Owning Player if any</returns>
    public PlayerController GetOwner()
    {
        return _owner;
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            _cardImageObj.sprite = CardImage;
            _nameTextObj.text = CardName;
            _descTextObj.text = Description;
        }
    }
}
