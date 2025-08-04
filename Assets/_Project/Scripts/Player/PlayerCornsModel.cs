using UnityEngine;

public class PlayerCornsModel : MonoBehaviour
{
    [SerializeField] private GameObject _cornsPrefab;
    [SerializeField] private Transform _cornsParent;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _offset;

    private ObjectPool<GameObject> _cornsPool;

    private void Awake()
    {
        
    }
}
