using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour {

    public float startHealth = 1000F;
    public float health;
    private Image healthBar;

    Renderer rend;
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void RepairDamage(float repair)
    {
        if (health >= startHealth) {
            return;
        }

        health += repair;
    }

    void Start () {
        health = startHealth;
        health = Random.Range(1.0f,startHealth);

        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        healthBar.fillAmount = health / startHealth;

        rend = transform.parent.transform.GetChild(1).GetComponent<Renderer>();

        // Use the Specular shader on the material
        // rend.material.shader = Shader.Find("towerShad");
    }

	void Update () {
        healthBar.fillAmount = health / startHealth;
        rend.material.SetFloat("_Health", health / startHealth);

        if(health <= 0){
            Destroy(gameObject.transform.parent.gameObject);
            //Also destroy tower/wall if it's a different entity
        }
    }
}
