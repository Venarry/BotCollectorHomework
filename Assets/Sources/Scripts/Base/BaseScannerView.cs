using System.Collections.Generic;
using UnityEngine;

public class BaseScannerView : MonoBehaviour
{
    private IInputsProvider _inputsProvider;
    private ResourcesPool _resourcesPool;
    private readonly Dictionary<CoalView, GameObject> _scannedResources = new();

    public void Init(
        IInputsProvider inputsProvider,
        ResourcesPool resourcesPool)
    {
        _inputsProvider = inputsProvider;
        _resourcesPool = resourcesPool;
    }

    private void Update()
    {
        if (_inputsProvider.IsPressedScan)
        {
            ClearScannedResources();
            CoalView[] scennedResources = _resourcesPool.GetAll();

            foreach(CoalView resource in scennedResources)
            {
                GameObject scanMarkPrefab = Resources.Load<GameObject>(Path.ScanMark);
                GameObject scanMark = Instantiate(
                    scanMarkPrefab, resource.transform.position, Quaternion.identity);

                _scannedResources.Add(resource, scanMark);
                resource.Destroyed += OnResourceDestroyed;
            }
        }
    }

    private void OnResourceDestroyed(CoalView coal)
    {
        coal.Destroyed -= OnResourceDestroyed;
        Destroy(_scannedResources[coal]);
        _scannedResources.Remove(coal);
    }

    private void ClearScannedResources()
    {
        _scannedResources.Clear();
    }
}
