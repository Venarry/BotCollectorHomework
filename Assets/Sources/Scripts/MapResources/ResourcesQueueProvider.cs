using System.Collections.Generic;
using UnityEngine;

public class ResourcesQueueProvider : MonoBehaviour
{
    private readonly Queue<CoalView> _coals = new();

    public ResourcesQueueProvider(CoalView[] coals)
    {
        foreach (CoalView coal in coals)
        {
            Enqueue(coal);
        }
    }

    public ResourcesQueueProvider()
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
