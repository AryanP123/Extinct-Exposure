using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreenUI;
    [SerializeField] private FPSController playerController;
    [SerializeField] public Text resumeButton;
    [SerializeField] public Text mainMenuButton;

    [Header("Button Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;
    [SerializeField] private Color clickColor = Color.green;

    public bool isWinScreenActive = false;

    void Start()
    {
        resumeButton.color = defaultColor;
        mainMenuButton.color = defaultColor;

        AddEventTriggers(resumeButton, Resume);
        AddEventTriggers(mainMenuButton, LoadMainMenu);
    }

    public void ShowWinScreen()
    {
        winScreenUI.SetActive(true);
        Time.timeScale = 0f;
        FPSController.isGameActive = false;
        playerController.LockCursor(false);
    }

    public void Resume()
    {
        winScreenUI.SetActive(false);
        Time.timeScale = 1f;
        FPSController.isGameActive = true;
        playerController.LockCursor(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AddEventTriggers(Text buttonText, UnityEngine.Events.UnityAction action)
    {
        EventTrigger trigger = buttonText.gameObject.AddComponent<EventTrigger>();

        // Pointer Enter
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { buttonText.color = hoverColor; });
        trigger.triggers.Add(enterEntry);

        // Pointer Exit
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { buttonText.color = defaultColor; });
        trigger.triggers.Add(exitEntry);

        // Pointer Click
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((data) => 
        { 
            buttonText.color = clickColor;
            action.Invoke();
        });
        trigger.triggers.Add(clickEntry);
    }
}