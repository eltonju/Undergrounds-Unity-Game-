using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class M4Script : MonoBehaviour {


	Animator anim;
	AudioSource audio;

	public Texture terra;

	public AudioClip tiro, clipIn, clipOff;

	public Transform camera;
	public int dano;
	public Transform muzzle;
	public Text pente;
	bool podeRecarregar;

	private int capacidade =30, municaoRestante, balas;
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

		if (Input.GetKey (KeyCode.Mouse0)) {
			if (balas > 0) {
				podeRecarregar = true;
				anim.SetTrigger ("Atirando");
			}
		}

		if (Input.GetKeyDown (KeyCode.R) && balas < capacidade && podeRecarregar == true) {
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
		audio.PlayOneShot (tiro, 0.1f);
		balas--;
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
}
