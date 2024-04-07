using System;
using TMPro;
using UnityEngine;

public class BotStorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourcesCountLabel;

    private StorageModel _storageModel;

    public void Init(StorageModel storageModel)
    {
        _storageModel = storageModel;
        _storageModel.Added += OnResourcesAdded;

        _resourcesCountLabel.text = _storageModel.Count.ToString();
    }

    private void OnDestroy()
    {
        _storageModel.Added -= OnResourcesAdded;
    }

    private void OnResourcesAdded()
    {
        _resourcesCountLabel.text = _storageModel.Count.ToString();
    }
}
