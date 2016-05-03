using UnityEngine;
using System.Collections;
using System;

public class GamepadInput : MonoBehaviour, IPlayerInput {
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
        return Input.GetAxis("Joystick 1 Fire") > 0;
    }

    public bool PressingToggleStation()
    {
        return Input.GetButtonDown("Joystick 1 Toggle Station");
    }

    public Vector2 GetSteering()
    {
        return new Vector2(leftThumbstick().x, Input.GetAxis("Joystick 1 Fire"));
    }

    public bool PreferAbsolute()
    {
        return false;
    }

    private Vector2 rightThumbstick()
    {
        return new Vector2(Input.GetAxis("Joystick 1 Aim Horizontal"), Input.GetAxis("Joystick 1 Aim Vertical"));
    }

    private Vector2 leftThumbstick()
    {
        return new Vector2(Input.GetAxis("Joystick 1 Move Horizontal"), Input.GetAxis("Joystick 1 Move Vertical"));
    }
}
