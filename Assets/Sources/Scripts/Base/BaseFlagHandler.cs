using UnityEngine;

public class BaseFlagHandler
{
    private readonly BaseFlag _flagPrefab;
    private readonly IStateSwitcher _baseStateSwitcher;

    public GameObject ProectionPrefab { get; private set; }
    public BaseFlag ActiveFlag { get; private set; }

    public BaseFlagHandler(
        BaseFlag flagPrefab,
        GameObject proectionPrefab,
        IStateSwitcher baseStateSwitcher)
    {
        ProectionPrefab = proectionPrefab;
        _flagPrefab = flagPrefab;
        _baseStateSwitcher = baseStateSwitcher;
    }

    public void SpawnFlag(Vector3 position)
    {
        if(ActiveFlag != null)
        {
            RemoveFlag();
        }

        ActiveFlag = Object.Instantiate(_flagPrefab, position, Quaternion.identity);
        _baseStateSwitcher.Switch<BaseBuildNewBaseState>();
    }

    public void RemoveFlag()
    {
        ActiveFlag.Destroy();
        ActiveFlag = null;
    }
}
