using System.Collections;
using System.Collections.Generic;
using Game.SharedGameSystems.SoundSystem.Scripts.Data;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Game.SharedGameSystems.SoundSystem.Scripts.Controllers
{
	public class SoundSystemManager : SingletonMonoBehaviour<SoundSystemManager>
	{
		private float RandomizedPitchValue => 1f + Random.Range(-0.2f, 0.2f);

		private const int _oneShotAudioSourcePoolCount = 4;

		private readonly Dictionary<string, (float, Coroutine)> _higherPitchValueCountdowns = new();

		private AudioSource[] _oneShotSoundEffectAudioSources;
		private int _currentAudioSourceIndex = -1;

		public void Initialize()
		{
			SetupAudioSources();
		}

		#region Sound Effects
		private void SetupAudioSources()
		{
			_oneShotSoundEffectAudioSources = new AudioSource[_oneShotAudioSourcePoolCount];

			for (int i = 0; i < _oneShotAudioSourcePoolCount; i++)
			{
				var newAudioSource = gameObject.AddComponent<AudioSource>();
				newAudioSource.playOnAwake = false;
				_oneShotSoundEffectAudioSources[i] = newAudioSource;
			}
		}

		public void PlaySoundEffectOnce(AudioClip clip, SoundEffectPitchMode pitchMode = SoundEffectPitchMode.Default, string higherPitchKey = null, float pitchValue = 1f)
		{
			if (clip == null) return;

			var audioSource = GetNextSoundEffectAudioSource();

			audioSource.pitch = pitchMode switch
								{
									SoundEffectPitchMode.Default        => 1f,
									SoundEffectPitchMode.Randomized     => RandomizedPitchValue,
									SoundEffectPitchMode.GetHigherPitch => GetHigherPitchValueFor(higherPitchKey, pitchValue),
									_                                   => 1f
								};
			audioSource.PlayOneShot(clip);
		}

		private float GetHigherPitchValueFor(string higherPitchKey, float pitchDifference)
		{
			float pitchValue = 1f;

			if (_higherPitchValueCountdowns.TryGetValue(higherPitchKey, out (float, Coroutine) pitchData))
			{
				pitchValue = Mathf.Clamp(pitchData.Item1 + pitchDifference, 1f, 1.75f);
				StopCoroutine(pitchData.Item2);
				pitchData = (pitchValue, StartCoroutine(PitchResetCoroutine(higherPitchKey)));
				_higherPitchValueCountdowns[higherPitchKey] = pitchData;
			}
			else
			{
				_higherPitchValueCountdowns.Add(higherPitchKey, (pitchValue, StartCoroutine(PitchResetCoroutine(higherPitchKey))));
			}

			return pitchValue;
		}

		private IEnumerator PitchResetCoroutine(string higherPitchKey)
		{
			yield return new WaitForSeconds(1f);
			_higherPitchValueCountdowns.Remove(higherPitchKey);
		}

		private AudioSource GetNextSoundEffectAudioSource()
		{
			_currentAudioSourceIndex = (_currentAudioSourceIndex + 1) % _oneShotSoundEffectAudioSources.Length;
			return _oneShotSoundEffectAudioSources[_currentAudioSourceIndex];
		}
		#endregion

		private void OnDestroy()
		{
			foreach (var iteratingCountdown in _higherPitchValueCountdowns)
			{
				StopCoroutine(iteratingCountdown.Value.Item2);
			}
		}
	}
}
