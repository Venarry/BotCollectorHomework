using UnityEngine;

public interface IInputsProvider
{
    public bool IsPressedScan { get; }
    public bool IsPressedClick { get; }
}
