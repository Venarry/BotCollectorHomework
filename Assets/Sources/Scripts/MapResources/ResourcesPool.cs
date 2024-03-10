using System.Collections.Generic;

public class ResourcesPool
{
    private readonly List<CoalView> _coals = new();

    public void Add(CoalView coal)
    {
        if (_coals.Contains(coal))
            return;

        _coals.Add(coal);
    }

    public void Add(CoalView[] coals)
    {
        foreach (CoalView coal in coals)
        {
            Add(coal);
        }
    }

    public void Remove(CoalView coal)
    {
        if (_coals.Contains(coal) == false)
            return;

        _coals.Remove(coal);
    }

    public CoalView[] GetAll()
        => _coals.ToArray();
}
