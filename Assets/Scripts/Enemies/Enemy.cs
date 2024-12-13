using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    //private - private to the class that created it.  Only a property of the class - nobody else can access it, not even inherited classes.
    //protected - private but also accessable to inherited classes.
    //public - it's a party and everyones invited. Any object that has reference to this class can access this property.

    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;
    [SerializeField] protected int maxHealth;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0) maxHealth = 5;

        health = maxHealth;
    }

    public virtual void TakeDamage(int damageValue)
    {
        health -= damageValue;

        if (health <= 0)
        {
            anim.SetTrigger("Death");

            if (transform.parent != null) Destroy(transform.parent.gameObject, 0.5f);
            else Destroy(gameObject, 0.5f);
        }
    }
}