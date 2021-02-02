using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_Enemys: MonoBehaviour
{
	float maxValue = 7f; // Max value the enemys can reach (y axis)
	float minValue = -9.5f; // Min value the enemys can reach (y axis)
	float position_Y = -11f; // Set enemy start position
	public float speed = 4f;

	public bool canShoot;
	readonly float fireRate = 1f;
	float nextFire;

	public GameObject projectile;

	public int points = 10;

	GameManager gameManager;
	GameObject player;

	Camera cam;
	float height;
	float width;


	void Start()
	{
		cam = Camera.main;
		height = cam.orthographicSize;
		width = height * cam.aspect;

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = GameObject.Find("Player");
	}

	void Update()
	{
		md_Move();
		if (canShoot)
		{
			nextFire += Time.deltaTime;
			if (nextFire > fireRate)
            {
				md_Shoot();
				nextFire = 0;
			}
		}
	}

	void md_Move()
    {
		// Move the enemy
		position_Y += Time.deltaTime * speed;
		transform.position = new Vector2(transform.position.x, position_Y);

		if (canShoot)
        {
			// If enemy position reach the up y axis limit => change direction to down
			if (position_Y >= maxValue)
			{
				speed *= -1;
				position_Y = maxValue;
			}
			// Else if enemy position reach the down y axis limit => change direction to up
			else if (position_Y <= minValue)
			{
				speed *= -1;
				position_Y = minValue;
			}
		} 
		else
        {
			if (transform.position.y > height - 1)
            {
				Destroy(gameObject);
            }
        }
	}

	void md_Shoot()
    {
		if(nextFire > fireRate)
        {
			GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
			// Set behaviour of enemy bullet
			bullet.GetComponent<md_Bullet>().is_EnemyBullet = true;
			bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, -bullet.GetComponent<md_Bullet>().speed, 0);
		}
	}


	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Player")
		{
			// Destroy enemy
			Destroy(gameObject);
		}
		else if (target.tag == "Bullet")
		{
			// Destroy bullet
			Destroy(target.gameObject);
			// Destroy enemy
			Destroy(gameObject);
			// add score
			gameManager.md_AddScore(points);
		}
	}
}
