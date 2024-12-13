using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField] private float distThreshold = 5;

    [SerializeField] private float projectileFireRate = 2;
    private float timeSinceLastFire = 0;
    public override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2;

        if (distThreshold <= 0)
            distThreshold = 5;
    }

    private void Update()
    {
        if (!GameManager.Instance.PlayerInstance) return;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        sr.flipX = (transform.position.x > GameManager.Instance.PlayerInstance.transform.position.x);

        float distance = Vector2.Distance(transform.position, GameManager.Instance.PlayerInstance.transform.position);

        if (distance <= distThreshold)
        {
            sr.color = Color.red;
            if (curPlayingClips[0].clip.name.Contains("Idle"))
            {
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                   anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                }
            }
        }
        else
            sr.color = Color.white;
    }


}