using UnityEngine;

public class testattack : MonoBehaviour {

    public float health = 100;

    public Wall target;
    public float maxDist;
    public float dist;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("dealDamage", 0f, 1f);
    }

    private void Update()
    {
        // These should now already be taken care of by the main Attacker Engine
        // if(health <= 0)
        // {
        //     animator.SetTrigger("death_trigger");
        // }
    }

    void dealDamage()
    {
        dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist <= maxDist)
        {
            animator.SetTrigger("attack_trigger");
            target.TakeDamage(100F);
        }
    }

}