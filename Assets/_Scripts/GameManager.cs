using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject canvas;
    public GameObject winImage;
    public GameObject loseImage;

	int numControllerPlayers;
	public GameObject controllerPlayerPrefab;
	static readonly Color[] colors = new Color[]{Color.yellow, Color.magenta, Color.green, Color.cyan};

    private GameObject[] _enemies;

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

        UpdateEnemyList();
	}

    void FixedUpdate()
    {
        if (CheckForWin())
            GameWon();
    }

    public void TriggerGameOver()
    {
        GameOver();
    }

    private void GameOver()
    {
        canvas.SetActive(true);
        loseImage.SetActive(true);
        gameObject.SetActive(false);
        Invoke("GoToMenu", 6.0f);
    }

    private void GameWon()
    {
        canvas.SetActive(true);
        winImage.SetActive(true);
        gameObject.SetActive(false);
        Invoke("GoToMenu", 6.0f);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private bool CheckForWin()
    {
        // unity sets GameObject references to a special wrapper where
        // obj == null returns true after the object has been destroyed
        foreach (GameObject obj in _enemies)
        {
            if (obj != null)
                return false;
        }

        return true;
    }

    private void UpdateEnemyList()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
