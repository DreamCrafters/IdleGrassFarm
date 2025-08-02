using UnityEngine;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private readonly GroundSpawnerService _groundSpawnerService;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _acceleration = 10f;

    private Vector3 _moveDirection;
    private Vector3 _currentVelocity;
    private Camera _playerCamera;

    private void Start()
    {
        _playerCamera = Camera.main;
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

        Vector3 targetVelocity = _moveDirection.normalized * _moveSpeed;

        _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, _acceleration * Time.deltaTime);

        if (_currentVelocity.magnitude >= 0.1f)
        {
            transform.Translate(_currentVelocity * Time.deltaTime, Space.World);
        }
    }

    private void ProcessBounds()
    {
        Vector3 position = transform.position;
        Vector2 gridSize = _groundSpawnerService.GridSize;
        Vector2 cellSize = _groundSpawnerService.CellSize;

        float halfGridWidth = gridSize.x * cellSize.x / 2f;
        float halfGridHeight = gridSize.y * cellSize.y / 2f;

        position.x = Mathf.Clamp(position.x, -halfGridWidth, halfGridWidth);
        position.z = Mathf.Clamp(position.z, -halfGridHeight, halfGridHeight);

        transform.position = position;
    }
}
