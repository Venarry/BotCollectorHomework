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

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _interactHandler = GetComponent<BotInteractHandler>();
        _botAIBehaviour = GetComponent<BotAIBehaviour>();
        _botStorageView = GetComponent<BotStorageView>();
    }

    public void Init(StorageModel storageModel, CoalView coalTarget, Transform botBase)
    {
        _interactHandler.Init(storageModel);
        _botStorageView.Init(storageModel);
        BotResourcesCollectState botResourcesCollectState = 
            new(_interactHandler, _botAIBehaviour, coalTarget, _stateMachine);

        BotBaseComingState botBaseComingState =
            new(_interactHandler, _botAIBehaviour, botBase);

        _stateMachine.Register(botResourcesCollectState);
        _stateMachine.Register(botBaseComingState);
        _stateMachine.Switch<BotResourcesCollectState>();
    }
}
