using UnityEngine;

public class testattack : MonoBehaviour {

    public float health = 100;

    public Wall target;
    public float maxDist;
    public float dist;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("dealDamage", 0f, 1f);
    }

    private void Update()
    {
        if(health <= 0)
        {
            GetComponent<Animator>().SetTrigger("death_trigger");
        }
    }

    void dealDamage()
    {
        dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist <= maxDist)
        {
            GetComponent<Animator>().SetTrigger("attack_trigger");
            target.TakeDamage(100F);
        }
    }

}