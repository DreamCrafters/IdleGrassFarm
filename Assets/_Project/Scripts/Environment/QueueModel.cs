using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class QueueModel : MonoBehaviour
{
    private readonly int SpeedKey = Animator.StringToHash("Speed");
    private readonly List<QueueItem> _queue = new();

    [Inject] private readonly PlayerCornsModel _playerCornsModel;

    [SerializeField] private List<Transform> _positions = new();
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 1f;

    private void Awake()
    {
        for (int i = 0; i < _positions.Count; i++)
        {
            Transform position = _positions[i];
            Transform prefab = Instantiate(_prefab, transform).transform;
            prefab.SetPositionAndRotation(position.position, position.rotation);
            _queue.Add(new QueueItem(position, prefab));
        }
    }

    private void OnEnable()
    {
        _playerCornsModel.OnCornSelled += MoveQueue;
    }

    private void OnDisable()
    {
        _playerCornsModel.OnCornSelled -= MoveQueue;
    }

    private void Update()
    {
        foreach (QueueItem item in _queue)
        {
            if (item.Owner.position != item.Position.position ||
                item.Owner.rotation != item.Position.rotation)
            {
                Vector3 direction = item.Position.position - item.Owner.position;

                if (direction == Vector3.zero)
                {
                    direction = item.Position.forward;
                }

                item.Owner.SetPositionAndRotation(
                    Vector3.MoveTowards(item.Owner.position, item.Position.position, _moveSpeed * Time.deltaTime),
                    Quaternion.RotateTowards(item.Owner.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime));
                item.Animator.SetFloat(SpeedKey, _moveSpeed);
            }
            else
            {
                item.Animator.SetFloat(SpeedKey, 0f);
            }
        }
    }

    public void MoveQueue()
    {
        Transform[] positions = _queue.ConvertAll(item => item.Position).ToArray();

        for (int i = 0; i < _queue.Count; i++)
        {
            int nextIndex = (i - 1 + positions.Length) % positions.Length;
            _queue[i].Position = positions[nextIndex];
        }
    }

    private class QueueItem
    {
        public Transform Position;
        public Transform Owner;
        public Animator Animator;

        public QueueItem(Transform position, Transform owner)
        {
            Position = position;
            Owner = owner;
            Animator = owner.GetComponent<Animator>();
        }
    }
}