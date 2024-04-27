using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ResourcesSpawner _resourcesSpawner;
    [SerializeField] private Camera _camera;
    [SerializeField] private UserInteractHandler _userInteractHandler;
    [SerializeField] private PanelClickHandler _panelClickHandler;
    [SerializeField] private BaseScannerView _scannerView;

    private void Awake()
    {
        IInputsProvider inputsProvider = new KeyboardInputsProvider();
        ResourcesPool resourcesPool = new();
        ScannedResourcesProvider scannedResourcesProvider = new();

        BotFactory botFactory = new();
        BaseFactory baseFactory = new(scannedResourcesProvider);
        CoalFactory coalFactory = new();

        botFactory.Init(baseFactory);
        baseFactory.Init(botFactory);
        _scannerView.Init(inputsProvider, resourcesPool, scannedResourcesProvider);

        baseFactory.Create(
            Vector3.zero,
            baseResourcesValue: 0,
            baseBotsCount: 3);

        _userInteractHandler.Init(_panelClickHandler, _camera);

        _resourcesSpawner.Init(coalFactory, resourcesPool);
        _resourcesSpawner.StartSpawn(interval: 1f);
    }
}
