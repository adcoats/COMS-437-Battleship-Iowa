﻿using UnityEngine;
using System.Collections;

public class SimpleShoot : MonoBehaviour 
{
	// +/- how far this station can aim, in degrees
	public float turnRadius = 50;

	// maximum number of shots that can be fired in a single burst
	public int maxBurst = 10;
	// how many shots have been fired in this burst
	private int shotsFired = 0;

	// delay in seconds between bursts
	public float reload = 3f;
	// timer for reload time
	private float timer;

	// player ship
	private GameObject target;

	// max distance the target can be before we start shooting
	public float maxFireDistance = 10.0f;

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

	private Quaternion _initialRotation;
	// _neutralRotation is used as the "center" when calculating rotation from _currentAim
	//private Quaternion _neutralRotation { get { return transform.parent.rotation * _initialRotation; } }
	private Quaternion _neutralRotation { get {
			return (transform.parent == null) ? _initialRotation : transform.parent.rotation * _initialRotation;
		} }
	private float _currentAim;  // current rotation around Z axis from _neutralRotation
	private AudioSource fireSoundSource;

	// Use this for initialization
	void Start () 
	{
		_initialRotation = transform.localRotation;
		timer = reload;
		if (target == null)
			target = GameObject.Find("Battleship");
		fireSoundSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null || (target.transform.position - transform.position).magnitude > maxFireDistance)
			return;

		// look at player ship
		Vector2 neutralUpVector = Camera.main.transform.rotation * _neutralRotation * Vector2.up;
		Vector2 aimVector = target.transform.position - transform.position;
		//Vector2 aimVector = transform.position - target.transform.position;
		_currentAim = Vector2.Angle(aimVector, neutralUpVector);

		// fix the sign because Vector2.Angle apparently doesn't give one (UGH)
		Vector2 aimFlipped = new Vector2(-aimVector.y, aimVector.x);
		_currentAim *= (Vector2.Dot(aimFlipped, neutralUpVector) > 0) ? -1 : 1;

		// limit _currentAim to our turning radius
		_currentAim = Mathf.Clamp(_currentAim, -turnRadius, turnRadius);

		// Apply _currentAim to our actual rotation
		//transform.rotation = _neutralRotation * Quaternion.AngleAxis(_currentAim, new Vector3(0, 0, 1));
        
		if ((target.transform.position - transform.position).magnitude > maxFireDistance)
			return;

		// fire if we're pressing the button and we've waited long enough for reloadTime seconds
		//Util.VectorAngleWithSign
		if (shotsFired < maxBurst && (Time.time - _lastShotTime) >= reloadTime && Vector2.Angle(aimVector, transform.rotation * Vector2.up) < turnRadius) {
			GameObject proj = Instantiate (projectilePrefab);

			// muzzle point we will fire from (where to spawn the
			// projectile, and with what rotation)
			GameObject mp = muzzlePoints [_nextMuzzlePoint];
			proj.transform.position = mp.transform.position;
			proj.transform.rotation = mp.transform.rotation;

            float angle = Vector2.Angle(Vector2.up, aimVector.normalized);

            if (aimVector.x > 0)
                angle *= -1;

            proj.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));


			// set initial velocity
			//aimVector = transform.rotation * Vector2.up;
			//aimVector = directionQ * Vector2.up;
			proj.GetComponent<Rigidbody2D> ().velocity = aimVector.normalized * projectileSpeed;

			// fire from the next muzzle point next time
			_nextMuzzlePoint = (_nextMuzzlePoint + 1) % muzzlePoints.Length;

			_lastShotTime = Time.time;
			shotsFired++;
			if (fireSoundSource != null) {

				// Play sound if it isn't looped
				if (!fireSoundSource.loop)
					fireSoundSource.Play ();

				// Play looped sound
				if (!fireSoundSource.isPlaying && fireSoundSource.loop)
					fireSoundSource.Play ();
			}
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
