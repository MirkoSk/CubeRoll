using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class defining ExtensionMethods for this project.
/// 
/// Author: Mirko Skroch
/// </summary>
public static class ExtensionMethods
{
    #region Collider
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
    #endregion



    #region AudioSource
    /// <summary>
    /// Plays the clip with a specified Fade-In time.
    /// </summary>
    /// <param name="fadeInTime">Length of the Fade-In in seconds</param>
    public static void Play(this AudioSource source, float fadeInTime)
    {
        float originalVolume = source.volume;
        source.volume = 0f;
        source.Play();
        LeanTween.value(source.gameObject, (float f) => { source.volume = f; }, 0f, originalVolume, fadeInTime)
                 .setEase(LeanTweenType.easeInOutQuad);
    }

    /// <summary>
    /// Stops playing the clip with a specified Fade-Out time.
    /// </summary>
    /// <param name="fadeOutTime">Length of the Fade-Out in seconds</param>
    public static void Stop(this AudioSource source, float fadeOutTime)
    {
        float originalVolume = source.volume;
        LeanTween.value(source.gameObject, (float f) => { source.volume = f; }, originalVolume, 0f, fadeOutTime)
                 .setEase(LeanTweenType.easeInOutQuad)
                 .setOnComplete(() => {
                     source.Stop();
                     source.volume = originalVolume;
                 });
    }

    /// <summary>
    /// Cross-Fades between two AudioSources over the specified time.
    /// </summary>
    /// <param name="otherSource">Reference to the AudioSource that shall fade in</param>
    /// <param name="fadingTime">Length of the Cross-Fade in seconds</param>
    public static void CrossFade(this AudioSource thisSource, AudioSource otherSource, float fadingTime)
    {
        float originalVolumeThis = thisSource.volume;
        float originalVolumeOther = otherSource.volume;
        LeanTween.value(thisSource.gameObject, (float f) => { thisSource.volume = f; }, originalVolumeThis, 0f, fadingTime)
                 .setEase(LeanTweenType.easeInOutQuad)
                 .setOnComplete(() => {
                     thisSource.Stop();
                     thisSource.volume = originalVolumeThis;
                 });
        LeanTween.value(otherSource.gameObject, (float f) => { otherSource.volume = f; }, 0f, originalVolumeOther, fadingTime)
                 .setEase(LeanTweenType.easeInOutQuad);
        otherSource.Play();
    }
    #endregion
}
