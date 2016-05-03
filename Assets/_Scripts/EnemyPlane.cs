using UnityEngine;
using System.Collections;

public class EnemyPlane : MonoBehaviour {

	public GameObject planeExplosion;

    // AI
    public GameObject target;

    public float speed = 10.0f;

    private const float CIRCLE_RADIUS = 10.0f;
    private const float APPROACH_DISTANCE = 2.0f;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        Vector2 dir = target.transform.position - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        Rigidbody2D body = GetComponent<Rigidbody2D>();

        if (dist > APPROACH_DISTANCE)
        {
            // approach
            float force = body.velocity.magnitude - speed;
            body.AddForce(dir * speed);
        } else {
            // circle

        }
	}

    void OnDestroyed()
    {
        // rekt
		Instantiate(planeExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
