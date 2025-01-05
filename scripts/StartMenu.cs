using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private FPSController playerController;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject howToMenuUI;
    [SerializeField] private GameObject creditsMenuUI;
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] public Text startButton;
    // [SerializeField] public Text settingsButton;
    [SerializeField] public Text howToButton;
    [SerializeField] public Text creditsButton;

    [Header("Button Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;
    [SerializeField] private Color clickColor = Color.green;

    // public Vector3 startingPosition;

    void Start() {
        startButton.color = defaultColor;
        howToButton.color = defaultColor;
        creditsButton.color = defaultColor;
        // startingPosition = transform.position;

        ShowMenu();
        AddEventTriggers(startButton, OnStartPressed);
        AddEventTriggers(howToButton, OnHowToPressed);
        AddEventTriggers(creditsButton, OnCreditsPressed);
    }

    /* public void ResetPosition()
    {
        transform.position = startingPosition; // Reset the player's position to the initial one
    } */

    private void ClearScreenshotsFolder()
    {
        string screenshotsPath = Application.dataPath + "/Screenshots";
        if (Directory.Exists(screenshotsPath))
        {
            DirectoryInfo di = new DirectoryInfo(screenshotsPath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            Debug.Log("Screenshots folder cleared!");
        }
    }

    public void OnStartPressed()
    {
        // Hide the start menu and start the game
        ClearScreenshotsFolder();
        startMenuUI.SetActive(false);
        FPSController.isGameActive = true;
        playerController.LockCursor(true);
        Time.timeScale = 1f;

        // playerController.ResetPosition();
    }

    public void OnHowToPressed()
    {
        Debug.Log("How-To button pressed!");
        // Add your how-to logic here
        startMenuUI.SetActive(false);
        howToMenuUI.SetActive(true);
        creditsMenuUI.SetActive(false);
    }

    public void OnCreditsPressed()
    {
        Debug.Log("Credits button pressed!");
        creditsMenuUI.SetActive(true);
        startMenuUI.SetActive(false);
        howToMenuUI.SetActive(false);
        // Add your credits logic here
    }

    public void ShowMenu()
    {
        // Show the start menu and disable gameplay
        startMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        FPSController.isGameActive = false;
        playerController.LockCursor(false);
        Time.timeScale = 0f;
    }

    private void AddEventTriggers(Text textObject, UnityEngine.Events.UnityAction action)
    {
        EventTrigger trigger = textObject.gameObject.AddComponent<EventTrigger>();

        // Hover enter event
        EventTrigger.Entry hoverEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        hoverEnter.callback.AddListener((data) => { Debug.Log($"{textObject.name} hovered."); textObject.color = hoverColor; });
        trigger.triggers.Add(hoverEnter);

        // Hover exit event
        EventTrigger.Entry hoverExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        hoverExit.callback.AddListener((data) => { Debug.Log($"{textObject.name} hover exited."); textObject.color = defaultColor; });
        trigger.triggers.Add(hoverExit);

        // Click event
        EventTrigger.Entry click = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        click.callback.AddListener((data) => { Debug.Log($"{textObject.name} clicked."); action.Invoke(); });
        trigger.triggers.Add(click);
    }
}
