using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	public AudioSource backgroundAudio;
	public AudioSource potAudio;
	public AudioSource gameAudio;
	public AudioSource uiAudio;

	public AudioClip music;
	public AudioClip backgroundSounds;
	public AudioClip splashSound;
	public AudioClip pushSound;
	public AudioClip clickSound;

	// Use this for initialization
	private void Awake () {
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

	public void playPushSound() {
		gameAudio.clip = pushSound;
		gameAudio.Play ();
	}

	public void playSplashSound() {
		potAudio.Play ();
	}

	public void playClickSound() {
		uiAudio.Stop ();
		uiAudio.clip = clickSound;
		uiAudio.Play ();
	}

}
