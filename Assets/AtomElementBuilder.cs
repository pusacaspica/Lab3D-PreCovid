using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AtomElementBuilder : MonoBehaviour {

    public Element element;
    public GameObject self;
    public float eletrosphereRadius = 10;
    public float eletronSpeed = 100;
    public Material coreMaterial;
    public Material eletronMaterial;
    // Start is called before the first frame update
    void Start() {
        self.transform.localScale += new Vector3(element.atomicMass/2, element.atomicMass/2, element.atomicMass/2);
        self.GetComponent<Renderer>().material = coreMaterial;
        foreach (int i in Enumerable.Range(1, element.valence)){
            GameObject eletron = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            eletron.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            eletron.transform.position = new Vector3(eletrosphereRadius*Mathf.Sin(i), 
                                                          eletrosphereRadius*Mathf.Cos(i), 
                                                          0);
            eletron.transform.RotateAround(self.transform.localScale, Vector3.right, eletronSpeed*Time.deltaTime);
            eletron.transform.parent = self.transform;
            eletron.GetComponent<Renderer>().material = eletronMaterial;
        }
    }

    void FixedUpdate(){
        GameObject eletron;
        foreach(int i in Enumerable.Range(0,self.transform.childCount)){
            eletron = self.transform.GetChild(i).gameObject;
            eletron.gameObject.transform.RotateAround(eletron.gameObject.transform.parent.position, Vector3.up + new Vector3(0, Mathf.Sin(i), Mathf.Cos(i)), eletronSpeed*Time.deltaTime);
        }
    }
}
