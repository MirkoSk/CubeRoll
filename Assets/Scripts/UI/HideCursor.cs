using UnityEngine;

/// <summary>
/// Simple script that hides or shows the cursor in the current scene.
/// 
/// Author: Mirko Skroch
/// </summary>
public class HideCursor : MonoBehaviour 
{

    #region Variable Declarations
    [SerializeField] bool hideCursor = true;
    [SerializeField] CursorLockMode cursorLockMode = CursorLockMode.Confined;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
#if !UNITY_EDITOR
        // Show cursor in built game
        Cursor.lockState = cursorLockMode;
        Cursor.visible = hideCursor;
#endif
    }
    #endregion
}
