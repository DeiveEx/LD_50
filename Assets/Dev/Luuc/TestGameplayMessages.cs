using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Test calls to gameplay messages.
/// </summary>
public class TestGameplayMessages : MonoBehaviour
{
    private void Update()
    {
        if(Keyboard.current.aKey.wasPressedThisFrame)
        {

            GameplayMessage.instance.ShowMessage("Hello darling", 3.0f);
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {

            GameplayMessage.instance.ShowMessageWithCallback("Delegate", 3.0f, Callback);
        }
    }

    public void Callback()
    {
        print("CALLED BACK BRO");
    }
}
