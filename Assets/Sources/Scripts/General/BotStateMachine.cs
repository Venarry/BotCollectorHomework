public class BotStateMachine : StateMachine, IBotStateHandler
{
    private BotResourcesCollectState _botResourcesCollectState;
    private BotBuildBaseState _botBuildBaseState;
    private BotBaseComingState _botBaseComingState;

    public void Init(
        StorageModel storageModel,
        ITarget botBase,
        BaseStorageView baseStorageView,
        BaseFactory baseFactory,
        BotInteractHandler interactHandler,
        BotAIBehaviour botAIBehaviour)
    {
        _botResourcesCollectState =
            new(interactHandler, botAIBehaviour, storageModel, this);

        _botBuildBaseState =
            new(interactHandler, botAIBehaviour, storageModel, baseFactory, this);

        _botBaseComingState =
            new(interactHandler, botAIBehaviour, storageModel, botBase, baseStorageView, transform);

        Register(_botResourcesCollectState);
        Register(_botBuildBaseState);
        Register(_botBaseComingState);
    }

    public void GoToResource(ITarget target)
    {
        _botResourcesCollectState.Set(target);
        Switch<BotResourcesCollectState>();
    }

    public void GoToFlag(ITarget target)
    {
        _botBuildBaseState.Set(target);
        Switch<BotBuildBaseState>();
    }

    public void ChangeBase(ITarget botBase, BaseStorageView baseStorageView)
    {
        _botBaseComingState.Change(botBase, baseStorageView);
    }
}
