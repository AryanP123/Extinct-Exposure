using UnityEngine;
using System.Collections.Generic;

public class DinoBoundary : MonoBehaviour
{
    public List<GameObject> charactersInside = new List<GameObject>();

    public bool isCharacterInside(GameObject other)
    {
        return charactersInside.Contains(other);
    }

    private void OnTriggerEnter(Collider other)
    {
            if (!charactersInside.Contains(other.gameObject))
            {
                charactersInside.Add(other.gameObject);
                Debug.Log($"Character {other.name} entered DinoBoundary.");
            }
    }
    private void OnTriggerStay(Collider other)
    {
            if (!charactersInside.Contains(other.gameObject))
            {
                charactersInside.Add(other.gameObject);
            }
    }

    private void OnTriggerExit(Collider other)
    {
            if (charactersInside.Contains(other.gameObject))
            {
                charactersInside.Remove(other.gameObject);
                Debug.Log($"Character {other.name} exited DinoBoundary.");
            }
    }
}
