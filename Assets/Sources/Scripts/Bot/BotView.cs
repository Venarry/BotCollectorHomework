using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(BotInteractHandler))]
[RequireComponent(typeof(BotAIBehaviour))]
[RequireComponent(typeof(BotStorageView))]
public class BotView : MonoBehaviour
{
    private StateMachine _stateMachine;
    private BotInteractHandler _interactHandler;
    private BotAIBehaviour _botAIBehaviour;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _interactHandler = GetComponent<BotInteractHandler>();
        _botAIBehaviour = GetComponent<BotAIBehaviour>();
    }

    public void Init(ResourcesProvider resourcesProvider, Vector3 botBase)
    {
        BotResourcesCollectState botResourcesCollectState = 
            new(_interactHandler, _botAIBehaviour, resourcesProvider, _stateMachine);

        BotBaseComingState botBaseComingState =
            new(_interactHandler, _botAIBehaviour, botBase);

        _stateMachine.Register(botResourcesCollectState);
        _stateMachine.Register(botBaseComingState);
        _stateMachine.Switch<BotResourcesCollectState>();
    }
}
