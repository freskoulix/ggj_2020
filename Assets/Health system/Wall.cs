using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour {

    public float startHealth = 1000F;
    public float health;
    public static Transform point;
    public Image healthBar;

    private void Awake()
    {
        point = transform.GetChild(0);
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;
    }

    void Start () {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
    }

	void Update () {
        healthBar.fillAmount = health / startHealth;
        if(health <= 0){
            Destroy(this);
        }
    }
}
