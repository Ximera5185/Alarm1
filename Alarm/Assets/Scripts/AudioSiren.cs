using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioSiren : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField] private TriggerZoneHandler _triggerZoneHandler;

    private AudioSource _audioSource;
    private AudioClip _audioClip;
    private Coroutine _coroutine;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _lerpVolume = 0.1f;
    private float _targetVolume;

    private AudioState _currentState = AudioState.Stopped;
    private enum AudioState { Stopped, Playing }

    private void Awake()
    {
        Initialization();
    }

    void Start()
    {
        InitializeTriggerZoneHandler();
    }

    private void Initialization()
    {
        const string PathAudioFile = "Sound/Sirena";

        _audioSource = GetComponent<AudioSource>();

        _audioClip = Resources.Load<AudioClip>(PathAudioFile);

        if (_audioClip != null)
        {
            _audioSource.clip = _audioClip;

            _audioSource.volume = _minVolume;
        }
    }

    private void InitializeTriggerZoneHandler()
    {
        _triggerZoneHandler = GetComponent<TriggerZoneHandler>();

        if (_triggerZoneHandler != null)
        {
            _triggerZoneHandler.OnTriggerEntered += HandleObjectEntered;

            _triggerZoneHandler.OnTriggerExited += HandleObjectExited;
        }
    }

    private void HandleObjectEntered(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            PlaySound();
        }
    }

    private void HandleObjectExited(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            StopSound();
        }
    }

    private void PlaySound()
    {
        _targetVolume = _maxVolume;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

            _coroutine = null;
        }

        _audioSource.Play();

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(UpdateVolume(_targetVolume));
        }
    }

    private void StopSound()
    {
        _targetVolume = _minVolume;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

            _coroutine = null;

            _coroutine = StartCoroutine(UpdateVolume(_targetVolume));
        }
    }

    private IEnumerator UpdateVolume(float targetVolume)
    {
        while (!Mathf.Approximately(_audioSource.volume, targetVolume))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _lerpVolume * Time.deltaTime);
            yield return null;

            if (_audioSource.volume == _minVolume)
            {
                _audioSource.Stop();
            }
        }
    }

    private void OnDestroy()
    {
        if (_triggerZoneHandler != null)
        {
            _triggerZoneHandler.OnTriggerEntered -= HandleObjectEntered;

            _triggerZoneHandler.OnTriggerExited -= HandleObjectExited;
        }
    }
}