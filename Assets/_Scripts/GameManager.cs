using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	int numControllerPlayers;
	public GameObject controllerPlayerPrefab;

	void Awake ()
	{
		numControllerPlayers = Input.GetJoystickNames ().Length;

		for(var i = 0; i < numControllerPlayers; i++)
		{
			GameObject playerRef = Instantiate (controllerPlayerPrefab);
			GamepadInput component = playerRef.GetComponent<GamepadInput> ();
			component.gamePadId = i + 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
