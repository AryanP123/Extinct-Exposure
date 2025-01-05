using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    public List<string> dinosaursToPhotograph;
    private HashSet<string> photographedDinosaurs;
    private string currentDinosaurName;

    void Start()
    {
        photographedDinosaurs = new HashSet<string>();
    }

    public void CheckForDinosaur()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            string hitObjectName = hit.collider.gameObject.name;
            currentDinosaurName = hitObjectName;
            Debug.Log("Hit object: " + hitObjectName);
            if (dinosaursToPhotograph.Contains(hitObjectName) && !photographedDinosaurs.Contains(hitObjectName))
            {
                photographedDinosaurs.Add(hitObjectName);
                Debug.Log("Photographed dinosaur: " + hitObjectName);
                CheckWinCondition();
            }
        }
    }

    public string GetCurrentDinosaurName()
    {
        return currentDinosaurName;
    }

    private void CheckWinCondition()
    {
        if (photographedDinosaurs.Count == dinosaursToPhotograph.Count)
        {
            Debug.Log("All dinosaurs photographed! You win!");
            // Implement win condition logic here (e.g., show win screen, end game, etc.)
        }
    }
}