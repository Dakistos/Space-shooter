using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public enum States
	{
		wait, play, levelup, dead
	}
	public static States state;

	public int score;
	public int lives;

	public Text scoreTxt;
	public Text livesTxt;

	public Text messageTxt;

	GameObject player;
	public GameObject boom;

	Camera cam;
	float height, width;
	

	public GameObject waitToStart;

	void Start()
	{
		messageTxt.gameObject.SetActive(false);

		player = GameObject.FindWithTag("Player");

		cam = Camera.main;
		height = cam.orthographicSize;
		width = height * cam.aspect;


		waitToStart.gameObject.SetActive(true);
		int highscore = PlayerPrefs.GetInt("highscore");
		if (highscore > 0)
		{
			messageTxt.text = "highscore: " + highscore;
			messageTxt.gameObject.SetActive(true);
		}


		state = States.wait;
	}

    public void md_LaunchGame()
	{
		// interface
		waitToStart.gameObject.SetActive(false);
		messageTxt.gameObject.SetActive(false);

		// Restore a game
		player.SetActive(true);
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemys)
		{
			Destroy(enemy);
		}

		// Launch a game
		md_InitGame();
		md_LoadLevel();
	}


	void md_LoadLevel()
	{
		state = States.play;

		md_UpdateTexts();
	}

	void md_InitGame()
	{
		score = 0;
		lives = 5;
	}

	// Method to update different texts
	public void md_UpdateTexts()
	{
		scoreTxt.text = "score: " + score;
		livesTxt.text = "lives: " + lives;
	}

	// Method to add score
	public void md_AddScore(int points)
	{
		score += points;
		md_UpdateTexts();
	}

	// Method to call when something append to the player (down life or kill him)
	public void md_KillPlayer()
	{
		StartCoroutine(PlayerAgain());
	}

	IEnumerator PlayerAgain()
	{
		state = States.dead;

		// Instantiate sprite boom when something hit player
		GameObject boomGO = Instantiate(boom, player.transform.position, Quaternion.identity);
		player.SetActive(false);

		yield return new WaitForSecondsRealtime(2f);
		player.SetActive(true);
		state = States.play;
		lives -= 1;
		player.SetActive(false);
		md_UpdateTexts();

		// Display game over when player lives = 0
		if (lives <= 0)
		{
			yield return new WaitForSecondsRealtime(2f);
			Destroy(boomGO);
			md_GameOver();
		}
		else
		{
			yield return new WaitForSecondsRealtime(1.5f);
			Destroy(boomGO);
			player.SetActive(true);
			state = States.play;
		}
	}

	void md_GameOver()
	{
		state = States.wait;

		// check if actual score is higher than highscore then set it if it's true
		int highscore = PlayerPrefs.GetInt("highscore");
		if (score > highscore)
		{
			PlayerPrefs.SetInt("highscore", score);
			messageTxt.text = "new highscore: " + score;
		}
		else
		{
			messageTxt.text = "game over\nhighscore: " + highscore;
		}

		messageTxt.gameObject.SetActive(true);
		waitToStart.gameObject.SetActive(true);
	}
}
