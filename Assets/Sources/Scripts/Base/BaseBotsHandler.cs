using System;
using UnityEngine;

public class BaseBotsHandler : MonoBehaviour
{
    [SerializeField] private Transform _botSpawnPoint;

    private StorageModel _botsStorageModel;
    private ResourcesProvider _scannedResourcesProvider;
    private BotFactory _botFactory;
    private Transform _base;

    public void Init(
        StorageModel botsStorageModel,
        ResourcesProvider resourcesProvider,
        BotFactory botFactory,
        Transform botBase)
    {
        _botsStorageModel = botsStorageModel;
        _scannedResourcesProvider = resourcesProvider;
        _botFactory = botFactory;
        _base = botBase;

        _botsStorageModel.Added += TrySendBots;
        _scannedResourcesProvider.Added += OnResourceAdd;
    }

    private void OnDestroy()
    {
        _botsStorageModel.Added -= TrySendBots;
        _scannedResourcesProvider.Added -= OnResourceAdd;
    }

    private void OnResourceAdd(CoalView _)
    {
        TrySendBots();
    }

    private void TrySendBots()
    {
        int availableShipments = Math
            .Min(_botsStorageModel.Count, _scannedResourcesProvider.CoalsCount);
        Debug.Log($"TrySend {availableShipments}");

        if (availableShipments == 0)
        {
            return;
        }

        for (int i = 0; i < availableShipments; i++)
        {
            _botsStorageModel.TryRemove();
            _scannedResourcesProvider.TryTake(out CoalView coal);
            _botFactory.Create(_botSpawnPoint.position, coal, _base);
        }
    }
}
