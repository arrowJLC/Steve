using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Add this to a gameobject with a trigger collider so that the checkpoints can be updates
/// </summary>
public class UpdateCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GameManager.Instance.UpdateCheckpoint(transform);
    }
}