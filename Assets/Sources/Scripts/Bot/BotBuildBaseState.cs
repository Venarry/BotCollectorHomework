public class BotBuildBaseState : BotComingState
{
    private readonly IBotStateHandler _stateSwitcher;
    private readonly BaseFactory _baseFactory;
    private readonly StorageModel _botStorage;

    public BotBuildBaseState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        StorageModel storageModel,
        BaseFactory baseFactory,
        IBotStateHandler stateSwitcher) 
        : base(interactHandler, botBehaviour)
    {
        _stateSwitcher = stateSwitcher;
        _botStorage = storageModel;
        _baseFactory = baseFactory;
    }

    protected override void OnTargetReach()
    {
        BaseView newBase = _baseFactory.Create(Target.Transform.position, baseResourcesValue: 0, baseBotsCount: 0);
        _stateSwitcher.ChangeBase(newBase, newBase.StorageView);
        _botStorage.TryRemove(_botStorage.Count);
        _stateSwitcher.Switch<BotBaseComingState>();

        Target.Destroy();
    }

    protected override void OnTargetDestroy()
    {
        _stateSwitcher.Switch<BotBaseComingState>();
    }
}
