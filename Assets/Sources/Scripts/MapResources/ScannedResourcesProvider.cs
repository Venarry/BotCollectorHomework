using System;
using System.Collections.Generic;

public class ScannedResourcesProvider
{
    private readonly List<CoalView> _scannedCoals = new();
    private readonly List<CoalView> _collectingCoals = new();

    public int CoalsCount => _scannedCoals.Count;

    public event Action<CoalView> ScannedCoalAdded;
    public event Action<CoalView> ScannedCoalRemoved;

    public event Action<CoalView> CollectingCoalAdded;
    public event Action<CoalView> CollectingCoalDestroyed;

    public ScannedResourcesProvider()
    {
    }

    public void Add(CoalView coal)
    {
        if (_scannedCoals.Contains(coal) || _collectingCoals.Contains(coal))
            return;

        _scannedCoals.Add(coal);
        coal.Destroyed += OnScannedResourceDestroy;
        ScannedCoalAdded?.Invoke(coal);
    }

    public bool TryTake(out CoalView coal)
    {
        coal = null;

        if (_scannedCoals.Count == 0)
            return false;

        coal = _scannedCoals[0];
        coal.Destroyed -= OnScannedResourceDestroy;
        ScannedCoalRemoved?.Invoke(coal);
        _scannedCoals.RemoveAt(0);

        _collectingCoals.Add(coal);
        CollectingCoalAdded?.Invoke(coal);
        coal.Destroyed += OnCollectingResourceDestroy;

        return true;
    }

    public void Clear()
    {
        foreach (CoalView coal in _scannedCoals)
        {
            coal.Destroyed -= OnScannedResourceDestroy;
            ScannedCoalRemoved?.Invoke(coal);
        }

        _scannedCoals.Clear();
    }

    private void OnScannedResourceDestroy(CoalView coal)
    {
        coal.Destroyed -= OnScannedResourceDestroy;
        ScannedCoalRemoved?.Invoke(coal);
        _scannedCoals.Remove(coal);
    }


    private void OnCollectingResourceDestroy(CoalView coal)
    {
        coal.Destroyed -= OnCollectingResourceDestroy;
        _collectingCoals.Remove(coal);
        CollectingCoalDestroyed?.Invoke(coal);
    }
}
