using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AttackerEngine : MonoBehaviour
{
  public static float startHealth = 100;
  public float damage = 100f;
  public float health = startHealth;
  public bool isDead = false;
  public float attackDistance = 15;
  public Transform goalAttackPoint;
  private Animator animator;
  private NavMeshAgent agent;

  private Transform parent;
  private Transform healthBarCanvas;
  private Image healthBar;
	Vector3 canvasModelPositionDiff;

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
        nearestAttackPoint = attackPoint.transform;
        minDistance = distance;
      }
    }

    return nearestAttackPoint;
  }

  // Use this for initialization
  void Start()
  {

    // health = startHealth;
    parent = transform.parent;
    healthBarCanvas = parent.transform.GetChild(0);

    healthBar = healthBarCanvas.GetChild(0).GetChild(0).GetComponent<Image>();
    healthBar.fillAmount = health / startHealth;
		canvasModelPositionDiff = healthBarCanvas.position - transform.position;


    goalAttackPoint = GetNearestAttackPoint();
    agent = GetComponent<NavMeshAgent>();
    agent.destination = goalAttackPoint.position;
    agent.speed = Random.Range(15, 35);
    agent.acceleration = Random.Range(2, 10);
    animator = GetComponent<Animator>();
    animator.SetBool("move_bool", true);
    InvokeRepeating("tryAttacking", 0f, 1.5f);
  }

  // Update is called once per frame
  void Update()
  {
    if(healthBarCanvas != null)
    {
      healthBarCanvas.position = transform.position + canvasModelPositionDiff;
      healthBar.fillAmount = health / startHealth;
      if(health <= 0){
          Destroy(healthBarCanvas.gameObject);
          //Also destroy tower/wall if it's a different entity
      }
    }

    if(goalAttackPoint == null)
      return;

    var dist = Vector3.Distance(goalAttackPoint.transform.position, transform.position);
    if (dist <= attackDistance)
    {
      //This will probably conflict with the actual attacking mechanism
      animator.SetBool("move_bool", false);
      // animator.SetBool("attack_bool", true);
      agent.velocity = Vector3.zero;
      agent.Stop();
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

    void tryAttacking()
    {
        if(goalAttackPoint == null)
            return;
        if(isDead == true)
          return;

        float dist = Vector3.Distance(goalAttackPoint.transform.position, transform.position);

        if (dist <= attackDistance)
        {
            animator.SetTrigger("attack_trigger");
            Invoke("damageTarget", 0.6f);
        }
    }

    void damageTarget()
    {
        if(goalAttackPoint == null)
            return;

      goalAttackPoint.GetChild(0).GetComponent<Wall>().TakeDamage(damage);
    }

  public void deleteMe()
  {
    Destroy(this.gameObject);
  }
}
