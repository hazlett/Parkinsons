using UnityEngine;
using System.Collections;

public class SoundFXManager : MonoBehaviour {

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
	}
	public void PlayClip(AudioClip clip)
	{
		audio.Stop ();
		audio.clip = clip;
		audio.Play ();
	}
}
