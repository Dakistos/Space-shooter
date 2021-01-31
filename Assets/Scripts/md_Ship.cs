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
	readonly float fireRate = .5f;
	float nextFire;

	Rigidbody2D rb;
	GameManager gameManager;
	md_CapsuleBonus capsule;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		capsule = GameObject.Find("GameManager").GetComponent<md_CapsuleBonus>();
	}

	void Update()
	{
		if (GameManager.state == GameManager.States.play)
		{
			md_Move();
			md_Fire();
		}

		// Block player if he goes on left or right bounds
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

	// Move the player when keyboard's arrows are pushed (Left, Right)
	void md_Move()
	{
		rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "capsuleBonus")
		{			
			capsule.md_heal();
			Destroy(target.gameObject);
		}

		if (target.tag == "EnemyBullet")
        {
			gameManager.md_KillPlayer();
        }
	}
}
