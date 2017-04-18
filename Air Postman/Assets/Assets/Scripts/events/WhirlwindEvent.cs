using UnityEngine;

namespace Assets.Scripts.events
{
    public class WhirlwindEvent : MonoBehaviour
    {
        public GameObject WhirlwindBaseObject;
        public Camera camera;

        private float PullRadius;
        private float PullForce;
        private float Size;
        private float Hitarea;
        private float Speed;
        private float StartPosition;

        public void SetParameters(float StartPos, float pullForce, float hitArea, float speed, float f = 0.0f, float c = 0.0f)
        {
            PullRadius = f;
            PullForce = pullForce;
            Size = c;
            Hitarea = hitArea;
            Speed = speed;
            StartPosition = StartPos;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                SpawnWhirlwind();
            }

        }

        private void SpawnWhirlwind()
        {
            //let's calculate the position for a bird
            float CamHeight = camera.orthographicSize *2f;
            float CamWidth = CamHeight * camera.aspect;
            float WwStart = camera.transform.position.x + (CamWidth / 2);
            Vector3 WwPos = new Vector3(WwStart, StartPosition, 0f);
            GameObject Whirlwind = Instantiate(WhirlwindBaseObject, WwPos, Quaternion.identity);
            Whirlwind.GetComponent<Whirlwind>().SetParameters(
            PullForce,
            Hitarea,
            Speed,
            PullRadius
            );
            if (Size != 0.0f)
            {
               Whirlwind.GetComponent<Whirlwind>().SetSize(Size);
            }
        }
    }
}