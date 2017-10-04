using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioSource musicSource, effectSource;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    [HideInInspector]
    public bool musicOn, effectOn;

    public static AudioManager instance = null;

    /// <summary>
    /// Singleton pattern.
    /// </summary>
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

    /// <summary>
    /// Play an effect.
    /// </summary>
    /// <param name="clip">Clip to play.</param>
    /// <param name="duration">Duration, default 1.</param>
    /// <param name="volume">Volume, default 1.</param>
    /// <param name="pitch">Pitch, default 1.</param>
    public void PlayEffect(AudioClip clip, float duracao = 1f, float volume = 1f, float pitch = 1f) {
        if (!effectOn) return;
        effectSource.volume = volume;
        effectSource.pitch = pitch;
        effectSource.PlayOneShot(clip, duracao);
    }

    /// <summary>
    /// Play a random effect.
    /// </summary>
    /// <param name="clips">Array of Clips to play.</param>
    /// <param name="duration">Duration, default 1.</param>
    /// <param name="volume">Volume, default 1.</param>
    public void PlayRandomEffect(AudioClip[] clips, float duracao = 1f, float volume = 1f) {
        int random = UnityEngine.Random.Range(0, clips.Length);
        float randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);

        PlayEffect(clips[random], duracao, volume, randomPitch);
    }

    /// <summary>
    /// Play a random effect.
    /// </summary>
    /// <param name="clips">Array of Clips to play.</param>
    /// <param name="duration">Duration, default 1.</param>
    /// <param name="volume">Volume, default 1.</param>
    public void PlayMusic(AudioClip clip) {
        if (!musicOn) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    /// <summary>
    /// Switch music on or off.
    /// </summary>
    /// <returns>Returns a boolean based on the status.</returns>
    public bool switchMusic() {
        if (musicOn) {
            musicSource.Stop();
        } else {
            musicSource.Play();
        }
        musicOn = !musicOn;
        return musicOn;
    }

    /// <summary>
    /// Switch effects on or off.
    /// </summary>
    /// <returns>Returns a boolean based on the status.</returns>
    public bool switchEffect() {
        effectOn = !effectOn;
        return effectOn;
    }
}
