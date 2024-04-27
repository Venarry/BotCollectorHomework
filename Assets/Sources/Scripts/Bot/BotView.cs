using UnityEngine;

[RequireComponent(typeof(BotStateMachine))]
[RequireComponent(typeof(BotInteractHandler))]
[RequireComponent(typeof(BotAIBehaviour))]
[RequireComponent(typeof(BotStorageView))]
public class BotView : MonoBehaviour
{
    private BotStateMachine _stateMachine;
    private BotInteractHandler _interactHandler;
    private BotAIBehaviour _botAIBehaviour;
    private BotStorageView _botStorageView;

    private void Awake()
    {
        _stateMachine = GetComponent<BotStateMachine>();
        _interactHandler = GetComponent<BotInteractHandler>();
        _botAIBehaviour = GetComponent<BotAIBehaviour>();
        _botStorageView = GetComponent<BotStorageView>();
    }

    public void Init(
        StorageModel storageModel,
        ITarget botBase,
        BaseStorageView baseStorageView,
        BaseFactory baseFactory)
    {
        _interactHandler.Init(storageModel);
        _botStorageView.Init(storageModel);
        _stateMachine.Init(storageModel, botBase, baseStorageView, baseFactory, _interactHandler, _botAIBehaviour);
    }

    public void GoToResource(ITarget target)
    {
        _stateMachine.GoToResource(target);
    }

    public void GoToFlag(ITarget target)
    {
        _stateMachine.GoToFlag(target);
    }
}