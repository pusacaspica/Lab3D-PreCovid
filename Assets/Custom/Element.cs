using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Element", menuName="Element")]
public class Element : ScriptableObject {

    public new string name;
    public int atomicNumber;
    public int atomicMass;
    public int valence;
    public enum atomictype{Hydrogen = 0, AlkaliMetal = 1, AlkalineEarthMetal = 2, TransitionMetal = 3, SemiMetal = 4, NonMetal = 5, NobleGas = 6, Lanthanide = 7, Actinide = 8};

}
