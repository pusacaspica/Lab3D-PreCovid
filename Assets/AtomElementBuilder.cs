using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AtomElementBuilder : MonoBehaviour {

    public Element element;
    public GameObject self;
    public float eletrosphereRadius = 10;
    public float eletronSpeed = 100;
    // Start is called before the first frame update
    void Start() {
        GameObject nucleus = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        nucleus.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        nucleus.transform.localScale += new Vector3(element.atomicMass, element.atomicMass, element.atomicMass);
        self.transform.parent = nucleus.transform;
        foreach (int i in Enumerable.Range(1, element.valence)){
            GameObject eletron = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            eletron.transform.position = nucleus.transform.position + new Vector3(0, eletrosphereRadius, 0);
            eletron.transform.RotateAround(nucleus.transform.localScale, Vector3.right, eletronSpeed*Time.deltaTime);
        }
    }
}
