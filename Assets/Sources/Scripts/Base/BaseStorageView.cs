using UnityEngine;

public class BaseStorageView : MonoBehaviour
{
    private StorageModel _resourcesStorageModel;
    private StorageModel _botsStorageModel;

    public void AddResource()
    {
        _resourcesStorageModel.Add();
    }

    public void AddResources(int count)
    {
        _resourcesStorageModel.Add(count);
    }

    public void AddBot()
    {
        _botsStorageModel.Add();
    }

    public void AddBots(int count)
    {
        _botsStorageModel.Add(count);
    }
}
