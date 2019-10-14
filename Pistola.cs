using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pistola : MonoBehaviour {


	Animator anim;
	AudioSource audio;

	public Texture terra;

	public AudioClip tiro, clipIn, clipOff;

	public Transform camera;
	public int dano;
	public Transform muzzle;
	public Text pente;
	bool podeRecarregar;

	private int capacidade =12, municaoRestante, balas;
	public int municao;

	RaycastHit hit;

	void Start () {
		balas = capacidade;
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		podeRecarregar = false;
	}

	void Update () {
		pente.text = balas + " / " + municao;
		audio.pitch = Time.timeScale;

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			if (balas > 0) {
				podeRecarregar = true;
				anim.SetTrigger ("Atirando");
			}
		}

		if (Input.GetKeyDown (KeyCode.R) && balas < 12 && podeRecarregar == true) {
			if (municao > 0) {
				podeRecarregar = false;
				anim.SetTrigger ("Recarregando");
			}
		}
	}

	void Atirar(){
		if (Physics.Raycast (camera.transform.position, camera.transform.forward, out hit, 100)) {
			if (hit.transform.gameObject.tag.Equals ("Terra")) {
				Instantiate (terra, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal));
			}
			if (hit.transform.gameObject.GetComponent<Enemy> () != null) {
				hit.transform.gameObject.GetComponent<Enemy> ().Dano (dano);
			}
		}
		audio.PlayOneShot (tiro, 0.5f);
		balas--;
	}
	void somTiro(){
		audio.PlayOneShot (tiro, 0.5f);
	}
	public void FlashOn(){
		muzzle.gameObject.active = true;
	}
	public void FlashOff(){
		muzzle.gameObject.active = false;
	}
	public void ClipIn(){
		audio.PlayOneShot (clipIn, 0.7f);
	}
	public void ClipOff(){
		audio.PlayOneShot (clipOff, 0.7f);
	}
	void BalanaArma(){
		municaoRestante = capacidade - balas;
		if (municao < balas) {
			balas += municaoRestante;
			municao -= municaoRestante;
		} else {
			balas = capacidade;
			municao -= municaoRestante;
		}
		if (municao < 0) {
			municao = 0;
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Municao") {
			balas += 10;
		}
	}
}
