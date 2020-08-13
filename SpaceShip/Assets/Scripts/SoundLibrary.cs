using System;
using System.Linq;
using UnityEngine;

namespace SpaceShip
{
    [CreateAssetMenu(menuName ="Sound Library")]
    [Serializable]
    public class SoundLibrary : ScriptableObject
    {
        public Sounds[] sounds;

        public AudioClip GetAudioClip(string name)
        {
            var sound = sounds.FirstOrDefault(s => s.name == name);
            return sound?.clip;
        }
    }

    [Serializable]
    public class Sounds
    {
        public string name;
        public AudioClip clip;
    }
}