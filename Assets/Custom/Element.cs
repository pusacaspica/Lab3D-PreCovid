using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Element", menuName="Element")]
public class Element : ScriptableObject {

    public new string name;
    public int atomicNumber;
    public int atomicMass;
    public int valence;
    public enum atomicType{
        Alkaline
    };

}
