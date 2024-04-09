using System;
using UnityEngine;

public class BaseFlag : MonoBehaviour, ITarget
{
    public Transform Transform => transform;

    public event Action<ITarget> Destroyed;

    public void Destroy()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
