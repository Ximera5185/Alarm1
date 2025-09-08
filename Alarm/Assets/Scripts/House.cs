using UnityEngine;

public class House : MonoBehaviour
{
    private const string ObjectNameAlarm = "Alarm";
    private const string ObjectNameTriggerZone = "TriggerZone";

    void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        float triggerZonePositionX = 9f;
        float triggerZonePositionY = 2f;
        float triggerZonePositionZ = -2f;

        float triggerZoneScaleX = 9f;
        float triggerZoneScaleY = 1f;
        float triggerZoneScaleZ = 8f;

        Vector3 triggerZonePosition = new Vector3(triggerZonePositionX, triggerZonePositionY, triggerZonePositionZ);

        Vector3 triggerZoneScale = new Vector3(triggerZoneScaleX, triggerZoneScaleY, triggerZoneScaleZ);

        CreateObject(ObjectNameAlarm, typeof(AudioSiren), Vector3.zero, Quaternion.identity, Vector3.zero);

        CreateObject(ObjectNameTriggerZone, typeof(TriggerZoneHandler), triggerZonePosition, Quaternion.identity, triggerZoneScale);
    }

    private void CreateObject(string objectName, System.Type componentType, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject newObject = new (objectName);

        newObject.transform.position = transform.TransformPoint(position);

        newObject.transform.rotation = transform.rotation;

        newObject.transform.localScale = scale;

        newObject.transform.SetParent(transform);

        newObject.AddComponent(componentType);
    }
}