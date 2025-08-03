using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _sellingDuration = 1f;

    public float MoveSpeed => _moveSpeed;
    public float Acceleration => _acceleration;
    public float SellingDuration => _sellingDuration;
}
