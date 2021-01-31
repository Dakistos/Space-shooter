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

	public int level;
	public int score;
	public int lives;

	public Text levelTxt;
	public Text scoreTxt;
	public Text livesTxt;

	public Text messageTxt;

	GameObject player;
	public GameObject asteroid; // le grand prefab
	public GameObject boom;

	Camera cam;
	float height, width;

	public GameObject waitToStart; // panel

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
		// restaurer après une partie
		player.SetActive(true);
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemys)
		{
			Destroy(enemy);
		}
		// lancer une partie
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
		level = 1;
		score = 0;
		lives = 5;
	}

	public void md_UpdateTexts()
	{
		levelTxt.text = "wave: " + level;
		scoreTxt.text = "score: " + score;
		livesTxt.text = "lives: " + lives;
	}


	public void md_AddScore(int points)
	{
		score += points;
		md_UpdateTexts();
	}

	void Update()
	{
		if (state == States.play)
		{
			//EndOfLevel();
		}
	}


	void md_EndOfLevel()
	{
		GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemys.Length == 0)
		{
			//StartCoroutine(md_LevelUp());
		}
	}




	public void md_KillPlayer()
	{
		StartCoroutine(PlayerAgain());
	}

	IEnumerator PlayerAgain()
	{
		state = States.dead;

		GameObject boomGO = Instantiate(boom, player.transform.position, Quaternion.identity);
		player.SetActive(false);

		yield return new WaitForSecondsRealtime(2f);
		player.SetActive(true);
		state = States.play;
		lives -= 1;
		player.SetActive(false);
		md_UpdateTexts();
		if (lives <= 0)
		{
			yield return new WaitForSecondsRealtime(2f);
			Destroy(boomGO);
			md_GameOver();
		}
		else
		{
			yield return new WaitForSecondsRealtime(3f);
			Destroy(boomGO);
			player.SetActive(true);
			state = States.play;
		}
	}

	void md_GameOver()
	{
		state = States.wait;

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
