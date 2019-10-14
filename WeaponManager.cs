using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

	public GameObject[] weapons;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha3) && weapons [0].active == false) {
			weapons [1].SetActive (false);
			weapons [0].SetActive (true);
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && weapons [0].active == true) {
			weapons [0].SetActive (false);
			weapons [1].SetActive (true);
		} else if(Input.GetKeyDown (KeyCode.Alpha3) && weapons [0].active == false && weapons [1].active == false){
			weapons [0].SetActive (true);
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			weapons [0].SetActive (false);
			weapons [1].SetActive (false);
		}
	}
}
