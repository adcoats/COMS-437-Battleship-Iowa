using UnityEngine;
using System.Collections;

public class DamageFlash : MonoBehaviour {

	public float maxDamageTaken = 5;
	public float decayFactor = 10;

	float damageTaken;
	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		damageTaken = 0;
	}
	
	// Update is called once per frame
	void Update () {
		damageTaken = Mathf.Clamp (damageTaken, 0, maxDamageTaken);
		renderer.color = Color.Lerp (Color.white, Color.red, damageTaken / maxDamageTaken);
		damageTaken -= decayFactor * Time.deltaTime;
	}

	void OnDamaged(float amount)
	{
		damageTaken += amount;
	}
}
