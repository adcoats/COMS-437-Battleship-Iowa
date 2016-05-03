using UnityEngine;

/**
 * Represents an object with health that can be destroyed by projectiles.
 * Call Damage(amt) or lower the "health" property to deal damage.
 * If the object has not already been destroyed, OnDamage(amt) will be triggered on
 * all script components this GameObject via SendMessage.
 * If health hits zero or below, OnDestroyed() will be called.
 */
public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health;

    // trying to set lower than our current health will re-route
    // to Damage() to make sure callbacks fire
    public float health
    {
        get { return _health; }
        set
        {
            if (value < _health)
                Damage(_health - value);
            else
                _health = health;
        }
    }

    // convenience getter for seeing if we've been destroyed
    public bool destroyed { get { return health <= 0; } }

    public void Damage(float amt)
    {
        if (destroyed)
            return;

        _health -= amt;
        SendMessage("OnDamaged", amt, SendMessageOptions.DontRequireReceiver);

        if (_health <= 0)
            SendMessage("OnDestroyed", SendMessageOptions.DontRequireReceiver);
    }
}
