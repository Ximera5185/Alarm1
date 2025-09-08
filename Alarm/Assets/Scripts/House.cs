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
        Vector3 triggerZonePosition = new Vector3(9, 2, -2);

        Vector3 triggerZoneScale = new Vector3(9, 1, 8);

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