using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioSiren : MonoBehaviour
{
    private const string PlayerTag = "Player";

    private TriggerZoneHandler _triggerZoneHandler;
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _lerpVolume = 0.1f;
    private float _targetVolume;

    private enum AudioState { Stopped, Playing }

    private AudioState _currentState = AudioState.Stopped;

    private void Awake()
    {
        Initialization();
    }

    void Start()
    {
        InitializeTriggerZoneHandler();
    }

    private void Update()
    {
        UpdateVolume(ref _audioSource);

        ManageAudioPlayback();
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
        _triggerZoneHandler = FindObjectOfType<TriggerZoneHandler>();

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
            _currentState = AudioState.Playing;
        }
    }

    private void HandleObjectExited(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            _currentState = AudioState.Stopped;
        }
    }

    private void PlaySound()
    {
        _targetVolume = _maxVolume;

        if (_audioSource != null && _audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }
    }

    private void StopSound()
    {
        _targetVolume = _minVolume;

        if (_audioSource != null && _audioSource.isPlaying == true)
        {
            if (_audioSource.volume == _minVolume)
            {
                _audioSource.Stop();
            }
        }
    }

    private void ManageAudioPlayback()
    {
        if (_currentState == AudioState.Playing)
        {
            PlaySound();
        }
        else if (_currentState == AudioState.Stopped)
        {
            StopSound();
        }
    }
  
    private void UpdateVolume(ref AudioSource audioSource)
    {
      audioSource.volume = Mathf.MoveTowards(audioSource.volume, _targetVolume, _lerpVolume * Time.deltaTime);
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