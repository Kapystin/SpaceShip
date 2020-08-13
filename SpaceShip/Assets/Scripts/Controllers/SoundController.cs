using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundController : MasterSingleton<SoundController>
    {
        [SerializeField]
        protected AudioSource _audioData;

        public void PlayAudioClip(string clip, float volume = 0.7f)
        {
            var audioClip = Settings.SoundLibrary.GetAudioClip(clip);
            _audioData.pitch = Random.Range(0.7f, 1f);
            _audioData.volume = volume;
            _audioData.PlayOneShot(audioClip); 
        }

    }

}