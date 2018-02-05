using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class containing all strings and constants used in the game.
/// 
/// Author: Melanie Ramsch, Mirko Skroch
/// </summary>
public class Constants 
{
    #region Inputs
    public static readonly string INPUT_HORIZONTAL = "Horizontal";
    public static readonly string INPUT_VERTICAL = "Vertical";
    public static readonly string INPUT_SUBMIT = "Submit";
    public static readonly string INPUT_CANCEL = "Cancel";
    public static readonly string INPUT_ESCAPE = "Escape";
    public static readonly string INPUT_DEBUGMODE = "DebugMode";
    public static readonly string INPUT_FIRE = "Fire";
    #endregion

    #region Tags and Layers
    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_START_AREA = "StartArea";
    public static readonly string TAG_ENVIRONMENT_PARENT = "EnvironmentParent";
    public static readonly string TAG_DYNAMIC_OBJECTS_PARENT = "DynamicObjectsParent";
    #endregion

    #region Sounds
    // Exposed Parameters in Mixers
    public static readonly string MIXER_SFX_VOLUME = "SFXVolume";
    public static readonly string MIXER_MUSIC_VOLUME = "MusicVolume";

    // AudioClip names
    public static readonly string SOUND_TRACK01INTRO = "Track01Intro";
    public static readonly string SOUND_TRACK01LOOP = "Track01Loop";
    public static readonly string SOUND_TRACK02LOOP = "Track02Loop";
    public static readonly string SOUND_MINE_PLOP = "MinePlop";
    public static readonly string SOUND_CUBE_HIT = "CubeHit";
    public static readonly string SOUND_MENU_CONFIRM = "MenuConfirm";
    #endregion
}