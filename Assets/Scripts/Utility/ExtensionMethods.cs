using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// Begins at the hit.transform and goes up in the hierarchy. 
    /// Returns the fist component of type T found.
    /// </summary>
    public static T FindComponentInParents<T>(this Collider hit) where T : Component
    {
        Transform transform = hit.transform;
        while (transform.GetComponent<T>() == null)
        {
            if (transform.parent == null)
            {
                Debug.LogError("Expected to find component of type "
               + typeof(T) + " in parents of " + hit.name + ", but found none.");
                break;
            }

            transform = transform.parent;
        }
        return transform.GetComponent<T>();
    }
}
