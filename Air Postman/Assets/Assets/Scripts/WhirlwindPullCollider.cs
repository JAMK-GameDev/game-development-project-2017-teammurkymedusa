using UnityEngine;

namespace Assets.Scripts
{
    public class WhirlwindPullCollider : MonoBehaviour
    {
        public Whirlwind Parent;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                Parent.PullPlane();
            }
        }
    }
}