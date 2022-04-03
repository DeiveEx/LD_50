using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField] private LayerMask cardMask;
    [SerializeField] private RadialLayout _radialLayout;
    [SerializeField] private float _cardFollowSpeed;
    [SerializeField] private float _animDuration;
    [SerializeField] private float _heldZ;

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
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                _heldCard.DOKill();
                _heldCard = null;
                _radialLayout.UpdateItemPositions();
            }
            else
            {
                Vector3 targetPos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                targetPos.z = _heldZ;

                _heldCard.transform.position = Vector3.Lerp(_heldCard.transform.position, targetPos, _cardFollowSpeed * Time.deltaTime);
            }
        }
        else
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
    
            if (Physics.Raycast(ray, out RaycastHit hit, 100, cardMask))
            {   
                if (_lastHit != hit.collider.gameObject)
                {
                    TryDoHover(_lastHit, false);
                    _lastHit = hit.collider.gameObject;
                    TryDoHover(_lastHit, true);
                }
            
                if (Mouse.current.leftButton.wasPressedThisFrame && hit.collider.TryGetComponent(out CardActor card))
                {
                    _heldCard = card;
                    TryDoHover(_heldCard.gameObject, false);
                    _heldCard.transform
                        .DORotate(Quaternion.identity.eulerAngles, _animDuration)
                        .Play();
                }
            }
            else
            {
                if (_lastHit != null)
                {
                    TryDoHover(_lastHit, false);
                    _lastHit = null;
                }
            }
        }
    }

    private void TryDoHover(GameObject obj, bool startHover)
    {
        if (obj != null && obj.TryGetComponent(out IHoverable hoverObj))
        {
            if (startHover)
            {
                hoverObj.OnStartHover();
            }
            else
            {
                hoverObj.OnEndHover();
            }
        }
    }
}
