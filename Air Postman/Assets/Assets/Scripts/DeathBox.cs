using UnityEngine;

namespace Assets.Scripts
{
    public class DeathBox : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                //Destroy(other.gameObject);
                GameManager.instance.ActivatePlaneBoom();
            }
        }
    }
}