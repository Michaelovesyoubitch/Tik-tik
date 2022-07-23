using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{

    //онбнпнр аюпнб гднпнбэъ й цкюбмни йюлепе.
    void Update()
    {
        if (Camera.main != null)
        transform.LookAt(Camera.main.transform.position);
    }
}
