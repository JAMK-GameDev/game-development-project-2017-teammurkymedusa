using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayButton : MonoBehaviour
    {
        public Button PlaySprite;

        private string level;
        private bool _levelValid = false;

        private void Start()
        {
            PlaySprite.GetComponent<Button>().onClick.AddListener(StartGame);
        }

        public void SetLevelName(string name)
        {
            Scene level = SceneManager.GetSceneByName(name);
            //if (level.IsValid())
            //{
                this.level = name;
                _levelValid = true;
            //}
            //else
            //{
                //Debug.LogWarning("Invalid Scene name given, name was " + name);
                //_levelValid = false;
            //}
        }

        void StartGame()
        {
            SceneManager.LoadScene(level);
        }
    }
}