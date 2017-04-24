using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Death : MonoBehaviour
    {
		public Canvas DeathUI;
		public Text DeathText;

		private void Start()
		{
			DeathUI.gameObject.SetActive(false);
		}

		public void ActivateDeath() {
			DeathUI.gameObject.SetActive(true);
			//BackButton.gameObject.SetActive(true);
			DeathText.GetComponent<Animator>().SetTrigger("Death");
		}
    }
}