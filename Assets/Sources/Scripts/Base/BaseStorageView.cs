using TMPro;
using UnityEngine;

public class BaseStorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourcesCount;
    [SerializeField] private TMP_Text _botsCount;

    private StorageModel _resourcesStorageModel;
    private StorageModel _botsStorageModel;

    public int ResourcesCount => _resourcesStorageModel.Count;
    public int BotsCount => _botsStorageModel.Count;

    public void Init(
        StorageModel resourcesStorageModel,
        StorageModel botsStorageModel)
    {
        _resourcesStorageModel = resourcesStorageModel;
        _botsStorageModel = botsStorageModel;

        _resourcesStorageModel.Added += RefreshResourcesCount;
        _resourcesStorageModel.Removed += RefreshResourcesCount;
        _botsStorageModel.Added += RefreshBotsCount;
        _botsStorageModel.Removed += RefreshBotsCount;

        RefreshResourcesCount();
        RefreshBotsCount();
    }

    private void OnDestroy()
    {
        _resourcesStorageModel.Added -= RefreshResourcesCount;
        _resourcesStorageModel.Removed -= RefreshResourcesCount;
        _botsStorageModel.Added -= RefreshBotsCount;
        _botsStorageModel.Removed -= RefreshBotsCount;
    }

    public void AddResources(int count = 1)
    {
        _resourcesStorageModel.Add(count);
    }

    public void AddBots(int count = 1)
    {
        _botsStorageModel.Add(count);
    }

    public bool TryRemoveResource(int count = 1) => 
        _resourcesStorageModel.TryRemove(count);

    public bool TryRemoveBots(int count = 1) =>
        _botsStorageModel.TryRemove(count);

    private void RefreshResourcesCount()
    {
        _resourcesCount.text = _resourcesStorageModel.Count.ToString();
    }

    private void RefreshBotsCount()
    {
        _botsCount.text = _botsStorageModel.Count.ToString();
    }
}
