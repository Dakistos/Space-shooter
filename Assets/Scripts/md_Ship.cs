using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_Ship : MonoBehaviour
{
	// accélération / décélération
	readonly float speed = 3f;

	// pouvoir tirer
	public GameObject projectile;
	readonly float projectileSpeed = 4f;

	// controler la fréquence de tir
	readonly float fireRate = .25f;
	float nextFire;

	Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		//if (GameManager.state == GameManager.States.play)
		//{
			md_Move();
			md_Fire();
		//}

		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		pos.x = Mathf.Clamp01(pos.x);
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	// Call md_Shoot when player press Fire1 on his mouse
	void md_Fire()
	{
		nextFire += Time.deltaTime;
		if (Input.GetButton("Fire1") && nextFire > fireRate)
		{
			md_Shoot();
			nextFire = 0;
		}
	}

	// Instantiate a bullet gameObject
	void md_Shoot()
	{
		GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
		bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, projectileSpeed, 0);
	}

	// Move the player when keyboard's key are pushed (Left, Right)
	void md_Move()
	{
		/*	if(Input.GetKey(KeyCode.LeftArrow))
			{
				transform.position += transform.right * speed * Time.deltaTime;
			}
			if(Input.GetKey(KeyCode.RightArrow))
			{
				transform.position += transform.right * -speed * Time.deltaTime;
			}*/

		rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
	}
}
