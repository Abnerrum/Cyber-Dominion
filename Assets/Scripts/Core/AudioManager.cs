using UnityEngine;

namespace CyberDominion
{
    public sealed class AudioManager : MonoBehaviour
    {
        private AudioSource source;

        private void Awake()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.volume = 0.18f;
        }

        public void PlayTone(float frequency, float duration)
        {
            const int sampleRate = 44100;
            int sampleCount = Mathf.CeilToInt(sampleRate * duration);
            float[] samples = new float[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                float envelope = 1f - (float)i / sampleCount;
                samples[i] = Mathf.Sin(2f * Mathf.PI * frequency * i / sampleRate) * envelope;
            }

            AudioClip clip = AudioClip.Create("CyberTone", sampleCount, 1, sampleRate, false);
            clip.SetData(samples, 0);
            source.PlayOneShot(clip);
        }
    }
}
