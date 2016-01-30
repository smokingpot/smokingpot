using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	private AudioSource backgroundAudio;
	public AudioSource potAudio;

	public AudioClip music;
	public AudioClip backgroundSounds;
	public AudioClip splashSound;

	// Use this for initialization
	private void Awake () {
		backgroundAudio = GetComponent<AudioSource> ();
		_instance = this;
	}

	public static AudioManager Instance {
		get {
			return _instance;
		}
	}

	public void playMenuMusic() {
		backgroundAudio.Stop ();
		backgroundAudio.clip = music;
		backgroundAudio.Play ();
	}

	public void playGameMusic() {
		backgroundAudio.Stop ();
		backgroundAudio.clip = backgroundSounds;
		backgroundAudio.Play ();
	}

	public void playSplashSound() {
		potAudio.Play ();
	}


}
