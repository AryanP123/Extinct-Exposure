using UnityEngine;
using UnityEngine.UI; // To display text on the UI
using TMPro;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int maxCollectibles = 100;
    public float spawnInterval = 0.01f;

    public float minX = 40, maxX = 440, minY = 20, maxY = 30, minZ = 40, maxZ = 440;

    private int currentCollectibles = 0;
    private float nextSpawnTime;

    // Add UI Text to display pickup count
    public int pickedUpCount = 0; 
    public TextMeshProUGUI collectibleText; // Drag a UI Text element here in the inspector

    void Start()
    {
        UpdateCollectibleDisplay();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentCollectibles < maxCollectibles)
        {
            SpawnCollectible();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnCollectible()
    {
        if (currentCollectibles < maxCollectibles)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float z = Random.Range(minZ, maxZ);

            Vector3 spawnPosition = new Vector3(x, y, z);

            GameObject collectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
            currentCollectibles++;
            Debug.Log("Current Collectibles: " + currentCollectibles);
        }
    }

    public void DecrementCollectibleCount()
    {
        currentCollectibles--;
        pickedUpCount++;
        UpdateCollectibleDisplay();
    }

    public void UpdateCollectibleDisplay()
    {
        if (collectibleText != null)
        {
            collectibleText.text = "Bones Collected: " + pickedUpCount;
        }
    }

    public int GetPickedUpCount()
    {
        return pickedUpCount;
    }

}
