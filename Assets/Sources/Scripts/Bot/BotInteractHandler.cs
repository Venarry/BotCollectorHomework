using System;
using UnityEngine;

public class BotInteractHandler : MonoBehaviour
{
    private BotInteractState _currentBotState;
    private StorageModel _storageModel;
    private CoalView _target;

    public event Action ResourceCollected;

    public void Init(StorageModel storageModel)
    {
        _storageModel = storageModel;
    }

    public void SetResourceTarget(CoalView target)
    {
        _target = target;
    }

    public void SetState(BotInteractState botState)
    {
        _currentBotState = botState;
    }

    private void OnTriggerStay(Collider other)
    {
        switch (_currentBotState)
        {
            case BotInteractState.ToResources:
                if(other.TryGetComponent(out CoalView coal) && coal == _target)
                {
                    _storageModel.Add();
                    coal.Destroy();
                    ResourceCollected?.Invoke();
                }
                break;

            case BotInteractState.ToBase:
                if (other.TryGetComponent(out BaseStorageView baseStorageView))
                {
                    baseStorageView.AddResource();
                    baseStorageView.AddBot();
                    Destroy(gameObject);
                }
                break;
        }
    }
}
