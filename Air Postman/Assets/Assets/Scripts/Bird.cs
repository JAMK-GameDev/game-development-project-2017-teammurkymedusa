using UnityEngine;

namespace Assets.Scripts
{
    public class Bird: MonoBehaviour
    {
        float speed;

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
            Debug.Log("Collision with bird!");
            if (col.gameObject.tag.Equals("Player"))
            {
                Destroy(gameObject);
                Destroy(col.gameObject);
            }
            GameManager.instance.ActivateDeath();
        }
    }
}