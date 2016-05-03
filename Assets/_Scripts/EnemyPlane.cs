using UnityEngine;
using System.Collections;

public class EnemyPlane : MonoBehaviour {

	public GameObject planeExplosion;

    // AI
    public GameObject target;

    public float speed = 150.0f;

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

		float angle = Vector2.Angle (body.velocity, Vector2.up);
		if (body.velocity.x > 0) 
		{
			angle *= -1;
		}
		body.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		//body.transform.rotation = Quaternion.LookRotation(new Vector3(body.velocity.x, body.velocity.y, 0), Vector3.back);
		//Vector3 position = new Vector3(body.position.x, body.position.y, 0);
		//Vector3 velocity = new Vector3(body.velocity.x, body.velocity.y, 0);
		//body.transform.LookAt(position + velocity, Vector3.);
		//Quaternion.Angle(transform)

		body.AddForce(dir * speed);

        if (dist > APPROACH_DISTANCE)
        {
            // approach
            //float force = body.velocity.magnitude - speed;
            
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
