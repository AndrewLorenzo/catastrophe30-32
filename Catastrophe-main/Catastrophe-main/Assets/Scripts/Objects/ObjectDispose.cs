using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispose : MonoBehaviour
{
    Transform playerTransform;
    float maxDistance = 25f;

    private void Start()
    {
        if (GameManager.instance != null && GameManager.instance.playerTransform != null)
        {
            playerTransform = GameManager.instance.playerTransform;
        }
        else
        {
            // Debug.LogError("Player Transform is null. Unable to set playerTransform.");
        }
    }


    private void Update()
    {
        // Check if the game is paused
        if (IsGamePaused())
        {
            return;
        }

        // Check if playerTransform is null or not
        if (playerTransform == null)
        {
            // Debug.LogError("Player Transform is null. Cannot perform distance check.");
            return;
        }

        // Check if object is null
        if (gameObject == null)
        {
            return; // Object is already destroyed, no need to proceed further
        }

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
}
