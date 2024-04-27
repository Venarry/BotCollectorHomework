using UnityEngine;

public class BaseFactory
{
    private readonly BaseView _prefab = Resources.Load<BaseView>(Path.Base);
    private readonly ScannedResourcesProvider _scannedResourcesProvider;
    private BotFactory _botFactory;

    public BaseFactory(
        ScannedResourcesProvider scannedResourcesProvider)
    {
        _scannedResourcesProvider = scannedResourcesProvider;
    }

    public void Init(BotFactory botFactory)
    {
        _botFactory = botFactory;
    }

    public BaseView Create(
        Vector3 position,
        int baseResourcesValue,
        int baseBotsCount)
    {
        BaseView baseView = Object.Instantiate(_prefab, position, Quaternion.identity);

        StorageModel resourcesStorageModel = new(baseResourcesValue);
        StorageModel botsStorageModel = new(baseBotsCount);

        baseView.Init(
            _scannedResourcesProvider,
            resourcesStorageModel,
            botsStorageModel,
            _botFactory);

        return baseView;
    }
}
