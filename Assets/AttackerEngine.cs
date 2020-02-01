﻿using UnityEngine;
using UnityEngine.AI;

public class AttackerEngine : MonoBehaviour {
  Transform GetNearestAttackPoint() {
    var attackPoints = GameObject.Find("/AttackPoints").transform;
    var iLen = attackPoints.childCount;

    Transform nearestAttackPoint = attackPoints.GetChild(0);
    float minDistance = float.MaxValue;

    for (int i = 0; i < attackPoints.childCount; i++) {
      var attackPoint = attackPoints.GetChild(i);
      var distance = Vector3.Distance(attackPoint.transform.position, transform.position);
      Debug.Log("AttackerPoint: " + i + " distance: " + distance + " maxDistance:" + minDistance);
      if (distance < minDistance) {
        nearestAttackPoint = attackPoint;
        minDistance = distance;
      }
    }

    return nearestAttackPoint.transform;
  }

  // Use this for initialization
  void Start () {
    var goal = GetNearestAttackPoint();
    NavMeshAgent agent = GetComponent<NavMeshAgent>();
    agent.destination = goal.position;
    agent.speed = Random.Range(15, 35);
    agent.acceleration = Random.Range(2, 10);
  }

  // Update is called once per frame
  void Update () {

  }
}
