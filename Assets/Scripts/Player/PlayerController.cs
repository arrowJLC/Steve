using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck), typeof(Jump), typeof(Shoot))]
public class PlayerController : MonoBehaviour
{
    //Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    GroundCheck gc;
   // public AudioSource audioSource { get; private set; }

    //

    //Movement variables
    [Range(3, 10)]
    public float speed = 5.5f;
    [Range(3, 10)]
    public float bounceForce = 10;

    public bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<GroundCheck>();
        //audioSource = GetComponent<AudioSource>();

        //audioSource.outputAudioMixerGroup = GameManager.Instance.SFXGroup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        CheckIsGrounded();
        float hInput = Input.GetAxis("Horizontal");

        if (curPlayingClips.Length > 0)
        {
            if (!(curPlayingClips[0].clip.name == "Fire"))
            {
                rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
            }

        }

        //sprite flipping
        if (hInput != 0) sr.flipX = (hInput < 0);

        //inputs for firing and jump attack
        if (Input.GetButtonDown("Fire1") && isGrounded) anim.SetTrigger("fire");
        if (Input.GetButtonDown("Fire1") && !isGrounded) anim.SetTrigger("jumpAttack");

        //alternate way to sprite flip
        //if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX;

        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }

    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.velocity.y <= 0) isGrounded = gc.IsGrounded();
        }
        else isGrounded = gc.IsGrounded();
    }

    public void JumpPowerup()
    {
        StartCoroutine(GetComponent<Jump>().jumpHeightChange());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish"))
        {
            collision.enabled = false;
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(9999);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
           // audioSource.PlayOneShot(stompSound);
        }

        IPickUp curPickup = collision.GetComponent<IPickUp>();
        if (curPickup != null)
        {
            curPickup.Pickup(gameObject);
        }
    }
}