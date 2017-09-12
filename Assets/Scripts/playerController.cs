using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class playerController : NetworkBehaviour 
{
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	void Update()
	{
		if (!isLocalPlayer) 
		{
			GetComponent<MeshRenderer>().material.color = Color.red;
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space) ) 
		{
			CmdFire();	
		}

		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate(0,0,z);

	}

	[Command]
	void CmdFire()
	{
		//Create bullet for bullet prefab
		var bullet = (GameObject)Instantiate (
			             bulletPrefab,
			             bulletSpawn.position,
			             bulletSpawn.rotation);
		//bulletvelocity
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		//Spawn the bullet on all clients
		NetworkServer.Spawn(bullet);

		//destroy bullet after 3.5 seconds
		Destroy(bullet, 3.5f);
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
		
}