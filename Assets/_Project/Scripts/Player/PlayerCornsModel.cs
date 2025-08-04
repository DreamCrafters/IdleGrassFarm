using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCornsModel : MonoBehaviour
{
    private readonly List<Transform> _corns = new();

    [SerializeField] private GameObject _cornsPrefab;
    [SerializeField] private Transform _cornsParent;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _cornMoveDuration = 0.1f;
    [SerializeField] private float _cornBounceHeight = 5;

    private ObjectPool<Transform> _cornsPool;

    public event Action OnCornSelled;

    private void Awake()
    {
        _cornsPool = new ObjectPool<Transform>(_cornsPrefab.transform, _cornsParent);
    }

    public void SpawnCorn(Vector3 fromPosition)
    {
        Transform corn = _cornsPool.Get();
        Vector3 toPosition = _startPosition + _offset * _corns.Count;
        corn.position = fromPosition;
        corn.parent = null;
        _corns.Add(corn);
        StartCoroutine(MoveCorn(corn, fromPosition, () => toPosition + _cornsParent.position, () => corn.parent = _cornsParent));
    }

    public void ReleaseCorn(Vector3 toPosition)
    {
        if (_corns.Count > 0)
        {
            Transform corn = _corns[^1];
            corn.parent = null;
            _corns.RemoveAt(_corns.Count - 1);
            StartCoroutine(MoveCorn(corn, corn.position,
                () => toPosition,
                () =>
                {
                    _cornsPool.Release(corn);
                    OnCornSelled?.Invoke();
                }));
        }
    }

    private IEnumerator MoveCorn(Transform corn, Vector3 fromPosition, Func<Vector3> toPosition, Action actionOnEnd)
    {
        float elapsedTime = 0f;
        float duration = _cornMoveDuration;
        float bounceHeight = _cornBounceHeight;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            Vector3 currentPosition = Vector3.Lerp(fromPosition, toPosition(), progress);
            
            float bounceOffset = Mathf.Sin(progress * Mathf.PI) * bounceHeight;
            currentPosition.y += bounceOffset;
            
            corn.position = currentPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        corn.position = toPosition();
        actionOnEnd?.Invoke();
    }
}
