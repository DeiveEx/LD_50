using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardPlayController : MonoBehaviour
{
    [SerializeField] private LayerMask cardMask;

    private Transform heldCard;
    
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
            if (Physics.Raycast(ray, out RaycastHit hit, 100, cardMask))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
