using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderScript : MonoBehaviour
{

  public float attack = 100;
  public float range = 40f;
  public float turnSpeed = 15f;
  public GameObject target = null;
  // Use this for initialization
  void Start()
  {
    InvokeRepeating("updateTarget", 0f, 0.5f);
    InvokeRepeating("attackTarget", 0f, 1f);
  }

  // Update is called once per frame
  void Update()
  {
    if (target == null)
      return;

    Vector3 dir = target.transform.position - transform.position;
    Quaternion lookRotation = Quaternion.LookRotation(dir);
    Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
  }

  void updateTarget()
  {
    float shortestDistance = Mathf.Infinity;
    GameObject nearestEnemy = null;
    var enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies)
    {
      float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
      if (distanceToEnemy < shortestDistance && !enemy.GetComponent<AttackerEngine>().isDead)
      {
        shortestDistance = distanceToEnemy;
        nearestEnemy = enemy;
      }
    }


    if (nearestEnemy == null)
      target = null;
    else if (shortestDistance > range)
    {
      target = null;
    }
    else
    {
      if (target == null)
        target = nearestEnemy;
      else
      {
        var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget > range)
          target = nearestEnemy;
      }
    }

  }

  public void attackTarget()
  {
    if (target != null && !target.GetComponent<AttackerEngine>().isDead)
    {
      target.GetComponent<AttackerEngine>().TakeDamage(attack);
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
  }
}
