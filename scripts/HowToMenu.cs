using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HowToMenu : MonoBehaviour
{
    [SerializeField] public Text backButton;

    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject howToMenuUI;

    [Header("Button Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color hoverColor = Color.red;
    [SerializeField] private Color clickColor = Color.green;
    
    // Start is called before the first frame update
    void Start()
    {
     backButton.color = defaultColor;   
     AddEventTriggers(backButton, BackToMenu);
    }

    public void BackToMenu()
    {
        // Hide the how-to screen and return to the start menu
        howToMenuUI.SetActive(false);
        startMenuUI.SetActive(true);
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
