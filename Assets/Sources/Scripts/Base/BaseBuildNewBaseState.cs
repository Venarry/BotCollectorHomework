using System;

public class BaseBuildNewBaseState : IState
{
    private const int ResourceForBuild = 5;

    private readonly BaseBotsHandler _baseBotsHandler;
    private readonly BaseFlagHandler _flagHandler;
    private readonly StorageModel _botsStorageModel;
    private readonly StorageModel _resourcesStorageModel;
    private readonly ScannedResourcesProvider _scannedResourcesProvider;
    private readonly IStateSwitcher _stateSwitcher;

    public BaseBuildNewBaseState(
        BaseBotsHandler baseBotsHandler,
        BaseFlagHandler flagHandler,
        StorageModel botsStorageModel,
        StorageModel resourcesStorageModel,
        ScannedResourcesProvider scannedResourcesProvider,
        IStateSwitcher stateSwitcher)
    {
        _baseBotsHandler = baseBotsHandler;
        _flagHandler = flagHandler;
        _botsStorageModel = botsStorageModel;
        _resourcesStorageModel = resourcesStorageModel;
        _scannedResourcesProvider = scannedResourcesProvider;
        _stateSwitcher = stateSwitcher;
    }

    public void OnEnter()
    {
        _botsStorageModel.Added += OnStorageChange;
        _resourcesStorageModel.Added += OnStorageChange;
        _scannedResourcesProvider.ScannedResourceAdded += OnScannedResourceAdd;
        TrySendBotsToBuildBase();
    }

    public void OnExit()
    {
        _botsStorageModel.Added -= OnStorageChange;
        _resourcesStorageModel.Added -= OnStorageChange;
        _scannedResourcesProvider.ScannedResourceAdded -= OnScannedResourceAdd;
    }

    public void OnUpdate()
    {
    }

    private void OnStorageChange()
    {
        if (TrySendBotsToBuildBase())
        {
            return;
        }

        _baseBotsHandler.TrySendBotsToResource();
    }

    private void OnScannedResourceAdd(ITarget target)
    {
        _baseBotsHandler.TrySendBotsToResource();
    }

    private bool TrySendBotsToBuildBase()
    {
        if(_resourcesStorageModel.Count >= ResourceForBuild)
        {
            if(_baseBotsHandler
                .TrySendBotToBuildBase(ResourceForBuild, _flagHandler.ActiveFlag))
            {
                _stateSwitcher.Switch<BaseSpawnBotsState>();
            }

            return true;
        }

        return false;
    }
}
