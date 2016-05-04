using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	int numControllerPlayers;
	public GameObject controllerPlayerPrefab;
	static Color[] colors = new Color[]{Color.yellow, Color.magenta, Color.green, Color.cyan};

	void Awake ()
	{
		numControllerPlayers = Input.GetJoystickNames ().Length;

		for(var i = 0; i < numControllerPlayers; i++)
		{
			GameObject playerRef = Instantiate (controllerPlayerPrefab);
			GamepadInput component = playerRef.GetComponent<GamepadInput> ();
			component.gamePadId = i + 1;
			SpriteRenderer renderer = playerRef.GetComponent<SpriteRenderer> ();
			renderer.color = colors[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
