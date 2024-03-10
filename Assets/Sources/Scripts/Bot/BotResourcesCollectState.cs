using System;
using UnityEngine;

public class BotResourcesCollectState : IState
{
    private readonly BotInteractHandler _interactHandler;
    private readonly BotAIBehaviour _botBehaviour;
    private readonly ResourcesProvider _resourcesProvider;
    private readonly IStateSwitcher _stateSwitcher;

    public BotResourcesCollectState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        ResourcesProvider resourcesProvider,
        IStateSwitcher stateSwitcher)
    {
        _interactHandler = interactHandler;
        _botBehaviour = botBehaviour;
        _resourcesProvider = resourcesProvider;
        _stateSwitcher = stateSwitcher;
    }

    public void OnEnter()
    {
        _interactHandler.SetState(BotInteractState.ToResources);
        _botBehaviour.SetDestination(_resourcesProvider.Dequeu().transform.position);
        _interactHandler.ResourceCollected += OnResourceCollect;
    }

    private void OnResourceCollect()
    {
        _stateSwitcher.Switch<BotBaseComingState>();
    }

    public void OnExit()
    {
        _interactHandler.ResourceCollected -= OnResourceCollect;
    }

    public void OnUpdate()
    {
        
    }
}
