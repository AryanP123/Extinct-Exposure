using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;
using System.Linq;

public class EncyclopediaMenu : MonoBehaviour
{
    [SerializeField] private GameObject encyclopediaUI;
    [SerializeField] private FPSController fpsController;
    [SerializeField] private GameObject imageContainer1;
    [SerializeField] private GameObject imagePrefab1;
    [SerializeField] private GameObject imageContainer2;
    [SerializeField] private GameObject imagePrefab2;
    [SerializeField] private GameObject imageContainer3;
    [SerializeField] private GameObject imagePrefab3;
    [SerializeField] private GameObject description1;
    [SerializeField] private GameObject description2;
    [SerializeField] private GameObject description3;
    [SerializeField] private WinScreenManager winScreenManager;
    private bool[] completedPages;
    public List<string> dinosaursToPhotograph;
    private string screenshotsFolderPath;
    public bool isEncyclopediaOpen = false;
    private int currentCanvasIndex = 0;
    private GameObject[] imageContainers;
    private GameObject[] descriptions;

    void Awake()
    {
        imageContainers = new GameObject[] { imageContainer1, imageContainer2, imageContainer3 };
        descriptions = new GameObject[] { description1, description2, description3 };
        completedPages = new bool[3];
    }

    void Start()
    {
        screenshotsFolderPath = Application.dataPath + "/Screenshots";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Debug.Log("E key pressed!");
            if (encyclopediaUI.activeSelf)
            {
                CloseEncyclopedia();
            }
            else
            {
                OpenEncyclopedia();
            }
        }

        if (isEncyclopediaOpen)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwitchCanvas(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwitchCanvas(1);
            }
        }
    }
    private void CheckCompletion()
    {
        if (winScreenManager == null)
        {
            Debug.LogError("WinScreenManager reference missing!");
            return;
        }

        bool allComplete = true;
        for (int i = 0; i < completedPages.Length; i++)
        {
            if (!completedPages[i])
            {
                allComplete = false;
                break;
            }
        }
        
        if (allComplete)
        {
            CloseEncyclopedia();
            winScreenManager.ShowWinScreen();
        }
    }

    public void OpenEncyclopedia()
    {
        // First activate UI
        encyclopediaUI.SetActive(true);
        
        // Load screenshots before setting active canvas
        LoadScreenshots();
        
        // Then set active canvas and descriptions
        SetActiveCanvas(currentCanvasIndex);
        
        // Finally update game state
        FPSController.isGameActive = false;
        fpsController.LockCursor(false);
        Time.timeScale = 0f;
        isEncyclopediaOpen = true;
    }

    public void CloseEncyclopedia()
    {
        encyclopediaUI.SetActive(false);
        FPSController.isGameActive = true;
        fpsController.LockCursor(true);
        Time.timeScale = 1f;
        isEncyclopediaOpen = false;
    }

    private void SwitchCanvas(int direction)
    {
        currentCanvasIndex = (currentCanvasIndex + direction + imageContainers.Length) % imageContainers.Length;
        SetActiveCanvas(currentCanvasIndex);
    }

    private void SetActiveCanvas(int index)
    {
        for (int i = 0; i < imageContainers.Length; i++)
        {
            imageContainers[i].SetActive(i == index);
            descriptions[i].SetActive(i == index); // Also toggle description visibility
        }
    }

    private void LoadScreenshots()
    {
        if (!Directory.Exists(screenshotsFolderPath)) return;
        
        Debug.Log("Loading screenshots...");
        if (Directory.Exists(screenshotsFolderPath))
        {
            foreach (string dinosaurName in dinosaursToPhotograph)
            {
                string[] files = Directory.GetFiles(screenshotsFolderPath, $"*{dinosaurName}*.png");

                if (files.Length > 0)
                {
                    // Get the latest file by creation time
                    string latestFile = files.OrderByDescending(f => File.GetCreationTime(f)).First();
                    Debug.Log("Latest file for " + dinosaurName + ": " + latestFile);
                    byte[] fileData = File.ReadAllBytes(latestFile);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    GameObject newImage;
                    GameObject description = null;
                    if (dinosaurName == "Pachycephalasaurus")
                    {
                        completedPages[0] = true;
                        newImage = Instantiate(imagePrefab1, imageContainer1.transform);
                        description = description1;
                    }
                    else if (dinosaurName == "Stegasaurus_20K")
                    {
                        completedPages[1] = true;
                        newImage = Instantiate(imagePrefab2, imageContainer2.transform);
                        description = description2;
                    }
                    else
                    {
                        completedPages[2] = true;
                        newImage = Instantiate(imagePrefab3, imageContainer3.transform);
                        description = description3;
                    }
                    newImage.GetComponent<Image>().sprite = sprite;
                    if (description != null)
                    {
                        description.SetActive(true);
                    }
                    CheckCompletion();
                }
            }
        }
    }
}