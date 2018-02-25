using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	public GameObject projectile, gun;

	private GameObject projectileParent;
	private Animator animator;
	private Spawner myLaneSpawner;

	private void Fire() {
		GameObject newProjectile = Instantiate(projectile) as GameObject;
		newProjectile.transform.parent = projectileParent.transform;
		newProjectile.transform.position = gun.transform.position;
	}

	// Use this for initialization
	void Start () {
		animator = GameObject.FindObjectOfType<Animator>();

		// Creates a parent if necessary
		projectileParent = GameObject.Find ("Projectiles");
		if(!projectileParent) {
			projectileParent = new GameObject("Projectiles");
		}

		SetMyLaneSpawner();
	}
	
	// Update is called once per frame
	void Update () {
		if(IsAttackerAheadInLane()) {
			animator.SetBool ("isAttacking", true);
		} else {
			animator.SetBool ("isAttacking", false);
		}
	}

	// Look through all spawners, and set myLaneSpawner if found
	void SetMyLaneSpawner () {
		Spawner[] spawnerArray = GameObject.FindObjectsOfType<Spawner>();

		foreach(Spawner spawner in spawnerArray) {
			if(spawner.transform.position.y == transform.position.y) {
				myLaneSpawner = spawner;
				return;
			}
		}
	}

	bool IsAttackerAheadInLane() {
		// Exit if no attackers in lane
		if(myLaneSpawner.transform.childCount <= 0) {
			return false;
		}

		// If there are attackers ahead
		foreach(Transform attackers in myLaneSpawner.transform) {
			if(attackers.transform.position.x > transform.position.x) {
				return true;
			}
		}

		// Attackers in lane but behind us
		return false;
	}
}
