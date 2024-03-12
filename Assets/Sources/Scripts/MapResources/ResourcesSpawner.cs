using System.Collections;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private float _squadRange;
    private CoalFactory _coalFactory;
    private ResourcesPool _pool;

    public void Init(
        CoalFactory coalFactory,
        ResourcesPool resourcesPool)
    {
        _coalFactory = coalFactory;
        _pool = resourcesPool;
    }

    public void StartSpawn(float interval)
    {
        WaitForSeconds waitForSeconds = new(interval);
        StartCoroutine(Spawning(waitForSeconds));
    }

    private IEnumerator Spawning(WaitForSeconds waitForSeconds)
    {
        while (true)
        {
            float spawnPositionX = Random.Range(-_squadRange, _squadRange);
            float spawnPositionZ = Random.Range(-_squadRange, _squadRange);

            Vector3 spawnPosition = new(
                transform.position.x + spawnPositionX,
                0,
                transform.position.z + spawnPositionZ);

            _coalFactory.Create(spawnPosition, _pool);

            yield return waitForSeconds;
        }
    }
}
