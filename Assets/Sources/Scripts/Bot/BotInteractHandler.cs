using System;
using UnityEngine;

public class BotInteractHandler : MonoBehaviour
{
    private BotInteractState _currentBotState;
    private StorageModel _storageModel;
    private Transform _target;

    public event Action ResourceCollected;

    public void Init(StorageModel storageModel)
    {
        _storageModel = storageModel;
    }

    public void SetTarget(Transform target)
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
                if (other.TryGetComponent(out CoalView coal) &&
                    coal.transform == _target)
                {
                    _storageModel.Add();
                    coal.Destroy();
                    ResourceCollected?.Invoke();
                }
                break;

            case BotInteractState.ToBase:
                if (other.TryGetComponent(out BaseStorageView baseStorageView) &&
                    baseStorageView.transform == _target)
                {
                    if(_storageModel.Count > 0)
                    {
                        baseStorageView.AddResources(_storageModel.Count);
                        _storageModel.TryRemove(_storageModel.Count);
                    }

                    baseStorageView.AddBots();
                    Destroy(gameObject);
                }
                break;
        }
    }
}
