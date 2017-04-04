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
    }
}