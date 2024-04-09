public class BotResourcesCollectState : IState
{
    private readonly BotInteractHandler _interactHandler;
    private readonly BotAIBehaviour _botBehaviour;
    private readonly IStateSwitcher _stateSwitcher;

    private ITarget _target;

    public BotResourcesCollectState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        IStateSwitcher stateSwitcher)
    {
        _interactHandler = interactHandler;
        _botBehaviour = botBehaviour;
        _stateSwitcher = stateSwitcher;
    }

    public void SetTarget(ITarget target)
    {
        _target = target;
        _interactHandler.SetState(BotInteractState.ToResources);
        _interactHandler.SetTarget(_target.Transform);

        _botBehaviour.SetDestination(_target.Transform.position);
        _target.Destroyed += OnResourceDestroy;
        _interactHandler.ResourceCollected += OnResourceCollect;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        _target.Destroyed -= OnResourceDestroy;
        _interactHandler.ResourceCollected -= OnResourceCollect;
    }

    public void OnUpdate()
    {
    }

    private void OnResourceDestroy(ITarget _)
    {
        OnResourceCollect();
    }

    private void OnResourceCollect()
    {
        _stateSwitcher.Switch<BotBaseComingState>();
    }
}
