using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // input device
    public IPlayerInput input { get { return GetComponent<IPlayerInput>(); } }

    // wrapper for accessing sprite color
    public Color playerColor
    {
        get { return GetComponent<SpriteRenderer>().color; }
        set { GetComponent<SpriteRenderer>().color = value; }
    }

    public float moveSpeed = 30.0f;

    // for moving with the battleship
    // we can't just set the battleship as our parent - Unity doesn't
    // like 2 RigidBodies in a parent-child relationship
    // don't worry too much about this, it's magic
    public GameObject battleship;
    private Vector3 _parentLocalPt;
    private Vector3 _lastGlobalPt;

    private Station _currentStation;  // station we are currently manning

    // minimum distance the player can be from a station to start manning it
    private const float MIN_STATION_MANNING_DISTANCE = 10.0f;

	// Use this for initialization
	void Start ()
    {
        //input = gameObject.AddComponent<MKBInput>();
        //input = gameObject.AddComponent<GamepadInput>();

        _lastGlobalPt = transform.position;
        _parentLocalPt = battleship.transform.InverseTransformPoint(transform.position);
	}
	
    void Update()
    {
        // are we currently manning a station?
        if (_currentStation == null)
        {
            // trying to man a station?
            if (input.PressingToggleStation())
            {
                // any nearby?
                Station station = FindNearestStation();
                if (station != null)
                {
                    // yep, let's man it
                    _currentStation = station;
                    _currentStation.StartManning(this);

                    // make sure we stop moving
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
        }
        else
        {
            // manning a station; trying to leave?
            if (input.PressingToggleStation())
            {
                _currentStation.StopManning();
                _currentStation = null;
            }
        }
    }

	void FixedUpdate ()
    {
        // follow battleship by tracking how a point in the battleship's "local space"
        // has changed since last frame; adapted from here:
        // http://answers.unity3d.com/questions/8207/charactercontroller-falls-through-or-slips-off-mov.html
        // this mess is necessary because Unity doesn't like having 2 rigidbodies in
        // a parent-child relationship (the child won't follow the parent)
        Vector3 newWorldPt = battleship.transform.TransformPoint(_parentLocalPt);
        transform.position += (newWorldPt - _lastGlobalPt);  // move like the battleship moved last frame
        _lastGlobalPt = transform.position;
        _parentLocalPt = battleship.transform.InverseTransformPoint(transform.position);

        if (_currentStation == null)
        {
            // not manning a station, move around the ship
            GetComponent<Rigidbody2D>().velocity = Camera.main.transform.rotation * input.GetMove() * Time.deltaTime * moveSpeed;
        }
	}

    private Station FindNearestStation()
    {
        // this is slow - we *should* cache this since stations don't change during play,
        // but this is only called when someone pressed the "toggle manning station" button, which is pretty rare
        Station[] stations = FindObjectsOfType<Station>();

        float nearestDistance = MIN_STATION_MANNING_DISTANCE;
        Station nearestStation = null;

        foreach (Station station in stations)
        {
            float dist = Vector3.Distance(station.transform.position, transform.position);
            if (dist < nearestDistance && !station.IsManned())
            {
                nearestDistance = dist;
                nearestStation = station;
            }
        }

        return nearestStation;
    }
}
