using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private WaveSpawner waveSpawner;

	private Text countDownTextComponent;
	private Text currentWaveTextComponent;
	private Text maxWavesTextComponent;
	// Use this for initialization
	void Start () {
		waveSpawner = FindObjectOfType<WaveSpawner>();
		countDownTextComponent = transform.GetChild(0).GetChild(0).GetComponent<Text>();
		currentWaveTextComponent = transform.GetChild(0).GetChild(1).GetComponent<Text>();
		maxWavesTextComponent = transform.GetChild(0).GetChild(2).GetComponent<Text>();

		maxWavesTextComponent.text = "Total Waves: " + (int) waveSpawner.waves.Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
				countDownTextComponent.text = "Countdown: " + (int) waveSpawner.waveCountdown;
				currentWaveTextComponent.text = "Current Wave: " + (waveSpawner.nextWave + 1);
	}
}
