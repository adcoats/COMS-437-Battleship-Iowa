using UnityEngine;
using System.Collections;

public class EnemyBattleship : MonoBehaviour {

	public GameObject shipExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy()
	{
		Instantiate(shipExplosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
