using UnityEngine;

public class BotFactory
{
    private readonly BotView _prefab = Resources.Load<BotView>(Path.Bot);
    private BaseFactory _baseFactory;

    public void Init(BaseFactory baseFactory)
    {
        _baseFactory = baseFactory;
    }

    public BotView Create(
        Vector3 position,
        ITarget botBase,
        BaseStorageView baseStorageView,
        int startResources = 0)
    {
        BotView bot = Object.Instantiate(_prefab, position, Quaternion.identity);
        StorageModel storageModel = new();
        storageModel.Add(startResources);
        bot.Init(storageModel, botBase, baseStorageView, _baseFactory);

        return bot;
    }
}
