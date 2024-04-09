using System;

public class BaseSpawnBotsState : IState
{
    private const int ResourceForSpawnBot = 3;

    private readonly BaseBotsHandler _baseBotsHandler;
    private readonly StorageModel _botsStorageModel;
    private readonly StorageModel _resourcesStorageModel;
    private readonly ScannedResourcesProvider _scannedResourcesProvider;

    public BaseSpawnBotsState(
        BaseBotsHandler baseBotsHandler,
        StorageModel botsStorageModel,
        StorageModel resourcesStorageModel,
        ScannedResourcesProvider resourcesProvider)
    {
        _baseBotsHandler = baseBotsHandler;
        _botsStorageModel = botsStorageModel;
        _resourcesStorageModel = resourcesStorageModel;
        _scannedResourcesProvider = resourcesProvider;
    }

    public void OnEnter()
    {
        _botsStorageModel.Added += OnBotAdd;
        _scannedResourcesProvider.ScannedResourceAdded += OnScannedResourceAdd;
        _resourcesStorageModel.Added += OnBaseResourceAdd;

        _baseBotsHandler.TrySendBotsToResource();
    }


    public void OnExit()
    {
        _botsStorageModel.Added -= OnBotAdd;
        _scannedResourcesProvider.ScannedResourceAdded -= OnScannedResourceAdd;
        _resourcesStorageModel.Added -= OnBaseResourceAdd;
    }

    public void OnUpdate()
    {
    }

    private void OnBotAdd()
    {
        _baseBotsHandler.TrySendBotsToResource();
    }

    private void OnScannedResourceAdd(ITarget _)
    {
        _baseBotsHandler.TrySendBotsToResource();
    }

    private void OnBaseResourceAdd()
    {
        if (_resourcesStorageModel.Count >= ResourceForSpawnBot)
        {
            _baseBotsHandler.AddNewBot();
            _resourcesStorageModel.TryRemove(ResourceForSpawnBot);
        }
    }
}
