using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ResourcesSpawner _resourcesSpawner;
    [SerializeField] private Camera _camera;
    [SerializeField] private UserInteractHandler _userInteractHandler;
    [SerializeField] private PanelClickHandler _panelClickHandler;

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

        _userInteractHandler.Init(_panelClickHandler, _camera);

        _resourcesSpawner.Init(coalFactory, resourcesPool);
        _resourcesSpawner.StartSpawn(interval: 1f);
    }
}
