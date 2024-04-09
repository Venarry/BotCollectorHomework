using UnityEngine;

[RequireComponent(typeof(BaseScannerView))]
[RequireComponent(typeof(BaseStorageView))]
[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(BaseInteractHandler))]
public class BaseView : MonoBehaviour
{
    [SerializeField] private Transform _botsSpawnPosition;
    [SerializeField] private BaseFlag _flagPrefab;
    [SerializeField] private GameObject _proectionPrefab;

    private BaseScannerView _scannerView;
    private BaseStorageView _storageView;
    private StateMachine _stateMachine;
    private BaseInteractHandler _interactHandler;

    private void Awake()
    {
        _scannerView = GetComponent<BaseScannerView>();
        _storageView = GetComponent<BaseStorageView>();
        _stateMachine = GetComponent<StateMachine>();
        _interactHandler = GetComponent<BaseInteractHandler>();
    }

    public void Init(
        IInputsProvider inputsProvider,
        ResourcesPool resourcesPool,
        ScannedResourcesProvider scannedResourceProvider,
        StorageModel resourcesStorageModel,
        StorageModel botsStorageModel,
        BotFactory botFactory)
    {
        BaseFlagHandler flagHandler = new(_flagPrefab, _proectionPrefab, _stateMachine);
        BaseBotsHandler baseBotsHandler = new(
            transform,
            _botsSpawnPosition,
            botsStorageModel,
            resourcesStorageModel,
            scannedResourceProvider,
            botFactory);

        _interactHandler.Init(flagHandler);

        _scannerView.Init(inputsProvider, resourcesPool, scannedResourceProvider);
        _storageView.Init(resourcesStorageModel, botsStorageModel);

        BaseSpawnBotsState baseResourceCollectState = 
            new(baseBotsHandler, botsStorageModel, resourcesStorageModel, scannedResourceProvider);

        BaseBuildNewBaseState baseBuildNewBaseState =
            new(baseBotsHandler, flagHandler, botsStorageModel, resourcesStorageModel, scannedResourceProvider);

        _stateMachine.Register(baseResourceCollectState);
        _stateMachine.Register(baseBuildNewBaseState);
        _stateMachine.Switch<BaseSpawnBotsState>();
    }
}
