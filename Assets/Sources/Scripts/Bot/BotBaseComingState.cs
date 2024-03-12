using UnityEngine;

public class BotBaseComingState : IState
{
    private readonly BotInteractHandler _interactHandler;
    private readonly BotAIBehaviour _botBehaviour;
    private readonly Transform _botBase;

    public BotBaseComingState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        Transform botBase)
    {
        _interactHandler = interactHandler;
        _botBehaviour = botBehaviour;
        _botBase = botBase;
    }

    public void OnEnter()
    {
        _interactHandler.SetState(BotInteractState.ToBase);
        _interactHandler.SetTarget(_botBase);
        _botBehaviour.SetDestination(_botBase.position);
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
