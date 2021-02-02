using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_Bullet : MonoBehaviour
{

	Camera cam;
	float height;
	float width;

    public float speed = 5f;

    [HideInInspector]
    public bool is_EnemyBullet = false;

    void Start()
    {
		cam = Camera.main;
		height = cam.orthographicSize;
		width = height * cam.aspect;

        if (is_EnemyBullet)
            speed *= speed;
    }

	
    void Update()
	{
        // Destroy bullet if it out of screen
        if (transform.position.y > height -1 || transform.position.y < -height +1)
        {
			Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet" || target.tag == "EnemyBullet")
        {
            Destroy(gameObject);
        }
    }
}
