using UnityEngine;

public class BotBaseComingState : IState
{
    private readonly BotInteractHandler _interactHandler;
    private readonly BotAIBehaviour _botBehaviour;
    private readonly Vector3 _botBase;

    public BotBaseComingState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        Vector3 botBase)
    {
        _interactHandler = interactHandler;
        _botBehaviour = botBehaviour;
        _botBase = botBase;
    }

    public void OnEnter()
    {
        _interactHandler.SetState(BotInteractState.ToBase);
        _botBehaviour.SetDestination(_botBase);
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
