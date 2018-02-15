using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Returns to the main menu when pressing Escape.
/// 
/// Author: Mirko Skroch
/// </summary>
public class PauseMenu : MonoBehaviour 
{
    #region Unity Event Functions
    private void Update()
    {
        if (Input.GetButtonDown(Constants.INPUT_ESCAPE) && SceneManager.GetActiveScene().buildIndex != Constants.MENU_SCENE)
        {
            SceneManager.LoadScene(Constants.MENU_SCENE);
        } else if (Input.GetButtonDown(Constants.INPUT_ESCAPE))
        {
            Application.Quit();
        }
    }
    #endregion
}
