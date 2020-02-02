using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderScript : MonoBehaviour
{
  public float attack = 100;
  public float range = 40f;
  public float turnSpeed = 15f;
  public GameObject target = null;

  private Animator animator;

  [Header("Attack Variables")]
  public float fireRate = 1f;
  public GameObject bulletPrefab;
  public Transform firePoint;

	public GameObject shotEffect;

  // Use this for initialization
  void Start()
  {
    animator = GetComponent<Animator>();
    InvokeRepeating("updateTarget", 0f, 0.5f);
    InvokeRepeating("shootTarget", 0f, 1f/fireRate);
  }

  // Update is called once per frame
  void Update()
  {
    if (target == null) {
      return;
    }

    Vector3 dir = target.transform.position - transform.position;
    Quaternion lookRotation = Quaternion.LookRotation(dir);
    Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

  }

  void shootTarget()
  {
    if (target == null)
    {
      return;
    }
    if(target.GetComponent<AttackerEngine>().isDead)
    {
      clearTarget();
      return;
    }

    animator.SetTrigger("attack");
		GameObject effectInst = Instantiate(shotEffect, firePoint.position, firePoint.rotation);
		Destroy(effectInst, 2f);

    GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Bullet bullet = bulletGO.GetComponent<Bullet>();
    if(bullet != null)
    {
      bullet.Seek(target.transform, attack);
    }
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

    if (nearestEnemy == null) {
      clearTarget();
    }
    else if (shortestDistance > range)
    {
      clearTarget();
    }
    else
    {
      if (target == null) {
        setTarget(nearestEnemy);
      }
      else
      {
        var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget > range)
          setTarget(nearestEnemy);
      }
    }

  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
  }

  private void setTarget(GameObject _target)
  {
    target = _target;
    // animator.SetBool("isAttacking", true);
  }

  private void clearTarget()
  {
    target = null;
    // animator.SetBool("isAttacking", false);
  }
}
