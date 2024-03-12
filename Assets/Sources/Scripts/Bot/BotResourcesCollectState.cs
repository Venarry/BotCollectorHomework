using System;
using UnityEngine;

public class BotResourcesCollectState : IState
{
    private readonly BotInteractHandler _interactHandler;
    private readonly BotAIBehaviour _botBehaviour;
    private readonly CoalView _target;
    private readonly IStateSwitcher _stateSwitcher;

    public BotResourcesCollectState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        CoalView target,
        IStateSwitcher stateSwitcher)
    {
        _interactHandler = interactHandler;
        _botBehaviour = botBehaviour;
        _target = target;
        _stateSwitcher = stateSwitcher;
    }

    public void OnEnter()
    {
        _interactHandler.SetState(BotInteractState.ToResources);
        _interactHandler.SetTarget(_target.transform);

        _botBehaviour.SetDestination(_target.transform.position);
        _target.Destroyed += OnResourceDestroy;
        _interactHandler.ResourceCollected += OnResourceCollect;
    }

    public void OnExit()
    {
        _target.Destroyed -= OnResourceDestroy;
        _interactHandler.ResourceCollected -= OnResourceCollect;
    }

    public void OnUpdate()
    {
    }

    private void OnResourceDestroy(CoalView _)
    {
        OnResourceCollect();
    }

    private void OnResourceCollect()
    {
        _stateSwitcher.Switch<BotBaseComingState>();
    }
}
