using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputsProvider : IInputsProvider
{
    public bool IsPressedScan => Input.GetKeyDown(KeyCode.E);
    public bool IsPressedClick => Input.GetMouseButtonDown(0);
}
