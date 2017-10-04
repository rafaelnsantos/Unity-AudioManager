using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioSource musicSource, effectSource;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    [HideInInspector]
    public bool musicOn, effectOn;

    public static AudioManager instance = null;

	void Awake () {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    private void Start() {
        musicOn = true;
        effectOn = true;
    }

    public void PlayEffect(AudioClip clip, float duracao = 1f, float volume = 1f, float pitch = 1f) {
        if (!effectOn) return;
        effectSource.volume = volume;
        effectSource.pitch = pitch;
        effectSource.PlayOneShot(clip, duracao);
        
    }

    public void PlayRandomEffect(AudioClip[] clips, float duracao = 1f, float volume = 1f) {
        int random = UnityEngine.Random.Range(0, clips.Length);
        float randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);

        PlayEffect(clips[random], duracao, volume, randomPitch);
    }

    public void PlayMusic(AudioClip clip) {
        if (!musicOn) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void switchMusic() {
        if (musicOn) {
            musicSource.Stop();
        } else {
            musicSource.Play();
        }
        musicOn = !musicOn;
    }

    public void switchEffect() {
        effectOn = !effectOn;
    }
}
