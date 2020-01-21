using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AtomElementBuilder : MonoBehaviour {


    public Atom atom;
    public Element element;
    public GameObject self;
    public float eletrosphereRadius = 10;
    public float eletronSpeed = 100;
    public Material coreMaterial;
    public Material eletronMaterial;
    // Start is called before the first frame update
    void Start() {
        eletrosphereRadius += element.atomicNumber;
        self.transform.localScale += new Vector3(element.atomicMass/2, element.atomicMass/2, element.atomicMass/2);
        Debug.Log(Enumerable.Range(2,1).ToString());
        int lastEletronLayer;
        if (element.valence==0){
            if (element.atomicNumber==2) lastEletronLayer = 2;
            else lastEletronLayer = 8;
        } else {
            lastEletronLayer = element.valence;
        }
        foreach (int i in Enumerable.Range(1, lastEletronLayer)){
            GameObject eletron = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            eletron.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            eletron.transform.position = new Vector3( 
                                                     eletrosphereRadius*Mathf.Cos((i-1)*2*Mathf.PI/lastEletronLayer), 
                                                     0,
                                                     eletrosphereRadius*Mathf.Sin((i-1)*2*Mathf.PI/lastEletronLayer)
                                                     );
            eletron.transform.RotateAround(self.transform.localScale, Vector3.right, eletronSpeed*Time.deltaTime);
            eletron.transform.parent = self.transform;
            eletron.GetComponent<Renderer>().material = eletronMaterial;
            atom.eletrons.Add(self);
            eletron.SetActive(false);
        }
    }

    void FixedUpdate(){
        GameObject eletron;
        foreach(int i in Enumerable.Range(0,self.transform.childCount)){
            eletron = self.transform.GetChild(i).gameObject;
            eletron.gameObject.transform.RotateAround(eletron.gameObject.transform.parent.position, new Vector3(0,1,0), eletronSpeed*Time.deltaTime);
        }
    }
}
