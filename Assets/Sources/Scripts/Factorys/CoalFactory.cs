using UnityEngine;

public class CoalFactory
{
    private readonly CoalView _prefab = Resources.Load<CoalView>(Path.Coal);

    public CoalView Create(Vector3 position, ResourcesPool resourcesPool)
    {
        CoalView coalView = Object.Instantiate(_prefab, position, Quaternion.identity);

        coalView.Init(resourcesPool);
        return coalView;
    }
}
