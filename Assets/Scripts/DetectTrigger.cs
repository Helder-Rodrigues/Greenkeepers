using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    private List<GameObject> objectsColliders = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {   
        if (!GameController.OVRInteraction(other.name))
            objectsColliders.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveObjectFromList(other.gameObject);
    }

    private bool AnyDetectedOjbectWithName(string objectName) => objectsColliders.Count != 0 && objectsColliders.Any(obj => GameController.ObjectNameContains(obj.name, objectName));

    public List<GameObject> GetDetectedObjectsWithName(string objectName)
    {
        if (AnyDetectedOjbectWithName(objectName))
            return objectsColliders.Where(obj => GameController.ObjectNameContains(obj.name, objectName)).ToList();
        return null;
    }

    public void RemoveObjectFromList(GameObject gameObject)
    {
        objectsColliders.Remove(gameObject);
    }
}
