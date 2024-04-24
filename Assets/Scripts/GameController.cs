using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static public bool ObjectNameContains(string objectName, string _string) => objectName.ToLower().Contains(_string.ToLower());

    static public bool OVRInteraction(string objectName) => ObjectNameContains(objectName, "controller") || ObjectNameContains(objectName, "hand");
}
