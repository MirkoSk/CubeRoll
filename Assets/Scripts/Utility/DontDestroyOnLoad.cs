using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prevents the GameObject from destroying itself on scene load.
/// 
/// Author: Mirko Skroch
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour {

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
