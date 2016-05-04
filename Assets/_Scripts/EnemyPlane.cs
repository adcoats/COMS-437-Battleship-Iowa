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

    // http://answers.unity3d.com/questions/162177/vector2angles-direction.html
    // pure fucking magic
    private float VectorAngleWithSign(Vector2 a, Vector2 b)
    {
        float ang = Vector2.Angle(a, b);
        Vector3 cross = Vector3.Cross(a, b);
 
        if (cross.z > 0)
            ang = 360 - ang;

        if (ang > 180)
            ang = -(360 - ang);

        //if (a.x > 0)
        //    ang *= -1;

        return -ang;
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
            float ang = VectorAngleWithSign(forward, dir);
            body.angularVelocity = Mathf.Clamp(ang, -maxTurnSpeed, maxTurnSpeed);
        }
        
        body.velocity = forward * speed;
    }

    void OnDestroyed()
    {
        // rekt
		Instantiate(planeExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
