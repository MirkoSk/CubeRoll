using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple Selfdestruct script. Destroys the GameObject after the defined time.
/// 
/// Author: Mirko Skroch
/// </summary>
public class Selfdestruct : MonoBehaviour 
{
    public float lifetime = 5f;

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}
