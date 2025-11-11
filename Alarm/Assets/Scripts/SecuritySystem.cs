using UnityEngine;

[RequireComponent(typeof(AudioSiren),typeof(TriggerZoneHandler))]
public class SecuritySystem : MonoBehaviour
{
    private AudioSiren _audioSiren;
    private TriggerZoneHandler _triggerZoneHandler;
    private void Start()
    {
        _audioSiren = GetComponent<AudioSiren>();
        _triggerZoneHandler = GetComponent<TriggerZoneHandler>();

        InitializeTriggerZoneHandler();
    }
    private void InitializeTriggerZoneHandler()
    {

        if (_triggerZoneHandler != null)
        {
            _triggerZoneHandler.OnTriggerEntered += HandleObjectEntered;
            _triggerZoneHandler.OnTriggerExited += HandleObjectExited;
        }
    }

    private void HandleObjectEntered(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            
        }
    }

    private void HandleObjectExited(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
          
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
