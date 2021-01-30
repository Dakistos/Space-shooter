using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_Enemys: MonoBehaviour
{
	public float xSpeed;
	public float ySpeed;
	Vector2 speed;

	bool canShoot;
	//readonly float fireRate = .25f;
	float nextFire;

	// pouvoir tirer
	public GameObject projectile;
	//readonly float projectileSpeed = 4f;

	public int points = 10;
	float bound_Y = 11f;

	Rigidbody2D rb;

	GameManager gameManager;


	void Start()
	{
		//gameManager = GameObject.Find("GameManager").GetComponent<GameManager>()

		// appliquer la vélocité
		rb = GetComponent<Rigidbody2D>();

	}

	void Update()
	{
		md_Move();
	}

	void md_Move()
    {
		Vector3 temp = transform.position;
		speed = new Vector2(xSpeed, ySpeed);
		rb.velocity = speed;

		if(temp.y > bound_Y)
        {
			Destroy(gameObject);
        }
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		// Bullet  Player  Enemy

		if (collision.tag == "Player")
		{
			//gameManager.KillPlayer();
		}
		else if (collision.tag == "Bullet")
		{
		//détruire la bullet
			Destroy(collision.gameObject);
			// destruction = asteroid initial
			Destroy(gameObject);
			// score
			//gameManager.AddScore(points);
		}
	}
}
