﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Attacker))]

public class Fox : MonoBehaviour {

	private Animator anim;
	private Attacker attacker;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		attacker = GetComponent<Attacker>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {

		GameObject obj = collider.gameObject;

		if (!obj.GetComponent<Defender>()) {
			return;	
		}

		if(obj.GetComponent<Stone>()) {
			anim.SetTrigger("Jump Trigger");
		} else {
			anim.SetBool("isAttacking", true);
			attacker.Attack(obj);
		}
	}
}
