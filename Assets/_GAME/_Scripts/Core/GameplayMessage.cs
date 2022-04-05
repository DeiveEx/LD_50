using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This is the main class of Gameplay Message system.
/// With GameplayMessages you can trigger a message anywhere by just
/// calling "ShowMessage" or "ShowMessageWithCallback"
/// 
/// </summary>
public class GameplayMessage : Singleton<GameplayMessage>
{
    [Header("UI Elements Reference")]
    public TMP_Text _textElement;
    private Animator _animController;

    private SimpleCallEvent _onFinish;

    protected override void Awake()
    {
        base.Awake();
        _animController = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public Animator GetAnim()
    {
        return _animController;
    }

    /// <summary>
    /// Displays a message for a specific amount of time
    /// </summary>
    /// <param name="message">The Body of the message</param>
    /// <param name="duration">Amount of time should be displayed</param>
    public void ShowMessage(string message, float duration)
    {
        gameObject.SetActive(true);
        _animController.Play("FadeIn");
        _textElement.text = message;
        StartCoroutine(EndMessage(duration, false));
    }

    /// <summary>
    /// Displays a message for a specific amount of time, closes it and triggers the callback.
    /// </summary>
    /// <param name="message">The Body of the message</param>
    /// <param name="duration">Amount of time should be displayed</param>
    /// <param name="onMessageOut">Callback Function ref. (void void)</param>
    public void ShowMessageWithCallback(string message, float duration, SimpleCallEvent onMessageOut)
    {
        gameObject.SetActive(true);
        _animController.Play("FadeIn");
        _onFinish = null;
        _onFinish += onMessageOut;
        _textElement.text = message;
        StartCoroutine(EndMessage(duration, true));
    }

    IEnumerator EndMessage(float duration, bool showCallback)
    {
        yield return new WaitForSeconds(duration);
        _animController.Play("FadeOut");
        yield return new WaitForSeconds(0.50f);
        gameObject.SetActive(false);
        if(showCallback)
        {
            _onFinish();
        }    
    }


}
