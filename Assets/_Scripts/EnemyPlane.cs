using UnityEngine;
using System.Collections;

public class EnemyPlane : MonoBehaviour {

	public GameObject planeExplosion;

    // AI
    public GameObject target;
    public float speed = 5.0f;  // forward speed
    public float minTurnDist = 1.0f;  // distance from target plane starts to turn at
    public float maxTurnSpeed = 45.0f;  // maximum angular velocity

	// Use this for initialization
	void Start()
    {
	
	}

    void Awake()
    {
        if (target == null)
            target = GameObject.Find("Battleship");
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        Vector2 dir = target.transform.position - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector2 forward = body.transform.rotation * Vector2.up;

        if (dist >= minTurnDist)
        {
            float ang = Util.VectorAngleWithSign(forward, dir);
            body.angularVelocity = Mathf.Clamp(ang, -maxTurnSpeed, maxTurnSpeed);
        }
        
        body.velocity = forward * speed / (1 + Mathf.Abs(body.angularVelocity / 180.0f));
    }

    void OnDestroyed()
    {
        // rekt
		Instantiate(planeExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
