using UnityEngine;

public class Enemy : MonoBehaviour {

    public static int damage = 50;
    private float distance = 0F;
    public Transform target;

    void Start (){
        // target = Wall.point;
        // if(distance <= 0.5){
        //     Attack(target);
        // }
	}

	void Update () {
		
	}

    void Attack(Transform target)
    {
        //target.TakeDamage(damage);
    }
}
