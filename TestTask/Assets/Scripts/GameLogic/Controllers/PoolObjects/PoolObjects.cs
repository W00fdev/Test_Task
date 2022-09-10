using System.Collections.Generic;
using UnityEngine;


public class PoolObjects
{
    private List<IPoolable> _poolables;
    private Transform _container;
    private GameObject _prefab;

    private int _poolCount;
    private bool _autoExpand;

    public PoolObjects(GameObject prefab, Transform container, 
        int count, bool autoExpand = false)
    {
        _prefab = prefab;
        _container = container;
        _poolCount = count;
        _autoExpand = autoExpand;

        _poolables = new List<IPoolable>(_poolCount);
        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private IPoolable CreateObject(bool isActiveInHierarchy = false)
    {
        var createdObject = GameObject.Instantiate(_prefab, _container);
        createdObject.transform.localPosition = Vector3.zero;
        createdObject.SetActive(isActiveInHierarchy);
        
        IPoolable ipoolableCreated = createdObject.GetComponent<IPoolable>();

        _poolables.Add(ipoolableCreated);
        _poolCount++;

        return ipoolableCreated;
    }

    public bool HasFreeObject(out IPoolable poolable)
    {
        foreach (var element in _poolables)
        {
            GameObject goPoolable = element.GetTransform().gameObject;

            if (goPoolable.activeInHierarchy == false)
            {
                goPoolable.SetActive(true);
                poolable = element;
                return true;
            }
        }

        poolable = null;
        return false;
    }

    public IPoolable GetFreeObject()
    {
        if (HasFreeObject(out var poolable))
            return poolable;

        if (_autoExpand)
            return CreateObject(isActiveInHierarchy: true);

        throw new System.OverflowException("No free objects in pool.");
    }
}
