using System;
using UnityEngine;

public class BotBuildBaseState : BotComingState
{
    private readonly IStateSwitcher _stateSwitcher;

    public BotBuildBaseState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        IStateSwitcher stateSwitcher) 
        : base(interactHandler, botBehaviour)
    {
        _stateSwitcher = stateSwitcher;
    }

    protected override void OnTargetReach()
    {
        Target.Destroy();
        _stateSwitcher.Switch<BotBaseComingState>();
    }

    protected override void OnTargetDestroy()
    {
        _stateSwitcher.Switch<BotBaseComingState>();
    }
}
