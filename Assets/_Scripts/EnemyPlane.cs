using UnityEngine;
using System.Collections;

public class EnemyPlane : MonoBehaviour {

	public GameObject planeExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroyed()
    {
        // rekt
		Instantiate(planeExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
