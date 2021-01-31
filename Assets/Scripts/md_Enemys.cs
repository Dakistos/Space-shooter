using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_Enemys: MonoBehaviour
{
	public float xSpeed;
	public float ySpeed;
	Vector2 speed;

	public bool canShoot;
	readonly float fireRate = 1f;

	// pouvoir tirer
	public GameObject projectile;

	public int points = 10;
	float bound_Y = 11f;

	Rigidbody2D rb;

	GameManager gameManager;


	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		// appliquer la vélocité
		rb = GetComponent<Rigidbody2D>();

		if (canShoot)
        {
			md_Shoot();
		}

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

	void md_Shoot()
    {
		GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
		// Set behaviour of enemy bullet
		bullet.GetComponent<md_Bullet>().is_EnemyBullet = true;
		bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, -bullet.GetComponent<md_Bullet>().speed, 0);

		Invoke("md_Shoot", fireRate);
	}


	void OnTriggerEnter2D(Collider2D target)
	{
		// Bullet  Player  Enemy

		if (target.tag == "Player")
		{
			gameManager.md_KillPlayer();
		}
		else if (target.tag == "Bullet")
		{
		//détruire la bullet
			Destroy(target.gameObject);
			// destruction = asteroid initial
			Destroy(gameObject);
			// score
			gameManager.md_AddScore(points);
		}
	}
}
