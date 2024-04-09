using System;
using System.Collections.Generic;

public class ScannedResourcesProvider
{
    private readonly List<ITarget> _scannedResources = new();
    private readonly List<ITarget> _collectingResources = new();

    public int CoalsCount => _scannedResources.Count;

    public event Action<ITarget> ScannedResourceAdded;
    public event Action<ITarget> ScannedResourceRemoved;

    public event Action<ITarget> CollectingResourceAdded;
    public event Action<ITarget> CollectingResourceDestroyed;

    public ScannedResourcesProvider()
    {
    }

    public void Add(ITarget resource)
    {
        if (_scannedResources.Contains(resource) || _collectingResources.Contains(resource))
            return;

        _scannedResources.Add(resource);
        resource.Destroyed += OnScannedResourceDestroy;
        ScannedResourceAdded?.Invoke(resource);
    }

    public bool TryTake(out ITarget resource)
    {
        resource = null;

        if (_scannedResources.Count == 0)
            return false;

        resource = _scannedResources[0];
        resource.Destroyed -= OnScannedResourceDestroy;
        ScannedResourceRemoved?.Invoke(resource);
        _scannedResources.RemoveAt(0);

        _collectingResources.Add(resource);
        CollectingResourceAdded?.Invoke(resource);
        resource.Destroyed += OnCollectingResourceDestroy;

        return true;
    }

    public void Clear()
    {
        foreach (CoalView coal in _scannedResources)
        {
            coal.Destroyed -= OnScannedResourceDestroy;
            ScannedResourceRemoved?.Invoke(coal);
        }

        _scannedResources.Clear();
    }

    private void OnScannedResourceDestroy(ITarget resource)
    {
        resource.Destroyed -= OnScannedResourceDestroy;
        ScannedResourceRemoved?.Invoke(resource);
        _scannedResources.Remove(resource);
    }


    private void OnCollectingResourceDestroy(ITarget resource)
    {
        resource.Destroyed -= OnCollectingResourceDestroy;
        _collectingResources.Remove(resource);
        CollectingResourceDestroyed?.Invoke(resource);
    }
}
