using UnityEngine;
using System.Collections;

public class RicochetSounds : MonoBehaviour {

	public AudioSource[] damageSoundSources;
	public float volume = 1.0f;

	// Use this for initialization
	void Start () {
		// clamp volume to a valid value
		Mathf.Clamp (volume, 0, 1);
//
//		for (int i = 0; i < damageSoundSources.Length; i++) 
//		{
//			damageSoundSources [i].volume = volume;
//		}
	}


	void OnDamaged(float amount)
	{
		int i = Random.Range (0, damageSoundSources.Length);

		// play sound
		if (damageSoundSources != null) 
		{

			if (amount > 3) // random number
			{ 
				damageSoundSources [i].pitch = Random.Range (0.3f, 0.4f);
				damageSoundSources [i].volume = volume;
			} else 
			{
				damageSoundSources [i].pitch = Random.Range (0.6f, 0.7f);
				damageSoundSources [i].volume = volume * 0.3f;
			}


			damageSoundSources [i].PlayOneShot (damageSoundSources [i].clip);
		}
	}
}
