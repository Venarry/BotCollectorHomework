using System;
using UnityEngine;

public class BotStorageView : MonoBehaviour
{
    private StorageModel _storageModel;

    public void Init(StorageModel storageModel)
    {
        _storageModel = storageModel;
        _storageModel.Added += OnResourcesAdded;
    }

    private void OnDestroy()
    {
        _storageModel.Added -= OnResourcesAdded;
    }

    private void OnResourcesAdded()
    {
        Debug.Log("resources was collect by bot");
    }
}
