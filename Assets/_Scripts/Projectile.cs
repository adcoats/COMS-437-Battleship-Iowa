using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    // how much damage this projectile deals on collision
    public float damage = 1.0f;

    // after lifetime seconds pass, projectile will be removed
    public float lifeTime = 1.0f;

    // when this object was created, used to check if lifeTime has been reached
    private float _spawnTime;

	void Start ()
    {
        _spawnTime = Time.time;

	}
	
	void Update ()
    {
        // did we reach our lifetime?
	    if (Time.time - _spawnTime > lifeTime)
        {
            Destroy(gameObject);
            return;
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject other = col.collider.gameObject;

        // did we hit something we can deal damage to?
        Health damageable = other.GetComponent<Health>();
        if (damageable != null)
        {
            // TODO type effectiveness (check other's layer?)
            damageable.Damage(damage);

            Destroy(gameObject);
        }
    }
}
