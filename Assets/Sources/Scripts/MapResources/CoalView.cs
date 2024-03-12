using System;
using UnityEngine;

public class CoalView : MonoBehaviour
{
    public event Action<CoalView> Destroyed;
    private ResourcesPool _resourcesPool;

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
