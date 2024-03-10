using System.Collections.Generic;
using UnityEngine;

public class ResourcesProvider : MonoBehaviour
{
    private readonly Queue<CoalView> _coals = new();

    public ResourcesProvider(CoalView[] coals)
    {
        foreach (CoalView coal in coals)
        {
            Enqueue(coal);
        }
    }

    public ResourcesProvider()
    {
    }

    public void Enqueue(CoalView coal)
    {
        if (_coals.Contains(coal))
            return;

        _coals.Enqueue(coal);
    }

    public CoalView Dequeu() => _coals.Dequeue();
}
