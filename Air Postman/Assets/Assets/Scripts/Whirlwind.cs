using UnityEngine;

namespace Assets.Scripts
{
    public class Whirlwind : MonoBehaviour
    {
        private float PullRadius;
        private float PullForce;
        private float Size;
        private float Hitarea;
        public float Speed;
        public GameObject Plane;

        public void SetParameters(float pullForce, float hitArea, float speed, float pullRadius = 0.0f)
        {
            PullRadius = pullRadius;
            PullForce = pullForce;
            Speed = speed;
        }

        private void Start()
        {
            if (PullRadius == 0.0f)
            {
                PullRadius = Size;
            }
        }

        void Update()
        {
            Vector3 oldPos = transform.position;
            oldPos.x -= Speed * Time.deltaTime;
            transform.position = oldPos;
        }

        public void PullPlane()
        {
            Rigidbody2D PlaneBody = this.Plane.GetComponent<Rigidbody2D>();
            Vector3 forceDirection = transform.position - PlaneBody.transform.position;
            Vector2 forceDirectionFixed = new Vector2(forceDirection.x, forceDirection.y);
            PlaneBody.AddForce(forceDirectionFixed * PullForce);
        }

        public void Death()
        {
            //Debug.Log("Collision with Whirlwind!");
            //gameObject.SetActive(false);
            //Destroy(Plane);
            GameManager.instance.ActivatePlaneBoom();
        }
        public void SetSize(float Size)
        {
            var currSize = GetComponent<SpriteRenderer>().sprite.rect.size;
            float scale = Size / currSize.x;
            transform.localScale = new Vector3(scale, scale, 1f);

        }

        public void SetCollider()
        {
            var currSize = GetComponentInChildren<CircleCollider2D>().radius;
            PlaneControls pc = Plane.GetComponent<PlaneControls>();
            float new_size = currSize * (1 / pc.Durability);
            gameObject.GetComponentInChildren<CircleCollider2D>().radius = new_size;
        }
    }
}