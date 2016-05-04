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
		if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
			return Input.GetAxis("Joystick " + gamePadId + " Fire") < 0;
		else
			return Input.GetAxis("Joystick " + gamePadId + " Fire") > 0;
    }

    public bool PressingToggleStation()
    {
		return Input.GetButtonDown("Joystick " + gamePadId + " Toggle Station");
    }

    public Vector2 GetSteering()
    {
		print (Application.platform);
		if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
			return new Vector2(leftThumbstick().x, leftThumbstick().y);
		else
			return new Vector2(leftThumbstick().x, Input.GetAxis("Joystick " + gamePadId + " Fire"));
    }

    public bool PreferAbsolute()
    {
        return false;
    }

    private Vector2 rightThumbstick()
    {
		return new Vector2(Input.GetAxis("Joystick " + gamePadId + " Aim Horizontal"), Input.GetAxis("Joystick " + gamePadId + " Aim Vertical"));
    }

    private Vector2 leftThumbstick()
    {
		return new Vector2(Input.GetAxis("Joystick " + gamePadId + " Move Horizontal"), Input.GetAxis("Joystick " + gamePadId + " Move Vertical"));
    }
}
