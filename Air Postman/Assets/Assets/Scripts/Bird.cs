using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bird: MonoBehaviour
    {
        float speed;
        public AudioSource[] Explosions;

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
        private void Update()
        {
            Vector3 oldPos = transform.position;
            oldPos.x -= speed * Time.deltaTime;
            transform.position = oldPos;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            //Debug.Log("Collision with bird!");
            if (col.gameObject.tag.Equals("Player"))
            {
                //gameObject.SetActive(false);
                //Destroy(col.gameObject);
                int startExplosion = Random.Range(0, Explosions.Length);
                Explosions[startExplosion].Play();
                StartCoroutine(SecondBoom(1 - startExplosion));
                GameManager.instance.ActivatePlaneBoom();
            }
        }

        IEnumerator SecondBoom(int other)
        {
            yield return new WaitForSeconds(.15f);
            Explosions[other].Play();
            Destroy(gameObject);
        }
    }
}