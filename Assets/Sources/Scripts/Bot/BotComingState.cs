public abstract class BotComingState : IState
{
    private readonly BotInteractHandler _interactHandler;
    private readonly BotAIBehaviour _botBehaviour;

    protected BotComingState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour)
    {
        _interactHandler = interactHandler;
        _botBehaviour = botBehaviour;
    }

    protected ITarget Target { get; private set; }

    public void Set(ITarget target)
    {
        if (Target != null)
        {
            Target.Destroyed -= OnTargetDestroy;
        }

        Target = target;
        _interactHandler.SetTarget(Target.Transform);
        _botBehaviour.SetDestination(Target.Transform.position);

        Target.Destroyed += OnTargetDestroy;
    }

    public void OnEnter()
    {
        _interactHandler.Reached += OnTryTargetReach;
    }

    public void OnUpdate()
    {
    }

    public void OnExit()
    {
        _interactHandler.Reached -= OnTryTargetReach;
    }

    private void OnTargetDestroy(ITarget target)
    {
        Target.Destroyed -= OnTargetDestroy;
        OnTargetDestroy();
    }

    private void OnTryTargetReach()
    {
        if (Target == null)
        {
            return;
        }

        OnTargetReach();
    }

    protected abstract void OnTargetDestroy();

    protected abstract void OnTargetReach();
}
