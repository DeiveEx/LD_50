using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


///This is the base class for all special effects a card can have.
///It should include active and passive effects and a way to specify
///the time that the effects happen.
///

public enum EEffectPhase
{
    EP_BeforeCooking,
    EP_AfterCooking,
    EP_AfterEvaluation,
    EP_OnEveryTurn,
    EP_Manual
}

/// <summary>
/// A CardEffect is a generic way of describing an effect functionallity 
/// as a component. Implement the Activation method.
/// </summary>
public abstract class CardEffect : MonoBehaviour
{
    public EEffectPhase EffectPhase;

    //The implementation of the functionality of the card.
    public virtual void Activation()
    {
        //Do nothing
    }
}
