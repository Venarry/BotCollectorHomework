using UnityEngine;

public class BaseFactory
{
    private readonly BaseCompositeRoot _prefab = Resources.Load<BaseCompositeRoot>(Path.Base);

    public BaseCompositeRoot Create(
        Vector3 position,
        IInputsProvider inputsProvider,
        ResourcesPool resourcesPool,
        BotFactory botFactory,
        int baseResourcesValue,
        int baseBotsCount)
    {
        BaseCompositeRoot baseCompositeRoot = Object.Instantiate(_prefab, position, Quaternion.identity);

        StorageModel resourcesStorageModel = new(baseResourcesValue);
        StorageModel botsStorageModel = new(baseBotsCount);
        ScannedResourcesProvider resourcesProvider = new();

        baseCompositeRoot.Init(
            inputsProvider,
            resourcesPool,
            resourcesProvider,
            resourcesStorageModel,
            botsStorageModel,
            botFactory);

        return baseCompositeRoot;
    }
}
