using UnityEngine;
using System.Collections;

public class Municao : MonoBehaviour {


	void Start () {
	
	}

	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Pistola") {
			int balas = GetComponent<Pistola> ().municao;
			balas += 10;
			int balasM4 = GetComponent<M4Script> ().municao;
			balasM4 += 20;
		}
	}
}
