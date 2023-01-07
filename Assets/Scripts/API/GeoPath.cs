using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VWorld", menuName = "VWorldPractice/GeoPath", order = 1)]
public class GeoPath : ScriptableObject
{
    [SerializeField]
    public List<Location> geoLocationPath;
}

[System.Serializable]
public struct Location
{
    public double latitude;
    public double longtitude;
}