﻿using UnityEngine;
using System.Collections;

public class EnemyFort : MonoBehaviour {

	public GameObject deathExplosion;
	public GameObject rekt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDestroyed()
	{
		if (deathExplosion) {
			Instantiate (deathExplosion, transform.position, transform.rotation);
			Instantiate (rekt, transform.position, transform.rotation);
		}

		Destroy(gameObject);
	}
}
