using UnityEngine;
using System.Collections;

public class active_buttons : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
				PauseMenu();

		
	}
	void PauseMenu(){
		pauseMenu pauseMenu = gameObject.GetComponent<pauseMenu>();
		if(!pauseMenu.enabled && !GameObject.Find("round_menu").GetComponent<roundEnd>().enabled){
			Time.timeScale = 0.0f;
			pauseMenu.LoadAchivments();
			pauseMenu.enabled = true;
		}
		
	}
}
