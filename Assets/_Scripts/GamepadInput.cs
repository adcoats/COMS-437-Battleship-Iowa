using UnityEngine;
using System.Collections;
using System;

public class GamepadInput : MonoBehaviour, IPlayerInput {

	public int gamePadId;

    public Vector2 GetAimAbsolute()
    {
        return Vector2.zero;
    }

    public Vector2 GetAimDelta()
    {
        return rightThumbstick();
    }

    public Vector2 GetMove()
    {
        return leftThumbstick();
    }

    public bool PressingFire()
    {
		return Input.GetAxis("Joystick " + gamePadId + " Fire") > 0;
    }

    public bool PressingToggleStation()
    {
		if (Input.GetButtonDown("Joystick " + gamePadId + " Toggle Station"))
		{
			print("pressing station");
		}
		return Input.GetButtonDown("Joystick " + gamePadId + " Toggle Station");
    }

    public Vector2 GetSteering()
    {
		return new Vector2(leftThumbstick().x, Input.GetAxis("Joystick " + gamePadId + " Fire"));
    }

    public bool PreferAbsolute()
    {
        return false;
    }

    private Vector2 rightThumbstick()
    {
		return new Vector2(Input.GetAxis("Joystick " + gamePadId + " Aim Horizontal"), Input.GetAxis("Joystick 1" + gamePadId + " Aim Vertical"));
    }

    private Vector2 leftThumbstick()
    {
		print("pressing station" + Input.GetAxis("Joystick " + gamePadId + " Move Horizontal"));
		return new Vector2(Input.GetAxis("Joystick " + gamePadId + " Move Horizontal"), Input.GetAxis("Joystick " + gamePadId + " Move Vertical"));
    }
}
