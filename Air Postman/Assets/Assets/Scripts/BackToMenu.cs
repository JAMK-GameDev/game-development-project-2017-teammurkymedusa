using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BackToMenu : MonoBehaviour
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
            SceneManager.LoadScene(this.level);
        }
    }
}