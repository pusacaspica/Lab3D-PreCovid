using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName="New Element", menuName="Element")]
public class Element : ScriptableObject {

    public new string name = "dunno";
    public string alias = "idk";
    public int atomicNumber = 0;
    public float atomicMass = 0.0f;
    public int valence;
    public int lastEletronLayer;
    public atomicType atomicGroup;

    public enum atomicType{
        Hydrogen,
        Alkaline,
        Alkaline_earth,
        Nonmetal,
        Semimetal,
        Noble_Gas,
        Transition_Metal,
        Lantinides,
        Actinides
    };

}
