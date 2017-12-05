using UnityEngine;

namespace AudioManager.scripts {
    public class AudioManager : MonoBehaviour {
        private AudioSource musicSource, effectSource;

        private bool musicOn, effectOn;

        public bool MusicOn {
            get { return musicOn; }
        }

        public bool EffectOn {
            get { return effectOn; }
        }

        public static AudioManager Instance = null;

        void Awake () {
            // Singleton Patter
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);

            musicSource = gameObject.GetComponents<AudioSource>()[0];
            effectSource = gameObject.GetComponents<AudioSource>()[1];
        }

        private void Start () {
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
        public void PlayEffect (AudioClip clip, float duration = 1f, float volume = 1f, float pitch = 1f) {
            if (!effectOn) return;

            effectSource.volume = volume;
            effectSource.pitch = pitch;
            effectSource.PlayOneShot(clip, duration);
        }

        /// <summary>
        /// Play a random effect.
        /// </summary>
        /// <param name="clips">Array of Clips to play.</param>
        /// <param name="duration">Duration, default 1.</param>
        /// <param name="volume">Volume, default 1.</param>
        /// <param name="lowPitchRange">Low Pitch, defalt 0.95</param>
        /// <param name="highPitchRange">High Pitch, defaltt 1.05</param>
        public void PlayRandomEffect (AudioClip[] clips, float duration = 1f, float volume = 1f,
            float lowPitchRange = 0.95f, float highPitchRange = 1.0f) {
            int random = UnityEngine.Random.Range(0, clips.Length);
            float randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);

            PlayEffect(clips[random], duration, volume, randomPitch);
        }

        /// <summary>
        /// Play a music.
        /// </summary>
        /// <param name="clip">Music to play.</param>
        public void PlayMusic (AudioClip clip) {
            if (!musicOn) return;

            musicSource.clip = clip;
            musicSource.Play();
        }

        /// <summary>
        /// Switch music on or off.
        /// </summary>
        /// <returns>Returns a boolean based on the status.</returns>
        public bool SwitchMusic () {
            musicOn = !musicOn;

            if (!musicOn) {
                musicSource.Stop();
            } else {
                musicSource.Play();
            }

            return musicOn;
        }

        /// <summary>
        /// Switch effects on or off.
        /// </summary>
        /// <returns>Returns a boolean based on the status.</returns>
        public bool SwitchEffect () {
            effectOn = !effectOn;
            return effectOn;
        }
    }
}