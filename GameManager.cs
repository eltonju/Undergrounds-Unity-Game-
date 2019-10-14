using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	bool pausada = false;
	bool bulletTime;

	public GameObject menu, enemy;
	public GameObject[] wayPoints;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && pausada == false) {
			PausarJogo ();
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && pausada == true) {
			ContinuarJogo ();
		}
		if (Input.GetKeyDown (KeyCode.E) && bulletTime == false) {
			BulletTimeOn ();
		} else if(Input.GetKeyDown (KeyCode.E) && bulletTime == true){
			BulletTimeOff ();
		}
	}

	void PausarJogo(){
		pausada = true;
		menu.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Time.timeScale = 0.0f;
	}

	void ContinuarJogo(){
		pausada = false;
		menu.SetActive (false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1.0f;
	}

	void BulletTimeOn(){
		Time.timeScale = 0.1f;
		bulletTime = true;
	}
	void BulletTimeOff(){
		Time.timeScale = 1.0f;
		bulletTime = false;
	}
	public void InsInimigo(){
		int i = Random.Range (0, wayPoints.Length);
		Instantiate (enemy, wayPoints [i].transform.position, wayPoints [i].transform.rotation);
	}
}
