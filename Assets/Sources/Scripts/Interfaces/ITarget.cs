using System;
using UnityEngine;

public interface ITarget
{
    public Transform Transform { get; }
    public event Action<ITarget> Destroyed;
    public void Destroy();
}
