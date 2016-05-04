using UnityEngine;
using System.Collections;

public class SimpleShoot : MonoBehaviour 
{
	// maximum number of shots that can be fired in a single burst
	public int maxBurst = 10;
	// how many shots have been fired in this burst
	private int shotsFired = 0;

	// delay in seconds between bursts
	public float reload = 3f;
	// timer for reload time
	private float timer;

	// minimum time between shots, in seconds
	public float reloadTime = 0.1f;

	// initial speed of fired projectile
	public float projectileSpeed = 20.0f;

	// where we shoot from
	public GameObject[] muzzlePoints;

	// projectile to spawn when we press fire
	public GameObject projectilePrefab;

	private float _lastShotTime;  // for making sure we wait at least reloadTime seconds between shots
	private int _nextMuzzlePoint;  // index of last muzzle point, so we cycle with each shot

	// Use this for initialization
	void Start () 
	{
		timer = reload;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// fire if we're pressing the button and we've waited long enough for reloadTime seconds
		if (shotsFired < maxBurst && (Time.time - _lastShotTime) >= reloadTime) {
			GameObject proj = Instantiate (projectilePrefab);

			// muzzle point we will fire from (where to spawn the
			// projectile, and with what rotation)
			GameObject mp = muzzlePoints [_nextMuzzlePoint];
			proj.transform.position = mp.transform.position;
			proj.transform.rotation = mp.transform.rotation;

			// set initial velocity
			Vector2 aimVector = transform.rotation * Vector2.up;
			proj.GetComponent<Rigidbody2D> ().velocity = aimVector * projectileSpeed;

			// fire from the next muzzle point next time
			_nextMuzzlePoint = (_nextMuzzlePoint + 1) % muzzlePoints.Length;

			_lastShotTime = Time.time;
			shotsFired++;
		} else if (shotsFired >= maxBurst) {
			// when timer runs out, enable shooting
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else {
				shotsFired = 0;
				timer = reload;
			}
		}
	}
}
