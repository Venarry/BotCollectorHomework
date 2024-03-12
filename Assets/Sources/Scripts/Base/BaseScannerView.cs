using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseScannerView : MonoBehaviour
{
    private IInputsProvider _inputsProvider;
    private ResourcesPool _resourcesPool;
    private ResourcesProvider _resourcesProvider;
    private readonly Dictionary<CoalView, GameObject> _scannedResources = new();

    public void Init(
        IInputsProvider inputsProvider,
        ResourcesPool resourcesPool,
        ResourcesProvider resourcesProvider)
    {
        _inputsProvider = inputsProvider;
        _resourcesPool = resourcesPool;
        _resourcesProvider = resourcesProvider;

        _resourcesProvider.Added += OnResourceAdd;
        _resourcesProvider.Removed += OnResourceDestroy;
    }

    private void OnDestroy()
    {
        _resourcesProvider.Added -= OnResourceAdd;
        _resourcesProvider.Removed -= OnResourceDestroy;
    }

    private void Update()
    {
        if (_inputsProvider.IsPressedScan)
        {
            //ClearScannedResources();
            _resourcesProvider.Clear();
            CoalView[] scennedResources = _resourcesPool.GetAll();

            foreach(CoalView resource in scennedResources)
            {
                _resourcesProvider.Add(resource);
            }
        }
    }

    private void OnResourceAdd(CoalView coalView)
    {
        GameObject scanMarkPrefab = Resources.Load<GameObject>(Path.ScanMark);
        GameObject scanMark = Instantiate(
            scanMarkPrefab, coalView.transform.position, Quaternion.identity);

        _scannedResources.Add(coalView, scanMark);
    }

    private void OnResourceDestroy(CoalView coal)
    {
        Destroy(_scannedResources[coal]);
        _scannedResources.Remove(coal);
    }

    private void ClearScannedResources()
    {
        /*foreach (KeyValuePair<CoalView, GameObject> resource in _scannedResources)
        {
            resource.Key.Destroyed -= OnResourceDestroy;
            Destroy(resource.Value);
        }

        _scannedResources.Clear();*/

        _resourcesProvider.Clear();
    }
}
