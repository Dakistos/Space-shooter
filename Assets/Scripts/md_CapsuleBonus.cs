using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_CapsuleBonus : MonoBehaviour
{

    public GameObject[] capsule;
    public float speed = 5f;
    private static GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    public void md_GetBonus(Vector3 position)
    {
        GameObject bonus = Instantiate(capsule[Random.Range(0, capsule.Length)], position, Quaternion.identity);
        bonus.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, speed, 0);
    }

    public void md_heal()
    {
        gameManager.lives++;
        gameManager.md_UpdateTexts();
    }

}
