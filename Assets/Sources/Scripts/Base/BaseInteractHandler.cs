using UnityEngine;

public class BaseInteractHandler : MonoBehaviour, IInteractable
{
    private BaseFlagHandler _baseFlagHandler;

    public void Init(BaseFlagHandler baseFlagHandler)
    {
        _baseFlagHandler = baseFlagHandler;
    }

    public void Interact(UserInteractHandler userInteractHandler)
    {
        userInteractHandler
            .PrepareToBuild(_baseFlagHandler.ProectionPrefab, _baseFlagHandler.SpawnFlag);
    }
}
