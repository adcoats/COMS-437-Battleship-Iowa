﻿using UnityEngine;
using System.Collections;

public class EnemyPlane : MonoBehaviour {

	public GameObject planeExplosion;
	public Vector3 initPos;
	public float searchRange = 0.5f; // The plane will search for the player in this radius from itself.
	public bool found;
	
    // AI
    public GameObject target;
    public float speed = 5.0f;  // forward speed
    public float minTurnDist = 1.0f;  // distance from target plane starts to turn at
    public float maxTurnSpeed = 45.0f;  // maximum angular velocity
    public float aggroRange = 25.0f;

	// Use this for initialization
	void Start()
    {
		this.initPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		found = false;
	}

    void Awake()
    {
        if (target == null)
            target = GameObject.Find("Battleship");
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        if (target == null || (target.transform.position - transform.position).magnitude > aggroRange)
        {
            if (body.velocity.sqrMagnitude > 0.001f)
                body.velocity *= 0.9f;
            return;
        }

        Vector2 dir = target.transform.position - transform.position;
        float dist = dir.magnitude;
		
		if( !found ){
			
			dir = initPos - transform.position;
			dist = dir.magnitude;
			
			if( dist <= searchRange ){
				found = true;
			}
		}
		
        dir.Normalize();

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
