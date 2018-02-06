using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Offers functions for playing 2D audio. This class is a singleton and won't be destroyed on load
/// 
/// Author: Mirko Skroch
/// </summary>
public class AudioManager : MonoBehaviour {

    #region Variable Declarations
    // Visible in Inspector
    [SerializeField] AudioMixer masterMixer;
    public AudioMixer MasterMixer { get { return masterMixer; } }
    [SerializeField] AudioTrack[] audioTracks;
    
    Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    public static AudioManager Instance;
    #endregion



    #region Unity Event Functions
    private void Awake() {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
        }

        //If instance already exists and it's not this:
        else if (Instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of an AudioManager.
            Destroy(this);
        }
    }

    private void Start()
    {
        SpawnAudioSources();
        FillDictionary();
    }
    #endregion



    #region Public Functions
    public void PlaySound(string name) {
        AudioSource src;
        audioSources.TryGetValue(name, out src);
        src.Play();
    }

    public void PlaySoundOneShot(string name) {
        AudioSource src;
        audioSources.TryGetValue(name, out src);
        src.PlayOneShot(src.clip);
    }

    public void PlaySoundScheduled(string name, double time) {
        AudioSource src;
        audioSources.TryGetValue(name, out src);
        src.PlayScheduled(time);
    }

    public void PlayMenuConfirm() {
        AudioSource src;
        audioSources.TryGetValue(Constants.SOUND_MENU_CONFIRM, out src);
        src.Play();
    }

    public void SetMusicVolume(float volume)
    {
        masterMixer.SetFloat(Constants.MIXER_MUSIC_VOLUME, volume);
    }

    public void SetSFXVolume(float volume)
    {
        masterMixer.SetFloat(Constants.MIXER_SFX_VOLUME, volume);
    }

    public AudioSource GetAudioSource(string name) {
        AudioSource src;
        audioSources.TryGetValue(name, out src);
        return src;
    }
    #endregion



    #region Private Functions
    void SpawnAudioSources()
    {
        foreach (AudioTrack track in audioTracks)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = track.clip;
            source.outputAudioMixerGroup = track.output;
            source.mute = track.mute;
            source.bypassEffects = track.bypassEffects;
            source.bypassListenerEffects = track.bypassListenerEffects;
            source.playOnAwake = track.playOnAwake;
            source.loop = track.loop;
            source.priority = track.priority;
            source.volume = track.volume;
            source.pitch = track.pitch;
            source.panStereo = track.stereoPan;
        }
    }

    void FillDictionary()
    {
        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            audioSources.Add(source.clip.name, source);
        }
    }
    #endregion
}
