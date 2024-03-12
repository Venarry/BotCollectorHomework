using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ResourcesSpawner _resourcesSpawner;

    private void Awake()
    {
        ResourcesPool resourcesPool = new();
        BaseFactory baseFactory = new();
        BotFactory botFactory = new();
        CoalFactory coalFactory = new();
        IInputsProvider inputsProvider = new KeyboardInputsProvider();

        baseFactory.Create(
            Vector3.zero,
            inputsProvider,
            resourcesPool,
            botFactory,
            baseResourcesValue: 0,
            baseBotsCount: 3);

        //coalFactory.Create(new Vector3(5,0,5), resourcesPool);
        //coalFactory.Create(new Vector3(15,0,5), resourcesPool);
        //coalFactory.Create(new Vector3(5,0,15), resourcesPool);
        //coalFactory.Create(new Vector3(15,0,15), resourcesPool);

        _resourcesSpawner.Init(coalFactory, resourcesPool);
        _resourcesSpawner.StartSpawn(interval: 1f);
    }
}
