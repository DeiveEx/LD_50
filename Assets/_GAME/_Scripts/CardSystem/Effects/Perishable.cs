using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Perishable Handles effects in three turns.
/// </summary>
public class Perishable : CardEffect
{
    public int AmountOfTurns = 3; 
    protected int turnsPassed;

    public UnityEvent OnPerish;

    public override void Activation()
    {
        turnsPassed++;
        if (turnsPassed == AmountOfTurns)
            OnPerish.Invoke();
    }

}
