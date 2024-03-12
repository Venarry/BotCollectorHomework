using System.Collections.Generic;
using UnityEngine;

public class BaseScannerView : MonoBehaviour
{
    private IInputsProvider _inputsProvider;
    private ResourcesPool _resourcesPool;
    private ScannedResourcesProvider _resourcesProvider;
    private readonly Dictionary<CoalView, GameObject> _scannedResources = new();
    private readonly Dictionary<CoalView, GameObject> _collectingResources = new();

    public void Init(
        IInputsProvider inputsProvider,
        ResourcesPool resourcesPool,
        ScannedResourcesProvider resourcesProvider)
    {
        _inputsProvider = inputsProvider;
        _resourcesPool = resourcesPool;
        _resourcesProvider = resourcesProvider;

        _resourcesProvider.ScannedCoalAdded += OnScannedCoalAdd;
        _resourcesProvider.ScannedCoalRemoved += OnScannedCoalRemove;
        _resourcesProvider.CollectingCoalAdded += OnCollectingCoalAdd;
        _resourcesProvider.CollectingCoalDestroyed += OnCollectingCoalDestroy;
    }


    private void OnDestroy()
    {
        _resourcesProvider.ScannedCoalAdded -= OnScannedCoalAdd;
        _resourcesProvider.ScannedCoalRemoved -= OnScannedCoalRemove;
        _resourcesProvider.CollectingCoalAdded -= OnCollectingCoalAdd;
        _resourcesProvider.CollectingCoalDestroyed -= OnCollectingCoalDestroy;
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

    private void OnScannedCoalAdd(CoalView coal)
    {
        GameObject scanMarkPrefab = Resources.Load<GameObject>(Path.ScanMark);
        GameObject scanMark = Instantiate(
            scanMarkPrefab, coal.transform.position, Quaternion.identity);

        _scannedResources.Add(coal, scanMark);
    }

    private void OnScannedCoalRemove(CoalView coal)
    {
        Destroy(_scannedResources[coal]);
        _scannedResources.Remove(coal);
    }

    private void OnCollectingCoalAdd(CoalView coal)
    {
        GameObject collectMarkPrefab = Resources.Load<GameObject>(Path.CollectMark);
        GameObject collectMark = Instantiate(
            collectMarkPrefab, coal.transform.position, Quaternion.identity);

        _collectingResources.Add(coal, collectMark);
    }

    private void OnCollectingCoalDestroy(CoalView coal)
    {
        Destroy(_collectingResources[coal]);
        _collectingResources.Remove(coal);
    }
}
