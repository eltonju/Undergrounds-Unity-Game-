using UnityEngine;
using System.Collections;

public class GameManagerCinema : MonoBehaviour {
	
	public GameObject[] atores;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ApaAtor(int i){
		atores [i].SetActive (true);
	}

	public void DesapaAtor(int i){
		atores [i].SetActive (false);
	}
}
