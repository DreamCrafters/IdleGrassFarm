using System;
using UnityEngine;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private readonly PlayerConfig _playerConfig;
    [Inject] private readonly FarmConfig _farmConfig;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private Vector3 _currentVelocity;
    private Camera _playerCamera;

    public Vector3 CurrentVelocity => _currentVelocity;

    public event Action<Collider> OnTriggerEnterEvent;
    public event Action<Collider> OnTriggerExitEvent;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }

    private void Update()
    {
        HandleMovementInput();
        ProcessBounds();
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = _playerCamera.transform.forward;
        Vector3 cameraRight = _playerCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        _moveDirection = cameraForward * vertical + cameraRight * horizontal;

        Vector3 targetVelocity = _moveDirection.normalized * _playerConfig.MoveSpeed;

        _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, _playerConfig.Acceleration * Time.deltaTime);

        if (_currentVelocity.magnitude >= 0.1f)
        {
            _rigidbody.MovePosition(_rigidbody.position + _currentVelocity * Time.deltaTime);
        }
    }

    private void ProcessBounds()
    {
        Vector3 position = transform.position;
        Vector2 gridSize = _farmConfig.GridSize;
        Vector2 cellSize = _farmConfig.CellSize;

        float halfGridWidth = gridSize.x * cellSize.x / 2f;
        float halfGridHeight = gridSize.y * cellSize.y / 2f;

        position.x = Mathf.Clamp(position.x, -halfGridWidth, halfGridWidth);
        position.z = Mathf.Clamp(position.z, -halfGridHeight, halfGridHeight);

        transform.position = position;
    }
}
