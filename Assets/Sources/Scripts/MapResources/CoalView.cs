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
        Destroyed.Invoke(this);
        _resourcesPool.Remove(this);
        Destroy(gameObject);
    }
}
