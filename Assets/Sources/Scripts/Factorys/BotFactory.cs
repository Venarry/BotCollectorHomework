using UnityEngine;

public class BotFactory : MonoBehaviour
{
    private readonly BotView _prefab = Resources.Load<BotView>(Path.Bot);

    public BotView Create(
        Vector3 position,
        CoalView[] targets,
        Vector3 botBasePosition)
    {
        BotView botView = Instantiate(_prefab, position, Quaternion.identity);

        ResourcesProvider provider = new(targets);
        botView.Init(provider, botBasePosition);

        return botView;
    }
}
