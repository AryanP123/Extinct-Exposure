using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class AreYouSure : MonoBehaviour
{
    [SerializeField] private GameObject areYouSureUI;
    
    [SerializeField] public Text yesButton;
    [SerializeField] public Text noButton;

    [Header("Button Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;
    [SerializeField] private Color clickColor = Color.green;

    public bool isPaused = false;

    void Start() {
        yesButton.color = defaultColor;
        noButton.color = defaultColor;

        AddEventTriggers(yesButton, BackToMainMenu);
        AddEventTriggers(noButton, Cancel);
    }

    public void Cancel()
    {
        areYouSureUI.SetActive(false);
    }

    public void BackToMainMenu()
    {   
        // Reset player position to the starting point
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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