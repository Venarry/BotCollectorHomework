using System;
using UnityEngine;

public class BaseBotsHandler
{
    private readonly ITarget _base;
    private readonly Transform _botsSpawnPoint;
    private readonly BaseStorageView _baseStorageView;
    private readonly ScannedResourcesProvider _scannedResourcesProvider;
    private readonly BotFactory _botFactory;

    public BaseBotsHandler(
        ITarget thisBase,
        Transform botsSpawnPoint,
        BaseStorageView baseStorageView,
        ScannedResourcesProvider scannedResourcesProvider,
        BotFactory botFactory)
    {
        _base = thisBase;
        _botsSpawnPoint = botsSpawnPoint;
        _baseStorageView = baseStorageView;
        _scannedResourcesProvider = scannedResourcesProvider;
        _botFactory = botFactory;
    }

    public void AddNewBot()
    {
        _baseStorageView.AddBots();
    }

    public void TrySendBotsToResource()
    {
        int availableShipments = Math
            .Min(_baseStorageView.BotsCount, _scannedResourcesProvider.CoalsCount);

        if (availableShipments == 0)
        {
            return;
        }

        for (int i = 0; i < availableShipments; i++)
        {
            _baseStorageView.TryRemoveBots();
            _scannedResourcesProvider.TryTake(out ITarget resource);
            BotView bot = _botFactory.Create(_botsSpawnPoint.position, _base, _baseStorageView);
            bot.GoToResource(resource);
        }
    }

    public bool TrySendBotToBuildBase(int resourcesForBuild, ITarget flag)
    {
        if (_baseStorageView.ResourcesCount >= resourcesForBuild &&
            _baseStorageView.BotsCount > 0)
        {
            _baseStorageView.TryRemoveBots();
            _baseStorageView.TryRemoveResource(resourcesForBuild);

            BotView bot = _botFactory
                .Create(_botsSpawnPoint.position, _base, _baseStorageView, startResources: resourcesForBuild);
            bot.GoToFlag(flag);

            return true;
        }

        return false;
    }
}
