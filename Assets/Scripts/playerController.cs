using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour 
{
	private Rigidbody rb;
	public float speed;
	public int ammo;


	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		ammo = 0;

	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.CompareTag("Ammo"))
		{
			other.gameObject.SetActive(false);
			ammo = ammo + 7;
		}

	}


}