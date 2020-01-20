using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Vuforia;

public class Atom : MonoBehaviour {

    public AtomElementBuilder elementBuilded;
    public string Name;
    public int Valence;
    public TrackableBehaviour self;
    private GameObject Nucleus;
    public List<GameObject> eletrons = new List<GameObject>();

    public GameManager gm;
    
    public Atom(string Name, int Valence, TrackableBehaviour Self){
        this.Name = Name;
        this.Valence = Valence;
        this.self = Self;
    }

    void Start(){
        Nucleus = this.gameObject.transform.GetChild(0).gameObject;
        if (!Nucleus.activeSelf){
            Nucleus.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(Nucleus.transform.childCount);
        if (self.CurrentStatus == TrackableBehaviour.Status.TRACKED || self.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED){
            if(gm.reload == true) self.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if (gm.track1bool == false){
                gm.track1bool = true;
                gm.track1 = self;
                foreach(int i in Enumerable.Range(1,Nucleus.transform.childCount)){
                    Nucleus.transform.GetChild(i).gameObject.SetActive(true);
                }
            } else if (gm.track2bool == false && gm.track1 != self){
                gm.track2bool = true;
                gm.track2 = self;
                foreach(int i in Enumerable.Range(1,Nucleus.transform.childCount)){
                    Nucleus.transform.GetChild(i).gameObject.SetActive(true);
                }
            } else{
                //do nothing
            }
        }
        if (self.CurrentStatus == TrackableBehaviour.Status.LIMITED || self.CurrentStatus == TrackableBehaviour.Status.NO_POSE){
            if (gm.reload == false){
                gm.reload = true;
            }
            if(gm.track1 == self){
                gm.track1 = gm.track2;
                gm.track1bool = gm.track2bool;
                gm.track2 = null;
                gm.track2bool = false;
                foreach(int i in Enumerable.Range(1,Nucleus.transform.childCount)){
                    Nucleus.transform.GetChild(i).gameObject.SetActive(false);
                }
            } else if (gm.track2 == self){
                gm.track2bool = false;
                gm.track2 = null;
                foreach(int i in Enumerable.Range(1,Nucleus.transform.childCount)){
                    Nucleus.transform.GetChild(i).gameObject.SetActive(false);
                }
            } else{
                //do nothing
            }
            if (self.gameObject.transform.childCount > 1){
                foreach(int childIndex in Enumerable.Range(1,self.gameObject.transform.childCount)){
                    Debug.Log(childIndex);
                    Destroy(self.gameObject.transform.GetChild(childIndex).gameObject);
                }            
            }
        }
    }

}
