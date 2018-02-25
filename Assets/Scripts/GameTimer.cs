using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	private Slider slider;
	private AudioSource audioSource;
	private bool isEndOfLevel = false;
	private LevelManager levelManager;
	private GameObject winLabel;

	public float levelSeconds = 100;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider>();
		audioSource = GetComponent<AudioSource>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		winLabel = GameObject.Find("WinText");
		winLabel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = Time.timeSinceLevelLoad / levelSeconds;

		if(Time.timeSinceLevelLoad >= levelSeconds && !isEndOfLevel) {
			DestroyAllTaggedObjects();
			audioSource.Play();
			winLabel.SetActive(true);
			Invoke ("LoadNextLevel", audioSource.clip.length);
			isEndOfLevel = true;
		}
	}

	// Destroys all objects with DestroyOnWin tag
	void DestroyAllTaggedObjects () {
		GameObject[] objArray = GameObject.FindGameObjectsWithTag("DestroyOnWin");
		foreach (GameObject obj in objArray) {
			Destroy(obj);
		}
	} 

	void LoadNextLevel () {
		levelManager.LoadNextLevel();
	}
}
