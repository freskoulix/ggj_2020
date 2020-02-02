using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	private WaveSpawner waveSpawner;

	private Text countDownTextComponent;
	private Text currentWaveTextComponent;

	// Use this for initialization
	void Start () {
		waveSpawner = FindObjectOfType<WaveSpawner>();
		countDownTextComponent = transform.GetChild(0).GetChild(0).GetComponent<Text>();
		currentWaveTextComponent = transform.GetChild(0).GetChild(1).GetComponent<Text>();
		InvokeRepeating("updateWavesUI", 0f, 0.1f);
	}

	void updateWavesUI ()
	{
		float currentWave = waveSpawner.nextWave;
		currentWaveTextComponent.text = "Current Wave: " + (currentWave);
		countDownTextComponent.text = "Countdown: " + (int) waveSpawner.waveCountdown;
	    var attackPoints = GameObject.Find("/AttackPoints").transform;
		var iLen = attackPoints.childCount;
		if(iLen < 8)
		{
			SceneManager.LoadScene("Game Over Menu");
		}
	}
}
