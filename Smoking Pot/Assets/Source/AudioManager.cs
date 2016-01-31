using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private static AudioManager _instance;

	public AudioSource backgroundMusic;
	public AudioSource backgroundAmbience;
	public AudioSource potAudio;
	public AudioSource gameAudio;
	public AudioSource uiAudio;
    public AudioSource victoryAudio;
    public AudioSource mediumVictoryAudio;
    public AudioSource defeatAudio;

	public AudioClip music;
	public AudioClip drumming;
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
		backgroundMusic.Stop ();
		backgroundMusic.clip = music;
		backgroundMusic.Play ();
		backgroundAmbience.Stop ();
	}

	public void playGameMusic() {
		Debug.Log ("Play game music");
		backgroundMusic.Stop ();
		backgroundMusic.clip = drumming;
		backgroundMusic.Play ();
		backgroundAmbience.Stop ();
		backgroundAmbience.clip = backgroundSounds;
		backgroundAmbience.Play ();
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

    public void playVictorySound()
    {
		backgroundAmbience.Stop ();
		backgroundMusic.Stop ();
        victoryAudio.Play();
    }

    public void playMediumVictorySound()
    {
		backgroundAmbience.Stop ();
		backgroundMusic.Stop ();
        mediumVictoryAudio.Play();
    }

    public void playDefeatSound()
    {
		backgroundAmbience.Stop ();
		backgroundMusic.Stop ();
        defeatAudio.Play();
    }
}
