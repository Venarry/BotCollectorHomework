using System;
using UnityEngine;

public class BaseBotsHandler
{
    private readonly Transform _base;
    private readonly Transform _botsSpawnPoint;
    private readonly StorageModel _botsStorageModel;
    private readonly StorageModel _resourcesStorageModel;
    private readonly ScannedResourcesProvider _scannedResourcesProvider;
    private readonly BotFactory _botFactory;

    public BaseBotsHandler(
        Transform thisBase,
        Transform botsSpawnPoint,
        StorageModel botsStorageModel,
        StorageModel resourcesStorageModel,
        ScannedResourcesProvider scannedResourcesProvider,
        BotFactory botFactory)
    {
        _base = thisBase;
        _botsSpawnPoint = botsSpawnPoint;
        _botsStorageModel = botsStorageModel;
        _resourcesStorageModel = resourcesStorageModel;
        _scannedResourcesProvider = scannedResourcesProvider;
        _botFactory = botFactory;
    }

    public void AddNewBot()
    {
        _botsStorageModel.Add();
    }

    public void TrySendBotsToResource()
    {
        int availableShipments = Math
            .Min(_botsStorageModel.Count, _scannedResourcesProvider.CoalsCount);

        if (availableShipments == 0)
        {
            return;
        }

        for (int i = 0; i < availableShipments; i++)
        {
            _botsStorageModel.TryRemove();
            _scannedResourcesProvider.TryTake(out ITarget resource);
            BotView bot = _botFactory.Create(_botsSpawnPoint.position, _base);
            bot.GoToResource(resource);
        }
    }

    public void TrySendBotToBuildBase(int resourcesForBuild, ITarget flag)
    {
        if (_resourcesStorageModel.Count >= resourcesForBuild &&
            _botsStorageModel.Count > 0)
        {
            _botsStorageModel.TryRemove();
            _resourcesStorageModel.TryRemove(resourcesForBuild);

            BotView bot = _botFactory
                .Create(_botsSpawnPoint.position, _base, startResources: resourcesForBuild);
            bot.GoToFlag(flag);
        }
    }
}
