using UnityEngine;
using System.Collections;

public class SoundFXManager : MonoBehaviour {

	public void PlayClip(AudioClip clip)
	{
		audio.Stop ();
		audio.clip = clip;
		audio.Play ();
	}
}
