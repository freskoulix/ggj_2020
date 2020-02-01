using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
  public enum SpawnState { SPAWNING, COUNTING }
  public Wave[] waves;
  public int nextWave;
  public float timeBetweenWaves = 5f;
  public float waveCountdown;
  public SpawnState state = SpawnState.COUNTING;

  public Transform spawnPoints;
  // Use this for initialization
  void Start()
  {
    state = SpawnState.COUNTING;
    waveCountdown = timeBetweenWaves;
  }

  // Update is called once per frame
  void Update()
  {

    if (nextWave == -1)
      return;

    if (waveCountdown <= 0 && state != SpawnState.SPAWNING)
    {
      // Start spawning
      Debug.Log("Spawning new wave");
      StartCoroutine(SpawnWave(waves[nextWave]));
      StartNextWave();
    }
    else
    {
      waveCountdown -= Time.deltaTime;
    }
  }

  IEnumerator SpawnWave(Wave _wave)
  {
    Debug.Log("Spawning Wave: " + _wave.name);
    state = SpawnState.SPAWNING;
    foreach (Transform spawnPoint in spawnPoints)
    {
      SpawnEnemy(spawnPoint, _wave.enemy);
    }

    yield break;
  }

  void StartNextWave()
  {
    Debug.Log("Starting new Wave");
    nextWave++;
    if (nextWave > waves.Length - 1)
    {
      Debug.Log("Waves completed. Stopping");
      nextWave = -1;
    }
    state = SpawnState.COUNTING;
    waveCountdown = timeBetweenWaves;

  }

  void SpawnEnemy(Transform _spawnPoint, Transform _enemy)
  {
    Debug.Log("Spawning Enemies");
    Debug.Log("Spawning at SP: " + _spawnPoint.name);
    Debug.Log("Spawning Enemy: " + _enemy.name);
    Instantiate(_enemy, _spawnPoint.position, _spawnPoint.rotation);
  }
}

[System.Serializable]
public class Wave
{
  public string name;
  public Transform enemy;
}
