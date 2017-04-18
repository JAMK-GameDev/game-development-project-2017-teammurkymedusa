using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AirfieldController : MonoBehaviour
    {
        public GameObject PlayMenu;
        public Button Airfield;
        public string LevelName;
        public float SelectorPosition_X;
        public float SelectorPosition_Y;

        private void Start()
        {
            Airfield.GetComponent<Button>().onClick.AddListener(ActivatePlayMenu);
        }

        void ActivatePlayMenu()
        {
            PlayMenu.SetActive(true);
            //set play menu to proper location
            PlayMenu.transform.position =  new Vector3(SelectorPosition_X, SelectorPosition_Y, 0);
            PlayMenu.GetComponent<PlayButton>().SetLevelName(LevelName);
        }
    }
}