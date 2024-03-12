using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.tvOS;

public class ResourcesProvider
{
    private readonly List<CoalView> _coals = new();

    public int CoalsCount => _coals.Count;

    public event Action<CoalView> Added;
    public event Action<CoalView> Removed;

    /*public ResourcesProvider(CoalView[] coals)
    {
        _coals.AddRange(coals);
    }*/

    public ResourcesProvider()
    {
    }

    public void Add(CoalView coal)
    {
        if (_coals.Contains(coal))
            return;

        _coals.Add(coal);
        coal.Destroyed += OnResourceDestroy;
        Added?.Invoke(coal);
    }

    public bool TryTake(out CoalView coal)
    {
        coal = null;

        if (_coals.Count == 0)
            return false;

        coal = _coals[0];
        coal.Destroyed -= OnResourceDestroy;
        Removed?.Invoke(coal);
        _coals.RemoveAt(0);
        return true;
    }

    public void Clear()
    {
        foreach (CoalView coal in _coals)
        {
            coal.Destroyed -= OnResourceDestroy;
            Removed?.Invoke(coal);
        }

        _coals.Clear();
    }

    private void OnResourceDestroy(CoalView coal)
    {
        coal.Destroyed -= OnResourceDestroy;
        Removed?.Invoke(coal);
        _coals.Remove(coal);
    }
}
