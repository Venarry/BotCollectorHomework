using UnityEngine;

[RequireComponent(typeof(BaseScannerView))]
[RequireComponent(typeof(BaseStorageView))]
[RequireComponent(typeof(BaseBotsHandler))]
public class BaseCompositeRoot : MonoBehaviour
{
    private BaseScannerView _scannerView;
    private BaseStorageView _storageView;
    private BaseBotsHandler _botsHandler;

    private void Awake()
    {
        _scannerView = GetComponent<BaseScannerView>();
        _storageView = GetComponent<BaseStorageView>();
        _botsHandler = GetComponent<BaseBotsHandler>();
    }

    public void Init(
        IInputsProvider inputsProvider,
        ResourcesPool resourcesPool,
        ScannedResourcesProvider baseScannerResourceProvider,
        StorageModel resourcesStorageModel,
        StorageModel botsStorageModel,
        BotFactory botFactory)
    {
        _scannerView.Init(inputsProvider, resourcesPool, baseScannerResourceProvider);
        _storageView.Init(resourcesStorageModel, botsStorageModel);
        _botsHandler
            .Init(botsStorageModel, baseScannerResourceProvider, botFactory, transform);
    }
}
