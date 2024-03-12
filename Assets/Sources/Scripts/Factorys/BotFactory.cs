using UnityEngine;

public class BotFactory
{
    private readonly BotView _prefab = Resources.Load<BotView>(Path.Bot);

    public BotView Create(
        Vector3 position,
        CoalView target,
        Transform botBase)
    {
        BotView botView = Object.Instantiate(_prefab, position, Quaternion.identity);
        StorageModel storageModel = new();
        botView.Init(storageModel, target, botBase);

        return botView;
    }
}
