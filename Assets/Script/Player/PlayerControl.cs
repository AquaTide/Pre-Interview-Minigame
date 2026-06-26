using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public PartyCharacter CharacterInLead;
    public float moveSpeed = 5f;
    public Interaction objectToInteract;
    [SerializeField] private List<Interaction> _possibleInteractions;
    private ColliderMonitor _colliderMonitor;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private PlayerInput _playerInput;
    private InputAction _moveInput;
    private InputAction _interactInput;

    public float MoveDirX => _moveDirection.x;
    public float MoveDirY => _moveDirection.y;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _colliderMonitor = GetComponentInChildren<ColliderMonitor>();
        _moveInput = _playerInput.actions["Move"];
        _interactInput = _playerInput.actions["Interact"];
    }

    private void OnEnable()
    {
        EnableControl();
        _moveInput.performed += OnMove;
        _moveInput.canceled += OnStopMove;
        _interactInput.canceled += OnInteract;
        _colliderMonitor.onTriggerEnter += InteractTriggerEnter;
        _colliderMonitor.onTriggerExit += InteractTriggerExit;
    }

    private void OnDisable()
    {
        DisableControls();
        _moveInput.performed -= OnMove;
        _moveInput.canceled -= OnStopMove;
        _interactInput.canceled -= OnInteract;
        _colliderMonitor.onTriggerEnter -= InteractTriggerEnter;
        _colliderMonitor.onTriggerExit -= InteractTriggerExit;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    private void OnStopMove(InputAction.CallbackContext context)
    {
        _moveDirection = Vector2.zero;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        objectToInteract?.InteractWithObject(this);
    }

    public void EnableControl()
    {
        _moveInput.Enable();
        _interactInput.Enable();
    }

    public void DisableControls()
    {
        _moveInput.Disable();
        _interactInput.Disable();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDirection * moveSpeed;
    }

    private void InteractTriggerEnter(Collider2D other)
    {
        if (other.TryGetComponent<Interaction>(out var interactObject))
        {
            _possibleInteractions.Add(interactObject);
            objectToInteract = _possibleInteractions.Last();

            if(!interactObject.AutoEvent) return;
            objectToInteract?.InteractWithObject(this);
        }
    }

    private void InteractTriggerExit(Collider2D other)
    {
        if (other.TryGetComponent<Interaction>(out var interactObject))
        {
            _possibleInteractions.Remove(interactObject);
            if (_possibleInteractions.Count == 0)
            {
                objectToInteract = null;
                return;
            }

            objectToInteract = _possibleInteractions.Last();
        }
    }
}