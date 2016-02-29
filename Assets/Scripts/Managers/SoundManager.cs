using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	private AudioSource efxSource;
	private AudioSource playerSource;
	private AudioSource otherSource;
	private AudioSource oldmanSource;

	public static SoundManager instance = null;
	public float lowPitchRange = .95f;				// The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;			// The highest a sound effect will be randomly pitched.

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		AudioSource[] sources = GetComponents<AudioSource>();
		efxSource = sources[0];
		playerSource = sources[1];
		otherSource = sources[2];
		oldmanSource = sources[3];
	}
	
	/// <summary>
	/// Used to play single sound clips.
	/// </summary>
	/// <param name="clip">Clip.</param>
	public void PlaySingle(AudioClip clip)
	{
		if (efxSource.isPlaying)
		{
			return;
		}

		efxSource.clip = clip;
		efxSource.Play();
	}

	public void PlayOldman()
	{
		oldmanSource.Play();
	}

	/// <summary>
	/// Used to play players specific single sound clips.
	/// </summary>
	/// <param name="clip">Clip.</param>
	public void PlayPlayersSingle(AudioClip clip)
	{
		if (playerSource.isPlaying)
		{
			return;
		}

		playerSource.clip = clip;
		playerSource.Play();
	}

	/// <summary>
	/// Used to play other single sound clips (like environmental)
	/// </summary>
	/// <param name="clip">Clip.</param>
	public void PlayOtherSingle(AudioClip clip)
	{
		if (otherSource.isPlaying)
		{
			return;
		}

		otherSource.clip = clip;
		otherSource.Play();
	}
	
	/// <summary>
	/// Chooses randomly between various audio clips and slightly changes their pitch.
	/// </summary>
	/// <param name="clips">Clips.</param>
	public void RandomizeSfx (params AudioClip[] clips)
	{
		if (efxSource.isPlaying)
		{
			return;
		}

		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips[randomIndex];

		efxSource.Play();
	}
}