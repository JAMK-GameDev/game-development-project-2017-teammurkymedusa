using UnityEngine;

namespace Assets.Scripts
{
    public class MusicManager : MonoBehaviour
    {
        public AudioSource[] Musics;

        private AudioSource current;
        void Start()
        {
            int startTrack = Random.Range(0, Musics.Length);
            Musics[startTrack].Play();
            current = Musics[startTrack];
        }

        void Update()
        {
            if (!current.isPlaying)
            {
                int newTrack = Random.Range(0, Musics.Length);
                Musics[newTrack].Play();
                current = Musics[newTrack];
            }
        }
    }
}