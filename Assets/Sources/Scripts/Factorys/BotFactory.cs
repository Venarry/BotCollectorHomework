using UnityEngine;

public class BotFactory
{
    private readonly BotView _prefab = Resources.Load<BotView>(Path.Bot);

    public BotView Create(
        Vector3 position,
        Transform botBase,
        int startResources = 0)
    {
        BotView bot = Object.Instantiate(_prefab, position, Quaternion.identity);
        StorageModel storageModel = new();
        storageModel.Add(startResources);
        bot.Init(storageModel, botBase);

        return bot;
    }
}
