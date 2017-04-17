using UnityEngine;

namespace Assets.Scripts
{
    public class WhirlwindColliders : MonoBehaviour
    {
        public Whirlwind Parent;

        public enum PullOrColl
        {
            PullCollider, HitCollider
        }
        public PullOrColl state;
        private void Start()
        {

        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                if (state == PullOrColl.PullCollider)
                {
                    Parent.PullPlane();
                }
                else
                {
                    Parent.Death();
                }

            }
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag.Equals("Player"))
            {
                if (state == PullOrColl.PullCollider)
                {
                    Parent.PullPlane();
                }
                else
                {
                    Parent.Death();
                }

            }
        }
    }
}