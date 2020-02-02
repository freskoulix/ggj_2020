using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
  public enum SpawnState { SPAWNING, COUNTING }
  public int nextWave;
  public float timeBetweenWaves = 5f;
  public float waveCountdown;
  public Transform enemy;

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

    if (waveCountdown <= 0 && state != SpawnState.SPAWNING)
    {
      // Start spawning
      // Debug.Log("Spawning new wave");
      StartCoroutine(SpawnWave(enemy));
      StartNextWave();
            FindObjectOfType<AudioManager>().Stop("intermission");
            FindObjectOfType<AudioManager>().Play("start of round");
            FindObjectOfType<AudioManager>().Play("running");
    }
    else
    {
      waveCountdown -= Time.deltaTime;
    }
  }

  IEnumerator SpawnWave(Transform enemy)
  {
    state = SpawnState.SPAWNING;
    foreach (Transform spawnPoint in spawnPoints)
    {
      SpawnEnemy(spawnPoint, enemy);
    }

    yield break;
  }

  void StartNextWave()
  {
    Debug.Log("Starting new Wave");
    nextWave++;
    state = SpawnState.COUNTING;
    waveCountdown = timeBetweenWaves;

  }

  void SpawnEnemy(Transform _spawnPoint, Transform _enemy)
  {
    // Debug.Log("Spawning Enemies");
    // Debug.Log("Spawning at SP: " + _spawnPoint.name);
    // Debug.Log("Spawning Enemy: " + _enemy.name);
    Instantiate(_enemy, _spawnPoint.position, _spawnPoint.rotation);
  }
}

// [System.Serializable]
// public class Wave
// {
//   public string name;
//   public Transform enemy;
// }
