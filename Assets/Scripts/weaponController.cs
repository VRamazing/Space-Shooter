using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour {

	private AudioSource audioSource;
	public Transform shotSpawn;
	public GameObject shot;
	public float fireRate;
	public float delay;

	// Use this for initialization 
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, fireRate);

		
	}
	
	// Update is called once per frame
	void Fire () {
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}
}
 