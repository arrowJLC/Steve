using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    public Vector2 initialShotVelocity;
    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public Projectile projectilePrefab;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Check default values and log warnings if necessary
        if (initialShotVelocity == Vector2.zero)
        {
            Debug.LogWarning("Initial shot velocity is zero; setting to a default value");
            initialShotVelocity.x = 7.0f; // Default horizontal velocity
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.LogWarning($"Please set all required references on the Shoot script for {gameObject.name}");
        }
    }

    public void Fire()
    {
        Vector2 shotVelocity = initialShotVelocity;

        if (sr.flipX)
        {
            // Facing left
            shotVelocity.x = -Mathf.Abs(initialShotVelocity.x);
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.SetVelocity(shotVelocity);
        }
        else
        {
            // Facing right
            shotVelocity.x = Mathf.Abs(initialShotVelocity.x);
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.SetVelocity(shotVelocity);
        }
    }
}
