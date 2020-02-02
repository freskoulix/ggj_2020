using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenuManager : MonoBehaviour {

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
