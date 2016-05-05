using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {

	public GameObject deathExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroyed()
	{
		if (deathExplosion)
			Instantiate(deathExplosion, transform.position, transform.rotation);
        
        GameObject.Find("Game Manager").GetComponent<GameManager>().TriggerGameOver();

        Destroy(gameObject);
	}
}
