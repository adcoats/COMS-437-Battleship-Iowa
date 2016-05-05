using UnityEngine;

/**
 * This interface represents an input device, like a Mouse + KB or GamePad.
 */
public interface IPlayerInput {
    Vector2 GetMove();  // WASD/arrow keys/left thumbstick
    Vector2 GetAimDelta();  // mouse movement this frame/right thumbstick
    Vector2 GetAimAbsolute();  // absolute mouse position in screen coordinates, (0, 0) for gamepad

    bool PressingFire();
    bool PressingToggleStation();

    Vector2 GetSteering();  // ship throttle + turning when piloting ship
    bool PressingZoomOut();

    // Should turrets try to aim at the mouse cursor?
    // Prefer using our cursor position (GetAimAbsolute()) instead of aiming based on mouse position deltas (GetAimDelta())?
    // (returns true for mouse, false for gamepad)
    bool PreferAbsolute();
}
