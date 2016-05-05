using UnityEngine;
using System.Collections;
using System;

public class MKBInput : MonoBehaviour, IPlayerInput
{
    // how much did the mouse move this frame?
    public Vector2 GetAimDelta()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    // where is the mouse now?
    public Vector2 GetAimAbsolute()
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    // state of WASD/arrow keys?
    public Vector2 GetMove()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    // left mouse button?
    public bool PressingFire()
    {
        // switch to GetMouseButton() for auto fire
        //return Input.GetMouseButtonDown(0);
		return Input.GetMouseButton(0);
    }

    // E key?
    public bool PressingToggleStation()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public Vector2 GetSteering()
    {
        // same as movement
        return GetMove();
    }

    // prefer using mouse position instead of mouse movements where applicable
    // (i.e. turret aims at mouse cursor)
    public bool PreferAbsolute()
    {
        return true;
    }

    public bool PressingZoomOut()
    {
        return Input.GetKey(KeyCode.Q);
    }
}
