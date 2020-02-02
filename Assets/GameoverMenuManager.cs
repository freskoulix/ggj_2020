using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverMenuManager : MonoBehaviour {

	public Transform scoreTextObject;

	public void Start()
	{
		var score = PlayerPrefs.GetInt("Score");
		var scoreText = "You lasted " + score + " rounds. Try repairing your castle!";
		if(score > 5 && score < 10)
			scoreText = "Awesome. You lasted " + score + " rounds";
		else if(score > 10)
			scoreText = "EPIC! You lasted " + score + " rounds";

		scoreTextObject.GetComponent<Text>().text = scoreText;
	}
	public void ReplayGame(){
		SceneManager.LoadScene("Main Scene");
	}

	public void MainMenu(){
		SceneManager.LoadScene("Main Menu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
