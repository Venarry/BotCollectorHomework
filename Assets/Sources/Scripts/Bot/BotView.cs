using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(BotInteractHandler))]
[RequireComponent(typeof(BotAIBehaviour))]
[RequireComponent(typeof(BotStorageView))]
public class BotView : MonoBehaviour
{
    private StateMachine _stateMachine;
    private BotInteractHandler _interactHandler;
    private BotAIBehaviour _botAIBehaviour;
    private BotStorageView _botStorageView;
    private BotResourcesCollectState _botResourcesCollectState;
    //private BotBuil _botResourcesCollectState;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _interactHandler = GetComponent<BotInteractHandler>();
        _botAIBehaviour = GetComponent<BotAIBehaviour>();
        _botStorageView = GetComponent<BotStorageView>();
    }

    public void Init(StorageModel storageModel, Transform botBase)
    {
        _interactHandler.Init(storageModel);
        _botStorageView.Init(storageModel);

        _botResourcesCollectState = 
            new(_interactHandler, _botAIBehaviour, _stateMachine);

        BotBaseComingState botBaseComingState =
            new(_interactHandler, _botAIBehaviour, botBase);

        _stateMachine.Register(_botResourcesCollectState);
        _stateMachine.Register(botBaseComingState);
    }

    public void GoToResource(ITarget target)
    {
        _botResourcesCollectState.SetTarget(target);
        _stateMachine.Switch<BotResourcesCollectState>();
    }

    public void GoToFlag(ITarget target)
    {

    }
}
