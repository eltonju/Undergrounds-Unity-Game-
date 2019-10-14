using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private int vida = 100;
	public Text vidaT;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		vidaT.text = "Vida " + vida;
	}
	public void damageP(int damage){
		vida -= damage;
		Debug.Log ("dano");
	}
	public void recVida(int rec){
	 	vida += rec;
	}
}
