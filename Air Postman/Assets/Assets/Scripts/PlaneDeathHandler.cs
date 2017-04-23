using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlaneDeathHandler : MonoBehaviour
    {
        public GameObject[] Explosions;
        void Start()
        {

        }

        public void BlownUpPlane()
        {
            StartCoroutine(PlaneExplosions());
        }
        IEnumerator PlaneExplosions()
        {
            int i = 0;
            float[] times = new float[3] {0.2f,0.1f,0.3f};
            while (i < 3)
            {
                GameObject currBoom = Explosions[i];
                currBoom.GetComponentInChildren<ParticleSystem>().Play();
                yield return new WaitForSeconds(times[i]);
                i++;
            }
            GameManager.instance.ActivateDeath(true);


            // Code to execute after the delay
        }
    }
}