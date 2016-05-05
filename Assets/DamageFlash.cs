using UnityEngine;
using System.Collections;

public class DamageFlash : MonoBehaviour {

	float damageTaken;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		damageTaken = Mathf.Clamp (damageTaken, 0, 5);
		renderer.color = Color.Lerp (Color.white, Color.red, damageTaken / 5);
		damageTaken -= 10 * Time.deltaTime;
	}
}
