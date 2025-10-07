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
        Initialization(_minVolume);
    }

    void Start()
    {
        InitializeTriggerZoneHandler();
    }

    private void HandleObjectEntered(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            PlaySound(_maxVolume);
        }
    }
    private void HandleObjectExited(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            StopSound(_minVolume);
        }
    }

    private void Initialization(float targetVolume)
    {
        const string PathAudioFile = "Sound/Sirena";

        _audioSource = GetComponent<AudioSource>();

        _audioClip = Resources.Load<AudioClip>(PathAudioFile);

        if (_audioClip != null)
        {
            _audioSource.clip = _audioClip;

            _audioSource.volume = targetVolume;
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

    private void PlaySound(float targetVolume)
    {
        SetTargetVolume(targetVolume);

        StopCoroutine();

        _audioSource.Play();

        StartCoroutine();
    }

    private void StopSound(float targetVolume)
    {
        SetTargetVolume(targetVolume);

        StopCoroutine();

        StartCoroutine();
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);

            _coroutine = null;
        }
    }
    private void StartCoroutine()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(UpdateVolume(_targetVolume));
        }
    }

    private void SetTargetVolume(float targetVolume)
    {
        _targetVolume = targetVolume;
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