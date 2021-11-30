using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	
	public bool gameStart;
	public bool gameOver;

	public int bombAmount;
	public int goldCointAmount;
	
	private int currentLevel;
	private int currentScore;
	private int highScore;
	
	public Text currentLevelText;
	public Text highScoreText;
	public Text currentScoreText;
	
	public GameObject tapToPlayGameObject;
	public GameObject gameOverObject;
	public GameObject goldPrefab;
	public GameObject bombPrefab;
	
	public GameObject ball;
    
    void Start()
    {
		gameStart = false;
		
		PlayerPrefs.DeleteKey("current level");
		PlayerPrefs.DeleteKey("current score");
		
		tapToPlayGameObject.SetActive(true);
		gameOverObject.SetActive(false);
		ResetLevelValue();
    }

	
	public void OnGoldCoinCollect(){
		
		currentScore += 1;
		currentScoreText.text = currentScore.ToString();
		PlayerPrefs.SetInt("current score", currentScore);
		
		highScore = PlayerPrefs.GetInt("highest score");
		if(highScore < currentScore){
			highScore = currentScore;
			PlayerPrefs.SetInt("highest score", highScore);
			highScoreText.text = highScore.ToString();
		}
		
		goldCointAmount -= 1;
		if(goldCointAmount == 0){
			
			Invoke("LevelComplete", 1.5f);
		}
		
	}
	
	public void LevelComplete(){
		
		currentLevel = PlayerPrefs.GetInt("current level") + 1;
		PlayerPrefs.SetInt("current level", currentLevel);
		ResetLevelValue();
	}
	
	public void GameOver(){
		
		PlayerPrefs.DeleteKey("current level");
		PlayerPrefs.DeleteKey("current score");
		
		gameOverObject.SetActive(true);
		Invoke("ReloadScene", 3f);
		
	}
	
	
	public void ResetLevelValue(){
		
		//Destroy all child Objects
		DestroyChildObjects();

		//set ball position
		ball.transform.position = new Vector3(0,1,0);
		//set variables default value
		currentLevel = PlayerPrefs.GetInt("current level");
		currentScore = PlayerPrefs.GetInt("current score");
		highScore = PlayerPrefs.GetInt("highest score");
		
		if(currentLevel == 0) {
			currentLevel = 1;
			PlayerPrefs.SetInt("current level", currentLevel);
		}
		
		currentLevelText.text = currentLevel.ToString();
		highScoreText.text = highScore.ToString();
		currentScoreText.text = currentScore.ToString();
		//Spawn new gold and bomb
		SpawnObjects();
		
	}
	
	void ReloadScene(){
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	
	void SpawnObjects(){
		
		bombAmount = Random.Range(2, 5);
		goldCointAmount = Random.Range(4, 8);
		
		SpawnBombs(bombAmount);
		SpawnGoldCoins(goldCointAmount);
		
	}
	
	
	void SpawnBombs(int bombAmount){
		
		for(int i=0;i<bombAmount;i++){
			
			Vector3 pos = new Vector3(Random.Range(-2,2), 0.5f, Random.Range(-2,3));
			GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
			bomb.transform.parent = this.transform;
		}
	}
	
	void SpawnGoldCoins(int goldCointAmount){
		
		for(int i=0;i<goldCointAmount;i++){
			
			Vector3 pos = new Vector3(Random.Range(-2,2), 0.5f, Random.Range(-2,3));
			GameObject gold = Instantiate(goldPrefab, pos, Quaternion.identity);
			gold.transform.parent = this.transform;
		}
	}
	
	void DestroyChildObjects(){
		
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
	
	public void GameStart(){
		
		gameStart = true;
		tapToPlayGameObject.SetActive(false);
	}
	
}
