using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChange;

	private AudioSource audioSource;
	private int activeSceneIndex;

	void OnEnable() {
		SceneManager.activeSceneChanged += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.activeSceneChanged -= OnSceneLoaded;
	}

	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	void Start() {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
	}
	
	void OnSceneLoaded(Scene lastScene, Scene newScene) {
		AudioClip thisLevelMusic = levelMusicChange[newScene.buildIndex];

		if(!thisLevelMusic) return;

			audioSource.clip = thisLevelMusic;
			audioSource.Play();
	}

	public void SetVolume (float volume) {
		audioSource.volume = volume;
	}
}
