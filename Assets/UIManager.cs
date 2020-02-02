using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private WaveSpawner waveSpawner;

	private Text countDownTextComponent;
	private Text currentWaveTextComponent;
	private Text maxWavesTextComponent;

	private int wavesLength = 0;
	// Use this for initialization
	void Start () {
		waveSpawner = FindObjectOfType<WaveSpawner>();
		countDownTextComponent = transform.GetChild(0).GetChild(0).GetComponent<Text>();
		currentWaveTextComponent = transform.GetChild(0).GetChild(1).GetComponent<Text>();
		maxWavesTextComponent = transform.GetChild(0).GetChild(2).GetComponent<Text>();

		wavesLength = (int) waveSpawner.waves.Length;
		maxWavesTextComponent.text = "Total Waves: " + wavesLength;
		InvokeRepeating("updateWavesUI", 0f, 0.1f);

	}

	void updateWavesUI ()
	{
		float currentWave = waveSpawner.nextWave;
		if(currentWave == -1)
			currentWave = wavesLength;
		currentWaveTextComponent.text = "Current Wave: " + (currentWave);

		if(currentWave == wavesLength)
			countDownTextComponent.text = "Final Wave!";
		else
			countDownTextComponent.text = "Countdown: " + (int) waveSpawner.waveCountdown;
	}
}
