using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
public class BatteryManager : MonoBehaviour
{
    public Image batteryBar;
    public float batteryAmount = 100f;
    private string folderPath;
    private string filePath;
    private string currentDinosaurName;
    public WinConditionManager winConditionManager;
    private CaptureSuccess uiManager;
    private string[] dinosaursToPhotograph = { "Pachycephalasaurus", "Stegasaurus_20K", "PBR_Velociraptor_Blue" };

    void Start()
    {
        uiManager = FindObjectOfType<CaptureSuccess>();
        folderPath = Application.dataPath + "/Screenshots";
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && batteryAmount > 0)
        {
            TakeCapture(10);
        }
    }

    private bool IsDinosaurPhoto(string fileName)
    {
        return dinosaursToPhotograph.Any(dino => fileName.Contains(dino));
    }
    public void TakeCapture(float juice)
    {
        winConditionManager.CheckForDinosaur();
        currentDinosaurName = winConditionManager.GetCurrentDinosaurName();

        string fileName;
        if(currentDinosaurName != null && IsDinosaurPhoto(currentDinosaurName))
        {
            uiManager.ShowFeedback(true);
            fileName = $"/screenshot-{currentDinosaurName}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png";
        }
        else
        {
            uiManager.ShowFeedback(false);
            fileName = $"/screenshot-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.png";
        }
        
        filePath = folderPath + fileName;
        ScreenCapture.CaptureScreenshot(filePath);
        Debug.Log("Screenshot saved to: " + filePath);

        batteryAmount -= juice;
        batteryBar.fillAmount = batteryAmount / 100f;
        StartCoroutine(FlashBlip());
    }

    public void InsertBattery(float juice)
    {
        batteryAmount += juice; // Increase the battery amount
        batteryAmount = Mathf.Clamp(batteryAmount, 0, 100); // Ensure the battery does not exceed 100%
        batteryBar.fillAmount = batteryAmount / 100f; // Update the battery display
    }

    private IEnumerator FlashBlip()
    {
        yield return new WaitForSeconds(0.4f);

        // Create a white flash effect
        GameObject blip = new GameObject("Blip");
        blip.AddComponent<CanvasRenderer>();
        Image blipImage = blip.AddComponent<Image>();
        blipImage.color = Color.white;

        // Find the Canvas in the scene
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            blip.transform.SetParent(canvas.transform, false);
        }
        else
        {
            Debug.LogError("Canvas not found in the scene.");
        }

        blip.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        // Flash the blip on screen
        blipImage.CrossFadeAlpha(1.0f, 0.1f, false);
        yield return new WaitForSeconds(0.1f);
        blipImage.CrossFadeAlpha(0.0f, 0.1f, false);
        yield return new WaitForSeconds(0.1f);

        // Destroy the blip after the effect
        Destroy(blip);
    }
}