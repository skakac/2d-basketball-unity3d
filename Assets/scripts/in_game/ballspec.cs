using UnityEngine;
using System.Collections;

public class ballspec : MonoBehaviour {
	public bool back_wall = false, clean = true;
	public bool fist_check = false, score = false, can = true;
	
	
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "BackWall")
			back_wall = true;
		if(other.gameObject.tag == "Rim"){
			clean = false;
		}
						
		if(other.gameObject.tag == "Floor" && !score && can ){			
			GameObject.Find("scoreScript").GetComponent<scoreScript>().breakCombo();
			can = false;
			}
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Net1" && !score)
			fist_check = true;
			
		if(other.gameObject.tag == "Floor"){
			if(can)
				GameObject.Find("gameScript").GetComponent<gameScript>().Sound(3);
			else
				GameObject.Find("gameScript").GetComponent<gameScript>().Sound(4);
		}
		if(other.gameObject.tag == "Rim"){
			GameObject.Find("gameScript").GetComponent<gameScript>().Sound(2);
		}
		if(other.gameObject.tag == "board"){
			GameObject.Find("gameScript").GetComponent<gameScript>().Sound(5);
		}
		
		if(other.gameObject.tag == "Net2" && fist_check && can){
			if(fist_check){
				score = true;
				GameObject.Find("gameScript").GetComponent<gameScript>().Sound(1);
				GameObject.Find("net").GetComponent<Animator>().SetTrigger("Score");
				GameObject.Find("scoreScript").GetComponent<scoreScript>().Score(clean, back_wall);
			}
			else
				can = false;
		}
	}
}
