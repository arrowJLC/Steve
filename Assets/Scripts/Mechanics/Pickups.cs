using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is fine for a smaller scope but it becomes harder to scale
public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        PowerupJump,
        Score
    }

   // public AudioClip pickupSound;

    public PickupType type;
    SpriteRenderer sr;
    //AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
       // audioSource = GetComponent<AudioSource>();

       // audioSource.outputAudioMixerGroup = GameManager.Instance.SFXGroup;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    break;
                case PickupType.PowerupJump:
                    pc.JumpPowerup();
                    break;
                case PickupType.Score:
                    //pc.score++;
                    break;
            }
            sr.enabled = false;
            //audioSource.PlayOneShot(pickupSound);

            //Destroy(gameObject, pickupSound.length);
        }
    }
}