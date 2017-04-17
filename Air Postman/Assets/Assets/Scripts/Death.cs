using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Death : MonoBehaviour
    {
        public Text DeathText;

        private void Start()
        {
            DeathText.gameObject.SetActive(false);
        }

        public void ActivateDeath() {
            DeathText.gameObject.SetActive(true);
            DeathText.GetComponent<Animator>().SetTrigger("Death");
        }
    }
}