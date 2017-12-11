using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour {
	void Start(){
		Cursor.visible = false;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
