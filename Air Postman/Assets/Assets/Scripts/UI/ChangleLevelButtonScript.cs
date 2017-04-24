using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ChangleLevelButtonScript : MonoBehaviour
    {
        public Button ButtonSprite;

        public string level;
        private bool _levelValid = false;

        private void Start()
        {
            ButtonSprite.GetComponent<Button>().onClick.AddListener(ActivateBackToMenu);
        }

        public void ActivateBackToMenu()
        {
			// We don't want the music to play in main menu
			if (level == "MainMenu") {
				Destroy (GameObject.Find ("SoundHolder"));
			}
			Destroy (GameObject.Find ("GameController"));
            SceneManager.LoadScene(this.level);
        }
    }
}