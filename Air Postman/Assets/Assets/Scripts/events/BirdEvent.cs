using UnityEngine;

namespace Assets.Scripts.events

{
    public class BirdEvent: MonoBehaviour
    {
        public GameObject BirdBaseObject;
        public Camera camera;

        private float Speed;
        private float Height;
        private int BirdType;

        public void SetParameters(Camera camera, float speed, float height, int birdType)
        {
            this.camera = camera;
            Speed = speed;
            Height = height;
            BirdType = birdType;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                SpawnBird();
            }

        }

        private void SpawnBird()
        {
            Debug.Log("Bird!!");
            //let's calculate the position for a bird
            float CamHeight = camera.orthographicSize *2f;
            float CamWidth = CamHeight * camera.aspect;
            float BirdStart = camera.transform.position.x + (CamWidth / 2);
            Vector3 birdPos = new Vector3(BirdStart, Height, 0f);
            GameObject Bird = Instantiate(BirdBaseObject, birdPos, Quaternion.identity);
            Bird.GetComponent<Bird>().SetSpeed(Speed);
        }
    }
}