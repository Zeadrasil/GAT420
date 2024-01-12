using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPerception : MonoBehaviour
{
    [SerializeField] protected string tagName = "";
    [SerializeField] protected float distance = 1;
    [SerializeField] protected float maxAngle = 45;
    public string getTagName { get { return tagName; } }
    public float getDistance { get { return distance; } }
    public float getMaxAngle { get {  return maxAngle; } }

    public abstract GameObject[] getGameObjects();
}
