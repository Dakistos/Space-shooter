using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class md_CapsuleBonus : MonoBehaviour
{
    private static GameManager gameManager;
    public GameObject[] capsule;
    GameObject bonusItem;

    // Player
    GameObject playerGO;

    public float speed = 5f;

    Camera cam;
    float height;
    float width;

    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    void Update()
    {
        md_DestroyCapsule();
    }

    // Instantiate a capsule (call it where we check last enemy position in "md_Enemys")
    public void md_GetBonus(Vector3 position)
    {
        GameObject bonus = Instantiate(capsule[Random.Range(0, capsule.Length)], position, Quaternion.identity);
        bonus.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, speed, 0);
    }

    // Heal method when player trigger this capsule's bonus type
    public void md_heal()
    {
        gameManager.lives++;
        gameManager.md_UpdateTexts();
    }

    // Set a shield if player trigger this capsule's bonus type
    public void md_Shield()
    {
        playerGO = GameObject.Find("Player");
        playerGO.transform.Find("shieldAura").gameObject.SetActive(true);
    }

    // Destroy capsule if it out of bounds
    void md_DestroyCapsule()
    {
        if (GameObject.FindWithTag("capsuleBonus"))
        {
            bonusItem = GameObject.FindWithTag("capsuleBonus");

            if (bonusItem.transform.position.y > height - 1)
            {
                Destroy(bonusItem);
            }
        }
    }

}
