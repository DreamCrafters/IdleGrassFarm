using UnityEngine;

public class SellArea : MonoBehaviour
{
    [SerializeField] private Transform _sellPoint;

    public Transform SellPoint => _sellPoint;
}
