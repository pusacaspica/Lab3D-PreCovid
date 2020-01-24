using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class AtomElementBuilder : MonoBehaviour {

    [SerializeField][Range(0.0f, 1.0f)]
    private float Tilt = 1;
    public Atom atom;
    public int valence;
    public Element element;
    public GameObject self;
    public int lastEletronLayer;
    public float eletrosphereRadius = 10;
    public float eletronSpeed = 100;
    public List<GameObject> eletrosphere = new List<GameObject>();
    public int sharedEletrons = 0; //shared or donated, it depends on the nature of the bonding
    //if shared, the eletron shall count for the valency of both atoms; the original owner and the receiver
    //if donated, the eletron shall count only for the valency of the receiver.

    void Awake() {
        this.valence = element.valence;
        self.transform.localScale += new Vector3(element.atomicMass/100, element.atomicMass/100, element.atomicMass/100);
        eletrosphereRadius += 5*self.transform.localScale.x;
        if (this.valence==0){
            if (element.atomicNumber==2) lastEletronLayer = 2;
            else lastEletronLayer = 8;
        } else {
            lastEletronLayer = element.lastEletronLayer;
        }
        if(eletrosphere.Count < element.valence){
            foreach (int i in System.Linq.Enumerable.Range(1, lastEletronLayer)){
                GameObject eletron = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                eletron.name = "eletron";
                eletron.transform.localScale = new Vector3(self.transform.localScale.x/2,self.transform.localScale.y/2,self.transform.localScale.z/2);
                eletron.transform.localPosition = new Vector3( 
                                                        eletrosphereRadius*Mathf.Cos((i-1)*2*Mathf.PI/lastEletronLayer), 
                                                        self.transform.position.y,
                                                        eletrosphereRadius*Mathf.Sin((i-1)*2*Mathf.PI/lastEletronLayer)
                                                        );
                eletron.transform.parent = self.transform;
                eletron.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Eletron.mat", typeof(Material));
                eletrosphere.Add(eletron);
                TrailRenderer trail = eletron.AddComponent<TrailRenderer>().GetComponent<TrailRenderer>();
                trail.material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Eletron.mat",typeof(Material));
                trail.minVertexDistance = 0.0001f;
                trail.time = 0.8f/lastEletronLayer;
                trail.startWidth = 5*eletron.transform.localScale.x;
                trail.endWidth = 0.0f;
                trail.startColor = Color.HSVToRGB(0.25f, 0.98f, 0.85f);
                trail.endColor = Color.HSVToRGB(0.19f, 0.92f, 0.65f);
                eletron.SetActive(false);
            }
        }
        switch(element.atomicGroup){
            case Element.atomicType.Alkaline:
                self.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Core_Alkaline.mat", typeof(Material));
                break;
            case Element.atomicType.Alkaline_earth:
                self.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Core_AlkalineEarth.mat", typeof(Material));
                break;
            case Element.atomicType.Lantinides:
                self.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Core_Lantinides.mat", typeof(Material));
                break;
            case Element.atomicType.Noble_Gas:
                self.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Core_NobleGas.mat", typeof(Material));
                break;
            case Element.atomicType.Nonmetal:
                self.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Core_Nonmetal.mat", typeof(Material));
                break;
            case Element.atomicType.Actinides:
                self.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Core_Actinides.mat", typeof(Material));
                break;
            case Element.atomicType.Semimetal:
                self.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Core_Semimetal.mat", typeof(Material));
                break;
            default:
                self.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/Core_Hydrogen.mat", typeof(Material));
                break;
        }
    }

    void FixedUpdate(){
        //self.transform.localRotation = new Quaternion(0,0,0,0);
        //self.transform.localPosition = new Vector3(0,0,0);
        GameObject eletron;
        foreach(int i in System.Linq.Enumerable.Range(0,self.transform.childCount)){
            eletron = self.transform.GetChild(i).gameObject;
            if (eletron.name != "eletron"){
                continue;
            }
            //Debug.Log(element.name.ToString()+", "+eletron.gameObject.transform.parent.localPosition.ToString() + ", " + eletron.gameObject.transform.parent.position.ToString());
            eletron.gameObject.transform.RotateAround(
                                                      self.transform.position, 
                                                      new Vector3(1-Tilt,0,Tilt), 
                                                      eletronSpeed*Time.deltaTime
                                                    );
            eletron.gameObject.transform.localPosition = new Vector3(eletron.gameObject.transform.localPosition.x, eletron.gameObject.transform.localPosition.y, eletron.gameObject.transform.localPosition.z);
        }
    }
}
