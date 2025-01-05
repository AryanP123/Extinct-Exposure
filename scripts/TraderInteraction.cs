using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; // Added for IEnumerator

public class TraderInteraction : MonoBehaviour
{
    public GameObject interactionMessage; // UI Text for "Press 'F' to Trade..."
    public GameObject notEnoughBonesMessage; // UI Text for "Don't try to scam me!"
    public BatteryManager batteryManager; // Reference to BatteryManager
    public CollectibleSpawner collectibleSpawner; // Reference to the spawner to access pickedUpCount

    private bool playerInRange = false;

    void Start()
    {
        interactionMessage.SetActive(false);
        notEnoughBonesMessage.SetActive(false);
    }

    void Update()
    {
        // Check if player is in range and presses 'F'
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            TryTrade();
        }
    }

    private void TryTrade()
    {
        if (collectibleSpawner != null && collectibleSpawner.GetPickedUpCount() >= 3)
        {
            // Deduct 3 bones and add battery power
            collectibleSpawner.pickedUpCount -= 3;
            collectibleSpawner.UpdateCollectibleDisplay(); // Update the UI text
            
            batteryManager.InsertBattery(20); // Add 20 battery power
            Debug.Log("Trade Successful! Battery Recharged.");

            // Hide interaction message
            interactionMessage.SetActive(false);
        }
        else
        {
            // Show "Not enough bones" message temporarily
            StartCoroutine(ShowNotEnoughBonesMessage());
        }
    }

    private IEnumerator ShowNotEnoughBonesMessage()
    {
        notEnoughBonesMessage.SetActive(true);
        yield return new WaitForSeconds(2f);
        notEnoughBonesMessage.SetActive(false);
    }

    // Detect when player enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionMessage.SetActive(true); // Show "Press F" message
        }
    }

    // Detect when player leaves the trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionMessage.SetActive(false); // Hide messages
        }
    }
}
