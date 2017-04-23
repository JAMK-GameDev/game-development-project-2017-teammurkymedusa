using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This script should be attached to an object that is persistent between scenes
public class ScoreManager : MonoBehaviour {

	public float CurrentScore;
	public float HighScore; // This is retrieved from main menu
	public float difficultyMod; // Difficulty modifier, retrieved from GameController
	public float ScoreInterval; // How often player is scored
	//GameObjects
	public GameObject GameController;
	// Use this for initialization
	void OnLevelWasLoaded () {
		Scene currentScene = SceneManager.GetActiveScene ();
		string currentSceneName = currentScene.name;

		// Are we in Game scene or in menu
		if (currentSceneName == "Game") {
			// If so, these are needed
			if (GameController != null) {

			} else {
				GameController = GameObject.Find ("GameController");
			}
			difficultyMod = GameController.GetComponent<LevelGenerator> ().Difficulty;

		} else if (currentSceneName == "MainMenu") {
			CurrentScore = 0;
			GameObject.Find ("HighScoreText").GetComponent<Text> ().text = "High score: " + HighScore;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addTravelScore(){ // This is ran by GameController while player is moving onwards
		CurrentScore += (1 + difficultyMod / 2) * 2; // Let's use small numbers to prevent int overflow
	}

	public void SetHighScore(){
		if (CurrentScore >= HighScore)
			HighScore = CurrentScore;
	}
}
