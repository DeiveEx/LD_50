using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField] private LayerMask _cardMask;
    [SerializeField] private LayerMask _slotMask;
    [SerializeField] private float _cardFollowSpeed;
    [SerializeField] private float _heldPositionZ;

    private GameObject _lastHit;
    private CardActor _heldCard;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (_heldCard != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ClickWhileHoldingCard();
            }
            else
            {
                MakeCardFollowMouse();
            }
        }
        else
        {
            CheckIfMouseAboveCard();

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ClickWithoutCard();
            }
        }
    }

    private void ClickWithoutCard()
    {
        if (_lastHit != null &&
            _lastHit.TryGetComponent(out CardActor card) &&
            card.currentHolder.AllowGrabbing)
        {
            _heldCard = card;
            TryDoGrab(_heldCard.gameObject, true);
            _lastHit = null;
        }
    }

    private void CheckIfMouseAboveCard()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100, _cardMask))
        {   
            if (_lastHit != hit.collider.gameObject)
            {
                TryDoHover(_lastHit, false);
                _lastHit = hit.collider.gameObject;
                TryDoHover(_lastHit, true);
            }
        }
        else
        {
            if (_lastHit != null)
            {
                TryDoHover(_lastHit, false);
            }
            
            _lastHit = null;
        }
    }

    private void MakeCardFollowMouse()
    {
        Vector3 targetPos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        targetPos.z = _heldPositionZ;

        _heldCard.transform.position = Vector3.Lerp(_heldCard.transform.position, targetPos, _cardFollowSpeed * Time.deltaTime);
    }

    private void ClickWhileHoldingCard()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        var previousCardHolder = _heldCard.currentHolder;
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _slotMask) &&
            hit.collider.TryGetComponent(out ICardHolder holder) &&
            holder.AddCard(_heldCard))
        {
            previousCardHolder.RemoveCard(_heldCard);
            TryDoGrab(_heldCard.gameObject, false);
            _heldCard = null;
        }
        else
        {
            TryDoGrab(_heldCard.gameObject, false);
            _heldCard.currentHolder.UpdateCardPositions();
            _heldCard = null;
        }
    }

    private void TryDoHover(GameObject obj, bool isHovering)
    {
        if (obj != null && obj.TryGetComponent(out IHoverable hoverObj))
        {
            if (isHovering)
            {
                hoverObj.OnStartHover();
            }
            else
            {
                hoverObj.OnEndHover();
            }
        }
    }

    private void TryDoGrab(GameObject obj, bool isGrabbing)
    {
        if (obj != null && obj.TryGetComponent(out IGrabable hoverObj))
        {
            if (isGrabbing)
            {
                hoverObj.OnGrab();
            }
            else
            {
                hoverObj.OnRelease();
            }
        }
    }
}