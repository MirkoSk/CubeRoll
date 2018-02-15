using UnityEngine;

/// <summary>
/// Shows the Tutorial Screen on the beginning of the game.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(Collider))]
public class Tutorial : MonoBehaviour 
{

    #region Variable Declarations
    [SerializeField] GameObject tutorialScreenPlayer1;
    [SerializeField] GameObject tutorialScreenPlayer2;
    #endregion



    #region Unity Event Functions
    private void OnTriggerEnter(Collider other)
    {
        // Since Triggers don't understand compound colliders: Go up the hierarchy until you hit the gameObject with the rigidbody
        CubeController parent = other.FindComponentInParents<CubeController>();

        if (parent.tag.Contains(Constants.TAG_PLAYER))
        {
            if (parent.PlayerNumber == 1) tutorialScreenPlayer1.SetActive(true);
            else if (parent.PlayerNumber == 2) tutorialScreenPlayer2.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Since Triggers don't understand compound colliders: Go up the hierarchy until you hit the gameObject with the rigidbody
        CubeController parent = other.FindComponentInParents<CubeController>();

        if (parent.tag.Contains(Constants.TAG_PLAYER))
        {
            if (parent.PlayerNumber == 1) tutorialScreenPlayer1.SetActive(false);
            else if (parent.PlayerNumber == 2) tutorialScreenPlayer2.SetActive(false);
        }
    }
    #endregion
}
