using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

/// <summary>
/// Handles the damage level of the cube.
/// 
/// Author: Mirko Skroch
/// </summary>
[RequireComponent(typeof(CubeController))]
public class CubeDamage : MonoBehaviour 
{

    #region Variable Declarations
    [Space]
    [SerializeField] float collisionCooldown = 3f;
    [SerializeField] float collisionSpeedThreshold = 2f;

    [Space]
    [SerializeField] StateMachine stateMachine;

    // Configuration
    Rigidbody rb;
    CubeController cubeController;

    // State
    float timer;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
        rb = GetComponent<Rigidbody>();
        cubeController = GetComponent<CubeController>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Contains(Constants.TAG_PLAYER)
            && (rb.velocity - collision.rigidbody.velocity).magnitude >= collisionSpeedThreshold)
        {
            TakeDamage();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }
    #endregion



    #region Public Functions
    /// <summary>
    /// 
    /// </summary>
    /// <returns>False if the cube couldn't take the damage and died :(</returns>
    public bool TakeDamage()
    {
        if (timer >= collisionCooldown)
        {
            timer = 0;
            AudioManager.Instance.PlaySound(Constants.SOUND_DAMAGE);
            if (stateMachine.currentState == stateMachine.Next())
            {
                cubeController.Respawn();
                return false;
            }
        }
        return true;
    }
    #endregion
}
