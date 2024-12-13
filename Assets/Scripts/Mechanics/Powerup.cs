using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IPickUp
{
    public void Pickup(GameObject mario)
    {
        PlayerController pc = mario.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.JumpPowerup(); // Trigger the jump power-up effect
            Destroy(gameObject); // Destroys the power-up on collision with the player
        }
    }
}
