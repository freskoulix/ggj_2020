using UnityEngine;
using UnityEngine.AI;

public class AttackerEngine : MonoBehaviour
{
  public static float startHealth = 100;
  public float health = startHealth;
  public bool isDead = false;
  public float attackDistance = 15;
  private Transform goalAttackPoint;
  private Animator animator;
  private NavMeshAgent agent;

  Transform GetNearestAttackPoint()
  {
    var attackPoints = GameObject.Find("/AttackPoints").transform;
    var iLen = attackPoints.childCount;

    Transform nearestAttackPoint = attackPoints.GetChild(0);
    float minDistance = float.MaxValue;

    for (int i = 0; i < attackPoints.childCount; i++)
    {
      var attackPoint = attackPoints.GetChild(i);
      var distance = Vector3.Distance(attackPoint.transform.position, transform.position);

      if (distance < minDistance)
      {
        nearestAttackPoint = attackPoint;
        minDistance = distance;
      }
    }

    return nearestAttackPoint.transform;
  }

  // Use this for initialization
  void Start()
  {
    goalAttackPoint = GetNearestAttackPoint();
    agent = GetComponent<NavMeshAgent>();
    agent.destination = goalAttackPoint.position;
    agent.speed = Random.Range(15, 35);
    agent.acceleration = Random.Range(2, 10);
    animator = GetComponent<Animator>();
    animator.SetBool("move_bool", true);
  }

  // Update is called once per frame
  void Update()
  {
    var dist = Vector3.Distance(goalAttackPoint.transform.position, transform.position);
    if (dist <= attackDistance)
    {
      animator.SetBool("move_bool", false);
      animator.SetBool("attack_bool", true);
      // agent.velocity = Vector3.zero;
      // agent.Stop();
    }
    if (health <= 0 && !isDead)
    {
      animator.SetBool("attack_bool", false);
      animator.SetBool("move_bool", false);
      animator.SetBool("death_bool", true);
      agent.velocity = Vector3.zero;
      agent.Stop();
      isDead = true;
      Invoke("deleteMe", 4f);
    }
  }

  public void TakeDamage(float attack)
  {
    health -= attack;
  }

  public void deleteMe()
  {
    Destroy(this.gameObject);
  }
}
