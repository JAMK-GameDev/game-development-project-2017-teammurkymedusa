using UnityEngine;

namespace Assets.Scripts
{
    public class DeathBox : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                Destroy(other.gameObject);
                GameManager.instance.ActivateDeath();
            }
        }
    }
}