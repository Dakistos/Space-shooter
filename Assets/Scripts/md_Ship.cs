using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_Ship : MonoBehaviour
{
	// Player's speed
	readonly float speed = 3f;

	// Player's shield
	public Transform shield;
	public bool activateShield;
	int shieldLife = 10;

	// Allow firing
	public GameObject projectile;
	readonly float projectileSpeed = 5f;

	// Fire rate control
	readonly float fireRate = .6f;
	float nextFire;

	Rigidbody2D rb;
	GameManager gameManager;
	md_CapsuleBonus capsule;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		capsule = GameObject.Find("GameManager").GetComponent<md_CapsuleBonus>();

		// Deactivate shield child object
		shield = transform.Find("shieldAura");
		activateShield = false;
		shield.gameObject.SetActive(false);
	}

	void Update()
	{
		if (GameManager.state == GameManager.States.play)
		{
			md_Move();
			md_Fire();
			//md_HasShieldTest();
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

	// Only to test shield with Editor checkbox (there is not necessary for the game)
	void md_HasShieldTest()
    {
		if (activateShield)
        {
			shield.gameObject.SetActive(true);
			activateShield = true;
        } else
        {
			shield.gameObject.SetActive(false);
			activateShield = false;
		}
    }

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.name == "health(Clone)")
		{			
			capsule.md_heal();
			Destroy(target.gameObject);
		}
		if (target.name == "shield(Clone)")
        {
			activateShield = true;
			shieldLife = 10;
			capsule.md_Shield();
			Destroy(target.gameObject);
        }

		if (target.tag == "EnemyBullet" || target.tag == "Enemy")
        {
			if (activateShield)
			{
				Debug.Log("Je suis ici");
				if (shieldLife <= 0)
                {
					activateShield = false;
					shield.gameObject.SetActive(false);
                }

				shieldLife -= 1;
				Destroy(target.gameObject);
			} 
			else
            {
				gameManager.md_KillPlayer();
				Destroy(target.gameObject);
			}
        }
	}
}
