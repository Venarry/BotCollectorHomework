using UnityEngine;

public class BotFactory
{
    private readonly BotCompositeRoot _prefab = Resources.Load<BotCompositeRoot>(Path.Bot);

    public BotCompositeRoot Create(
        Vector3 position,
        CoalView target,
        Transform botBase)
    {
        BotCompositeRoot bot = Object.Instantiate(_prefab, position, Quaternion.identity);
        StorageModel storageModel = new();
        bot.Init(storageModel, target, botBase);

        return bot;
    }
}
