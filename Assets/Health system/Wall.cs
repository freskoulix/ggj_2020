using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour {

    public float startHealth = 1000F;
    public float health;
    public static Transform point;
    private Image healthBar;

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
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        healthBar.fillAmount = health / startHealth;
    }

	void Update () {
        healthBar.fillAmount = health / startHealth;
        if(health <= 0){
            Destroy(gameObject.transform.parent.gameObject);
            //Also destroy tower/wall if it's a different entity
        }
    }
}
