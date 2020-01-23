using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Vuforia;

public class Molecule : MonoBehaviour
{

    public List<Atom> atoms = new List<Atom> (); //all the atoms that are in the molecule
    public List<Element> elements = new List<Element> (); //all the elements that are in the molecule
    public int valence; //if it's any different than zero, it means one of the atoms in the molecule can form another bond.


    // Start is called before the first frame update
    void Start()
    {
        elements = elements.OrderBy(element=>element.valence).ToList();
        foreach (Element el in elements){
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
