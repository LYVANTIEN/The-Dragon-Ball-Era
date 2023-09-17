using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Vector2 lastCheckpointPosition;

    
    public Animator CheckPointAnim; // This line is causing the error.

    private bool firstTime = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointAnim.SetFloat("Active", 1.0f);
            lastCheckpointPosition = transform.position;
           

            // This is where the error occurs.
        }
    }
}
