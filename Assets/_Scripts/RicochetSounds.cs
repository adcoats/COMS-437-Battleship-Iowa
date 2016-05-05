using UnityEngine;
using System.Collections;

public class RicochetSounds : MonoBehaviour {

	public AudioSource[] damageSoundSources;

	// Use this for initialization
	void Start () {
		//damageSoundSources = GetComponents<AudioSource> ();
	}


	void OnDamaged(float amount)
	{
		int i = Random.Range (0, damageSoundSources.Length);

		// play sound
		if (damageSoundSources != null) 
		{

			if (amount > damageSoundSources.Length) // random number
			{ 
				damageSoundSources [i].pitch = Random.Range (0.3f, 0.4f);
			} else 
			{
				damageSoundSources [i].pitch = Random.Range (0.6f, 0.7f);
			}


			damageSoundSources [i].PlayOneShot (damageSoundSources [i].clip);
		}
	}
}
