using UnityEngine;

/// <summary>
/// Simple script that hides or shows the cursor in the current scene.
/// 
/// Author: Mirko Skroch
/// </summary>
public class HideCursor : MonoBehaviour 
{

    #region Variable Declarations
#pragma warning disable 0414
    [SerializeField] bool hideCursor = true;
    [SerializeField] CursorLockMode cursorLockMode = CursorLockMode.Confined;
#pragma warning restore 0414
    #endregion



    #region Unity Event Functions
    private void Start () 
	{
#if !UNITY_EDITOR
        // Show cursor in built game
        Cursor.lockState = cursorLockMode;
        Cursor.visible = !hideCursor;
#endif
    }
    #endregion
}
