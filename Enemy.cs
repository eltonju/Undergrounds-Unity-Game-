using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int Vida = 100;
	public float patrulhaTempo;
	private WaitForSeconds tempo;
	public Transform[] waypoints;
	private int index;
	private Animator anim;
	private NavMeshAgent agent;
	public GameObject muzzle;
	public Transform pivot;
	public int ataque;
	public GameObject morto, manager;

	AudioSource audio;
	public AudioClip tiro;

	RaycastHit hit;

	[SerializeField]
	public GameObject player;
	private bool perseguir = false, atirando = false;
	[SerializeField]
	private float dist = 5;
	[SerializeField]
	private float distAtaque;

	void Start () {
		player = GameObject.Find ("Jogador");
		manager = GameObject.Find ("GameManager");
		tempo = new WaitForSeconds (patrulhaTempo);
		agent = GetComponent<NavMeshAgent> ();
		index = Random.Range (0, waypoints.Length);
		anim = GetComponent<Animator> ();
		StartCoroutine (ChamaPatrulha());
		audio = GetComponent<AudioSource> ();

		distAtaque = agent.stoppingDistance;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("Move", agent.velocity.sqrMagnitude, 0.06f, Time.deltaTime);

		if (Vida <= 0) {
			morte ();
		}

		Perseguir ();

		if (player != null && Vector3.Distance (transform.position, player.transform.position) < distAtaque) {
			anim.SetTrigger ("Atirando");
			Quaternion rotaion = Quaternion.LookRotation (player.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotaion, Time.deltaTime * 6.0f);
		}
	}

	IEnumerator ChamaPatrulha(){
		while (true) {
			yield return tempo;
			patrol ();
		}
	}
	void patrol(){
		index = index == waypoints.Length - 1 ? 0 : index + 1;
		agent.destination = waypoints[index].position;
	}
	void Perseguir(){
		if (player != null && Vector3.Distance (transform.position, player.transform.position) < dist && !perseguir) 
		{
			perseguir = true;
		}else if(Vector3.Distance(transform.position, player.transform.position) > dist){
			perseguir = false;
		}

		if (perseguir) 
		{
			agent.destination = player.transform.position;
		}
	}
	void Atirar(){
		atirando = true;
		if (Physics.Raycast (pivot.transform.localPosition, pivot.transform.forward, out hit, 100)) {
			if (hit.transform.gameObject.GetComponent<Player> () != null) {
				//if(Random.Range(1, 10) == 1)
				hit.transform.gameObject.GetComponent<Player> ().damageP(ataque);
			}
		}
	}
	void flash(){
		muzzle.gameObject.active = true;
	}

	public void Dano(int damage){
		Vida -= damage;
		anim.SetTrigger ("Dano");
	}

	void morte(){
		manager.GetComponent<GameManager> ().InsInimigo();
		Instantiate (morto, transform.position, transform.rotation);
		Destroy (gameObject);
	}
	public void FlashOn(){
		audio.PlayOneShot (tiro, 0.5f);
		muzzle.gameObject.active = true;
	}
	public void FlashOff(){
		muzzle.gameObject.active = false;
	}
}
