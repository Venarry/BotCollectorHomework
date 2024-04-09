using System;
using UnityEngine;

public class CoalView : MonoBehaviour, ITarget
{
    public event Action<ITarget> Destroyed;
    private ResourcesPool _resourcesPool;

    public Transform Transform => transform;

    public void Init(ResourcesPool resourcesPool)
    {
        _resourcesPool = resourcesPool;
        _resourcesPool.Add(this);
    }

    public void Destroy()
    {
        _resourcesPool.Remove(this);
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
