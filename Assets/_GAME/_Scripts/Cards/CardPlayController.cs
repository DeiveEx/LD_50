using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardPlayController : MonoBehaviour
{
    [SerializeField] private LayerMask cardMask;
    [SerializeField] private RadialLayout _radialLayout;
    [SerializeField] private float _cardFollowSpeed;
    [SerializeField] private float _animDuration;

    private Transform _heldCard;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
            if (Physics.Raycast(ray, out RaycastHit hit, 100, cardMask))
            {
                Debug.Log(hit.collider.name);
                _heldCard = hit.transform;
                _heldCard.DORotate(Quaternion.identity.eulerAngles, _animDuration);
            }
        }

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
                targetPos.z = _heldCard.position.z;

                _heldCard.position = Vector3.Lerp(_heldCard.position, targetPos, _cardFollowSpeed * Time.deltaTime);
            }
        }
    }
}
