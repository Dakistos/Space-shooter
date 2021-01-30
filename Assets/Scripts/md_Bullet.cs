using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_Bullet : MonoBehaviour
{

	Camera cam;
	float height;
	float width;

    void Start()
    {
		cam = Camera.main;
		height = cam.orthographicSize;
		width = height * cam.aspect;
	}

	// Destroy bullet if it out of screen
    void Update()
	{
		if(transform.position.y > height -1 || transform.position.y < -height +1)
        {
			Destroy(gameObject);
        }
	}
}
