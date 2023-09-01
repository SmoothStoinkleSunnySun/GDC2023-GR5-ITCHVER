using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPlease : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}

