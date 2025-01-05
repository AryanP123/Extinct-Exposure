using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{
    // Reference to the spawner to update the count
    private CollectibleSpawner spawner;

    void Start()
    {
        // Find the spawner in the scene (ensure only one spawner exists)
        spawner = FindObjectOfType<CollectibleSpawner>();
    }

    // Detect collision with the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            // Notify the spawner to update the count
            spawner.DecrementCollectibleCount();

            // Destroy this collectible
            Destroy(gameObject);
        }
    }
}
