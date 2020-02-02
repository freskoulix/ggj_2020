using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 350f;
	private Transform target;
	private float attackPower = 0;
	public GameObject impactEffect;

	// Use this for initialization
	public void Seek (Transform _target, float _attackPower) {
		target = _target;
		attackPower = _attackPower;
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if(dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

	void HitTarget()
	{
		if (target == null)
		{
			return;
		}
		GameObject effectInst = Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectInst, 2f);

		target.GetComponent<AttackerEngine>().TakeDamage(attackPower);
		Destroy(gameObject);
	}
}
