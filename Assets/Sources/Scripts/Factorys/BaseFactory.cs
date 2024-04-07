using UnityEngine;

public class BaseFactory
{
    private readonly BaseView _prefab = Resources.Load<BaseView>(Path.Base);

    public BaseView Create(
        Vector3 position,
        IInputsProvider inputsProvider,
        ResourcesPool resourcesPool,
        BotFactory botFactory,
        int baseResourcesValue,
        int baseBotsCount)
    {
        BaseView baseView = Object.Instantiate(_prefab, position, Quaternion.identity);

        StorageModel resourcesStorageModel = new(baseResourcesValue);
        StorageModel botsStorageModel = new(baseBotsCount);
        ScannedResourcesProvider resourcesProvider = new();

        baseView.Init(
            inputsProvider,
            resourcesPool,
            resourcesProvider,
            resourcesStorageModel,
            botsStorageModel,
            botFactory);

        return baseView;
    }
}
