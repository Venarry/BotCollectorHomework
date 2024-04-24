public class BotResourcesCollectState : BotComingState
{
    private readonly StorageModel _botStorageModel;
    private readonly IStateSwitcher _stateSwitcher;

    public BotResourcesCollectState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        StorageModel botStorageModel,
        IStateSwitcher stateSwitcher) : base(interactHandler, botBehaviour)
    {
        _botStorageModel = botStorageModel;
        _stateSwitcher = stateSwitcher;
    }

    /*public void SetTarget(ITarget target)
    {
        if(_target != null)
        {
            _target.Destroyed -= OnTargetDestroy;
        }

        _target = target;
        _interactHandler.SetState(BotInteractState.ToResources);
        _interactHandler.SetTarget(_target.Transform);
        _botBehaviour.SetDestination(_target.Transform.position);

        _target.Destroyed += OnTargetDestroy;
        _interactHandler.Reached += OnTargetReach;
    }*/

    protected override void OnTargetDestroy()
    {
        _stateSwitcher.Switch<BotBaseComingState>();
    }

    protected override void OnTargetReach()
    {
        _botStorageModel.Add();
        Target.Destroy();
        _stateSwitcher.Switch<BotBaseComingState>();
    }
}
