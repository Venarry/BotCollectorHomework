using System;
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
        _interactHandler.SetTarget(_botBase);
        _botBehaviour.SetDestination(_botBase.position);

        _interactHandler.Reached += OnTargetReach;
    }

    private void OnTargetReach()
    {
        if (_storageModel.Count > 0)
        {
            baseStorageView.AddResources(_storageModel.Count);
            _storageModel.TryRemove(_storageModel.Count);
        }

        baseStorageView.AddBots();
        Destroy(gameObject);
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        
    }
}
