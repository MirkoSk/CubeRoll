using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

/// <summary>
/// 
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

    // State
    float timer;
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
        rb = GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Contains(Constants.TAG_PLAYER) 
            && timer >= collisionCooldown
            && (rb.velocity - collision.rigidbody.velocity).magnitude >= collisionSpeedThreshold)
        {
            stateMachine.Next();
            timer = 0;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }
    #endregion
}
