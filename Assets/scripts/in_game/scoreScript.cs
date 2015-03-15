using UnityEngine;
using System.Collections;

public class scoreScript : MonoBehaviour {
	
	public int balls, max_balls;
	public int ball = 0, limit = 10;
	public int score = 0, default_score = 100;
	public int combo = 0, combos = 0, cleans = 0, tricks = 0;
	public bool down = false, end_it = false;
	
	public GameObject basketball, clean_sprite, trick_sprite;
	static float ball_cooldown = 1f, cooldown = 1f, clean_countdown, trick_countdown;
	static float sprite_countdown = 2f;
	
	public GameObject lastball;
	static showScore showScore;
	static gameScript gameScript;

	
	// Score and ball stuff
	void FixedUpdate(){
		if(down && cooldown > 0) cooldown -= Time.fixedDeltaTime;
		else if(cooldown <= 0){ newBall(); cooldown = ball_cooldown;}
		
		if(clean_countdown > 0) clean_countdown -= Time.fixedDeltaTime;
		else clean_sprite.renderer.enabled = false;
		
		if(trick_countdown > 0) trick_countdown -= Time.fixedDeltaTime;
		else trick_sprite.renderer.enabled = false;
	}
	
	void Start () {
		gameScript = GameObject.Find("gameScript").GetComponent<gameScript>();
		showScore = gameObject.GetComponent<showScore>();
		gameObject.GetComponent<showScore>().enabled =  true;
		combo = gameScript.combo;
		balls = gameScript.balls;
		max_balls = gameScript.max_balls; 
		newBall();
		UpdateScore();
		
	}
	
	void newBall(){
		down = false;
		if (ball >= limit || balls == 0) {
			end_it = true;
			return;	
		}
		else
			end_it = false;
				
		ball++;
		if(ball == limit){
			lastball = (GameObject) Instantiate(basketball, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f,1.5f), -2), Quaternion.identity );
			return;
		}
		Instantiate(basketball, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f,1.5f), -2), Quaternion.identity );
		
		if(ball == 1)
			UpdateScore();
	}
	
	//Ball callbacks
	public void Shot(){
		WastedBall();
	
	}
	public void WastedBall(){
		down = true;		
		balls--;
		UpdateScore();
		cooldown = ball_cooldown;
	}
	
	public void breakCombo(){
		combo = 0;
		UpdateScore();
	}

	public void Score(bool clean, bool back_wall){
		float multi = 1.0f;
		if(back_wall){
			balls += 2;
			multi +=.5f;
			tricks++;
			showSprite("trick");
		}
		if(clean){
			balls += 1;
			multi +=.25f;
			cleans++;
			showSprite("clean");
		}
		balls += 1;
		if(balls > max_balls) balls = 10;
		score += (int)Mathf.Ceil(default_score*multi) + (int)Mathf.Ceil(combo*10*multi);
		combo++;
		UpdateScore();

		if(end_it)
			newBall();

		bool done = GameObject.Find("gameScript").GetComponent<aManager>().Check(combo, cleans, tricks, clean);
		if(done)
			showScore.AchivmentDone();
	}
	
	public void DestroyedBall(GameObject gBall){
		if(gBall == lastball || balls == 0){	
			gameObject.GetComponent<showScore>().enabled =  false;
			gameScript.FinishRound(score, combo, balls);
		}
	}
	
	
	
	//
	void showSprite(string sprite_name){
		switch(sprite_name){
			case "clean":
				clean_sprite.transform.position = new Vector3(Random.Range(-5.8f, -4.1f), Random.Range(0f,4f), -1);
				clean_sprite.renderer.enabled = true;
				clean_countdown = sprite_countdown;
				break;
				
			case "trick":
				trick_sprite.transform.position = new Vector3(Random.Range(-6f, -1f), Random.Range(-2f,-3f), -1);
				trick_sprite.renderer.enabled = true;
				trick_countdown = sprite_countdown;
				break;
		}
	}
	
	void UpdateScore(){
		showScore.Set(balls, ball, gameScript.round, gameScript.points + score, gameScript.record, gameScript.multi, gameScript.player, gameScript.mpoints);
	}
	
}
