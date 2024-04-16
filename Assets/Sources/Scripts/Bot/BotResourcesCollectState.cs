public class BotResourcesCollectState : IState
{
    private readonly BotInteractHandler _interactHandler;
    private readonly BotAIBehaviour _botBehaviour;
    private readonly StorageModel _botStorageModel;
    private readonly IStateSwitcher _stateSwitcher;

    private ITarget _target;

    public BotResourcesCollectState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        StorageModel botStorageModel,
        IStateSwitcher stateSwitcher)
    {
        _interactHandler = interactHandler;
        _botBehaviour = botBehaviour;
        _botStorageModel = botStorageModel;
        _stateSwitcher = stateSwitcher;
    }

    public void SetTarget(ITarget target)
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
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        _interactHandler.ResourceCollected -= OnTargetReach;
    }

    public void OnUpdate()
    {
    }

    private void OnTargetDestroy(ITarget _)
    {
        _target.Destroyed -= OnTargetDestroy;
        _stateSwitcher.Switch<BotBaseComingState>();
    }

    private void OnTargetReach()
    {
        _botStorageModel.Add();
        _target.Destroy();
        _stateSwitcher.Switch<BotBaseComingState>();
    }
}
