using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour {

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
