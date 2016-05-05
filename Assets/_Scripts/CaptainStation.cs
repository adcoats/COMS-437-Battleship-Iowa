using UnityEngine;
using System.Collections;
using System;

public class CaptainStation : Station
{
    public GameObject battleship;
    public GameObject playerSeat;

    public float forwardSpeed = 15.0f;
    public float turnSpeed = 50.0f;

    public float zoomAmount = 45.0f;
    private float _origCameraSize;
    private float _currentZoom;

    // Use this for initialization
    void Start ()
    {
        _origCameraSize = Camera.main.orthographicSize;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentPlayer == null)
            return;

        Rigidbody2D body = battleship.GetComponent<Rigidbody2D>();

        Vector2 forward = battleship.transform.rotation * Vector2.up * currentPlayer.input.GetSteering().y;
        body.AddForce(forward * forwardSpeed);

        // move point of steering torque further back the faster we go - turn better at higher speeds
        // i made this up based on one of the formulas available here:
        // https://gamedev.stackexchange.com/questions/92747/2d-boat-controlling-physics
        // Vector2 side = battleship.transform.rotation * Vector2.right * -currentPlayer.input.GetMove().x;
		//body.AddTorque(body.velocity.sqrMagnitude * -currentPlayer.input.GetSteering().x * turnSpeed);
		body.AddTorque(-currentPlayer.input.GetSteering().x * turnSpeed);
        // make sure the player is in their seat
        currentPlayer.transform.position = playerSeat.transform.position;

        // zoom stuff
        if (currentPlayer.input.PressingZoomOut())
        {
            _currentZoom += Time.deltaTime * 2.0f;
        } else {
            _currentZoom -= Time.deltaTime * 2.0f;
        }
        _currentZoom = Mathf.Clamp(_currentZoom, 0f, 1f);
        
        Camera.main.orthographicSize = _origCameraSize + _currentZoom *_currentZoom * zoomAmount;
	}

    protected override void OnStartManning(Player player)
    {
        
    }

    protected override void OnStopManning()
    {
        
    }
}