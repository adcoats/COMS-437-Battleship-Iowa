using UnityEngine;
using System.Collections;
using System;

public class GunStation : Station
{
    // +/- how far this station can aim, in degrees
    public float turnRadius = 30;

    // minimum time between shots, in seconds
    public float reloadTime = 0.1f;

    // initial speed of fired projectile
    public float projectileSpeed = 20.0f;

    // where we move the player to
    public GameObject playerSeat;

    // where we shoot from
    public GameObject[] muzzlePoints;

    // projectile to spawn when we press fire
    public GameObject projectilePrefab;

    private Quaternion _initialRotation;
    // _neutralRotation is used as the "center" when calculating rotation from _currentAim
    private Quaternion _neutralRotation { get { return transform.parent.rotation * _initialRotation; } }
    private float _currentAim;  // current rotation around Z axis from _neutralRotation
    private float _lastShotTime;  // for making sure we wait at least reloadTime seconds between shots
    private int _nextMuzzlePoint;  // index of last muzzle point, so we cycle with each shot

    // Use this for initialization
    void Start ()
    {
        _initialRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if no one is manning this station, bail
        if (currentPlayer == null)
            return;

        // update aim
		if (currentPlayer.input.PreferAbsolute())
        {
            // mouse
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 neutralUpVector = Camera.main.transform.rotation * _neutralRotation * Vector2.up;
            Vector2 aimVector = currentPlayer.input.GetAimAbsolute() - new Vector2(screenPos.x, screenPos.y);
            _currentAim = Vector2.Angle(aimVector, neutralUpVector);

            // fix the sign because Vector2.Angle apparently doesn't give one (UGH)
            Vector2 aimFlipped = new Vector2(-aimVector.y, aimVector.x);
            _currentAim *= (Vector2.Dot(aimFlipped, neutralUpVector) > 0) ? -1 : 1;
        } else {
            // gamepad
            Vector2 aimVector = currentPlayer.input.GetAimDelta();

            // if not using right stick, use left stick
            if (aimVector.magnitude < 0.2f)
                aimVector = currentPlayer.input.GetMove();

            if (aimVector.magnitude >= 0.2f)
            {
                Vector2 neutralUpVector = Camera.main.transform.rotation * _neutralRotation * Vector2.up;
                _currentAim = Vector2.Angle(aimVector, neutralUpVector);

                // fix the sign because Vector2.Angle apparently doesn't give one (UGH)
                Vector2 aimFlipped = new Vector2(-aimVector.y, aimVector.x);
                _currentAim *= (Vector2.Dot(aimFlipped, neutralUpVector) > 0) ? -1 : 1;
            }

            /*
            // aim by turning left/right
            if (aimDelta != Vector2.zero)
            {
                Vector2 aimFlipped = new Vector2(-aimDelta.y, aimDelta.x);
                float sign = Vector2.Dot(Camera.main.transform.rotation *_neutralRotation * Vector2.up, aimFlipped.normalized);

                // deadzone, kicks in when aim delta is mostly 
                // along neutral rotation forward axis (feels a bit better for mouse)
                if (Mathf.Abs(sign) >= 0.3f)
                {
                    sign = (sign > 0) ? -1 : 1;
                    _currentAim += Time.deltaTime * sign * 120;
                }
            }*/
        }

        // limit _currentAim to our turning radius
        _currentAim = Mathf.Clamp(_currentAim, -turnRadius, turnRadius);
        // Apply _currentAim to our actual rotation
        transform.rotation = _neutralRotation * Quaternion.AngleAxis(_currentAim, new Vector3(0, 0, 1));

        // move the player to their seat
        currentPlayer.transform.position = playerSeat.transform.position;

        // fire if we're pressing the button and we've waited long enough for reloadTime seconds
        if (currentPlayer.input.PressingFire() && (Time.time - _lastShotTime) >= reloadTime)
        {
            GameObject proj = Instantiate(projectilePrefab);
            proj.layer = 11;
            // Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), transform.parent.GetComponent<Collider2D>());

            // muzzle point we will fire from (where to spawn the
            // projectile, and with what rotation)
            GameObject mp = muzzlePoints[_nextMuzzlePoint];
            proj.transform.position = mp.transform.position;
            proj.transform.rotation = mp.transform.rotation;

            // set initial velocity
            Vector2 aimVector = transform.rotation * Vector2.up;
            proj.GetComponent<Rigidbody2D>().velocity = aimVector * projectileSpeed;

            // fire from the next muzzle point next time
            _nextMuzzlePoint = (_nextMuzzlePoint + 1) % muzzlePoints.Length;

            _lastShotTime = Time.time;
        }
    }

    protected override void OnStartManning(Player player)
    {
        // might create a crosshair sprite here later (that follows the mouse cursor)
    }

    protected override void OnStopManning()
    {
        
    }
}
