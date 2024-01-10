using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPerception : MonoBehaviour
{
    [SerializeField] private string tagName = "";
    [SerializeField] float distance = 1;
    [SerializeField] float maxAngle = 45;
    public string getTagName { get { return tagName; } }
    public float getDistance { get { return distance; } }
    public float getMaxAngle { get {  return maxAngle; } }

    public abstract GameObject[] getGameObjects();
}
