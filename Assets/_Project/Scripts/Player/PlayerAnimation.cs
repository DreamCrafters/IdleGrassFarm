using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _meshTransform;
    [SerializeField] private float _rotationSpeed = 10f;

    private void Update()
    {
        HandleMovementAnimation();
        RotateMesh();
    }

    private void HandleMovementAnimation()
    {
        Vector3 velocity = _playerMovement.CurrentVelocity;
        _animator.SetFloat(SpeedHash, velocity.magnitude);
    }

    private void RotateMesh()
    {
        if (_playerMovement.CurrentVelocity.sqrMagnitude > 0.01f)
        {
            Vector3 direction = _playerMovement.CurrentVelocity.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            _meshTransform.rotation = Quaternion.Slerp(_meshTransform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
