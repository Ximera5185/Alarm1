using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const string PathPrefabCharacter = "Prefabs/Character";
    private const string PathPrefabHome = "Prefabs/Home";

    private void Awake()
    {
        Initializlevel();
    }

    private void Initializlevel()
    {
        InstantiatePrefabs();
    }

    private GameObject LoadPrefab(string pathPrefab)
    {
        return Resources.Load<GameObject>(pathPrefab);
    }

    private void InstantiatePrefabs()
    {
        float positionHouseX = 2f;
        float positionHouseY = -1.6f;
        float positionHouseZ = 20f;

        float rotationHouseX = 0f;
        float rotationHouseY = 90f;
        float rotationHouseZ = 0f;

        GameObject home = Instantiate(LoadPrefab(PathPrefabHome), new Vector3(positionHouseX, positionHouseY, positionHouseZ), Quaternion.Euler(rotationHouseX, rotationHouseY, rotationHouseZ));

        Instantiate(LoadPrefab(PathPrefabCharacter), transform.position, Quaternion.identity);

        home.AddComponent<House>();
    }
}