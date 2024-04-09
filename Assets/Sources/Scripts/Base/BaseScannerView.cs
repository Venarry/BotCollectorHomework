using System.Collections.Generic;
using UnityEngine;

public class BaseScannerView : MonoBehaviour
{
    private IInputsProvider _inputsProvider;
    private ResourcesPool _resourcesPool;
    private ScannedResourcesProvider _resourcesProvider;
    private readonly Dictionary<ITarget, GameObject> _scannedResources = new();
    private readonly Dictionary<ITarget, GameObject> _collectingResources = new();

    public void Init(
        IInputsProvider inputsProvider,
        ResourcesPool resourcesPool,
        ScannedResourcesProvider resourcesProvider)
    {
        _inputsProvider = inputsProvider;
        _resourcesPool = resourcesPool;
        _resourcesProvider = resourcesProvider;

        _resourcesProvider.ScannedResourceAdded += OnScannedResourceAdd;
        _resourcesProvider.ScannedResourceRemoved += OnScannedResourceRemove;
        _resourcesProvider.CollectingResourceAdded += OnCollectingResourceAdd;
        _resourcesProvider.CollectingResourceDestroyed += OnCollectingResourceDestroy;
    }


    private void OnDestroy()
    {
        _resourcesProvider.ScannedResourceAdded -= OnScannedResourceAdd;
        _resourcesProvider.ScannedResourceRemoved -= OnScannedResourceRemove;
        _resourcesProvider.CollectingResourceAdded -= OnCollectingResourceAdd;
        _resourcesProvider.CollectingResourceDestroyed -= OnCollectingResourceDestroy;
    }

    private void Update()
    {
        if (_inputsProvider.IsPressedScan)
        {
            _resourcesProvider.Clear();
            CoalView[] scennedResources = _resourcesPool.GetAll();

            foreach(CoalView resource in scennedResources)
            {
                _resourcesProvider.Add(resource);
            }
        }
    }

    private void OnScannedResourceAdd(ITarget coal)
    {
        GameObject scanMarkPrefab = Resources.Load<GameObject>(Path.ScanMark);
        GameObject scanMark = Instantiate(
            scanMarkPrefab, coal.Transform.position, Quaternion.identity);

        _scannedResources.Add(coal, scanMark);
    }

    private void OnScannedResourceRemove(ITarget coal)
    {
        Destroy(_scannedResources[coal]);
        _scannedResources.Remove(coal);
    }

    private void OnCollectingResourceAdd(ITarget coal)
    {
        GameObject collectMarkPrefab = Resources.Load<GameObject>(Path.CollectMark);
        GameObject collectMark = Instantiate(
            collectMarkPrefab, coal.Transform.position, Quaternion.identity);

        _collectingResources.Add(coal, collectMark);
    }

    private void OnCollectingResourceDestroy(ITarget coal)
    {
        Destroy(_collectingResources[coal]);
        _collectingResources.Remove(coal);
    }
}
