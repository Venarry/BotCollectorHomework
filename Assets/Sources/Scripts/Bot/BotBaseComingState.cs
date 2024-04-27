using UnityEngine;

public class BotBaseComingState : IState
{
    private readonly BotInteractHandler _interactHandler;
    private readonly BotAIBehaviour _botBehaviour;
    private readonly StorageModel _botStorageModel;
    private readonly Transform _bot;
    private ITarget _botBase;
    private BaseStorageView _baseStorageView;

    public BotBaseComingState(
        BotInteractHandler interactHandler,
        BotAIBehaviour botBehaviour,
        StorageModel botStorageModel,
        ITarget botBase,
        BaseStorageView baseStorageView,
        Transform bot)
    {
        _interactHandler = interactHandler;
        _botBehaviour = botBehaviour;
        _botStorageModel = botStorageModel;
        _baseStorageView = baseStorageView;
        _botBase = botBase;
        _bot = bot;
    }

    public void Change(ITarget botBase, BaseStorageView baseStorageView)
    {
        if(_botBase != null)
        {
            _botBase.Destroyed -= OnBaseDestroy;
        }

        _botBase = botBase;
        _baseStorageView = baseStorageView;

        _botBase.Destroyed += OnBaseDestroy;
    }

    private void OnBaseDestroy(ITarget obj)
    {
        _botBase.Destroyed -= OnBaseDestroy;
    }

    public void OnEnter()
    {
        _interactHandler.SetTarget(_botBase.Transform);
        _botBehaviour.SetDestination(_botBase.Transform.position);

        _interactHandler.Reached += OnTargetReach;
    }

    public void OnExit()
    {
        _interactHandler.Reached -= OnTargetReach;
    }

    public void OnUpdate()
    {
    }

    private void OnTargetReach()
    {
        if (_botStorageModel.Count > 0)
        {
            _baseStorageView.AddResources(_botStorageModel.Count);
            _botStorageModel.TryRemove(_botStorageModel.Count);
        }

        _baseStorageView.AddBots();
        Object.Destroy(_bot.gameObject);
    }
}
