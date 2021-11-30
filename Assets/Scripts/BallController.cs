using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	private GameManager gameManager;
	private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		gameManager = FindObjectOfType<GameManager>();
    }
	
	void Update(){
		
		
	}
	
	
	public void OnCollisionEnter(Collision col){
		
		Debug.Log(col.gameObject);
		if(col.gameObject.tag == "Gold"){
			gameManager.OnGoldCoinCollect();
			Destroy(col.gameObject);
		}else if(col.gameObject.tag == "Bomb"){
			
			gameManager.GameOver();
		}
		
	}
	
	

    
}
