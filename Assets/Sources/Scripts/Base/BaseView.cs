using System;
using UnityEngine;

[RequireComponent(typeof(BaseStorageView))]
[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(BaseInteractHandler))]
public class BaseView : MonoBehaviour, ITarget
{
    [SerializeField] private Transform _botsSpawnPosition;
    [SerializeField] private BaseFlag _flagPrefab;
    [SerializeField] private GameObject _proectionPrefab;

    private StateMachine _stateMachine;
    private BaseInteractHandler _interactHandler;

    public event Action<ITarget> Destroyed;

    public BaseStorageView StorageView { get; private set; }
    public Transform Transform => transform;

    private void Awake()
    {
        StorageView = GetComponent<BaseStorageView>();
        _stateMachine = GetComponent<StateMachine>();
        _interactHandler = GetComponent<BaseInteractHandler>();
    }

    public void Init(
        ScannedResourcesProvider scannedResourceProvider,
        StorageModel resourcesStorageModel,
        StorageModel botsStorageModel,
        BotFactory botFactory)
    {
        BaseFlagHandler flagHandler = new(_flagPrefab, _proectionPrefab, _stateMachine);
        BaseBotsHandler baseBotsHandler = new(
            this,
            _botsSpawnPosition,
            StorageView,
            scannedResourceProvider,
            botFactory);

        _interactHandler.Init(flagHandler);

        StorageView.Init(resourcesStorageModel, botsStorageModel);

        BaseSpawnBotsState baseResourceCollectState = 
            new(baseBotsHandler, botsStorageModel, resourcesStorageModel, scannedResourceProvider);

        BaseBuildNewBaseState baseBuildNewBaseState =
            new(baseBotsHandler, flagHandler, botsStorageModel, resourcesStorageModel, scannedResourceProvider, _stateMachine);

        _stateMachine.Register(baseResourceCollectState);
        _stateMachine.Register(baseBuildNewBaseState);
        _stateMachine.Switch<BaseSpawnBotsState>();
    }

    public void Destroy()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
