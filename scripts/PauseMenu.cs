using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject areYouSureUI;
    [SerializeField] private FPSController playerController;


    [SerializeField] public Text resumeButton;
    [SerializeField] public Text mainMenuButton;

    [Header("Button Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;
    [SerializeField] private Color clickColor = Color.green;

    public bool isPaused = false;

    void Start() {
        resumeButton.color = defaultColor;
        mainMenuButton.color = defaultColor;

        AddEventTriggers(resumeButton, ResumeGame);
        AddEventTriggers(mainMenuButton, BackToMainMenu);
    }

    void Update()
    {
        // Listen for the ESC key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed!");

            // Toggle pause state
            /* if (isPaused) {
                ResumeGame();
            }
            else { */
                PauseGame();
            //}
       }
    }

    public void PauseGame()
    {
        // Enable the pause menu and stop time
        pauseMenuUI.SetActive(true);
        FPSController.isGameActive = false;
        playerController.LockCursor(false);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Disable the pause menu and resume time
        pauseMenuUI.SetActive(false);
        FPSController.isGameActive = true;
        playerController.LockCursor(true);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void BackToMainMenu()
    {   
        // Reset player position to the starting point
        areYouSureUI.SetActive(true);
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