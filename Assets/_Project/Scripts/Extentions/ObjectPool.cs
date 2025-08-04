using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Queue<T> _objects = new();
    private readonly Transform _parent;

    public ObjectPool(T prefab, Transform parent, int initialSize = 0)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    public T Get()
    {
        if (_objects.Count == 0)
        {
            CreateObject();
        }

        T obj = _objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        _objects.Enqueue(obj);
    }

    private void CreateObject()
    {
        T newObject = Object.Instantiate(_prefab, _parent);
        newObject.gameObject.SetActive(false);
        _objects.Enqueue(newObject);
    }
}