using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Vuforia;

public class Atom : MonoBehaviour {

    public Element element;
    public string Name;
    public int valence;
    public TrackableBehaviour self;
    private GameObject Nucleus;
    public List<GameObject> eletrons = new List<GameObject>();

    public GameManager gm;

    void Start(){
        this.Name = element.name;
        this.valence = element.valence;
        Nucleus = this.gameObject.transform.GetChild(0).gameObject;
        if (!Nucleus.activeSelf){
            Nucleus.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
        if (self.CurrentStatus == TrackableBehaviour.Status.TRACKED || self.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED){
            if(gm.reload == true) self.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if (gm.track1bool == false){
                gm.track1bool = true;
                gm.track1 = self;
                foreach(int i in Enumerable.Range(0,Nucleus.transform.childCount)){
                    Nucleus.transform.GetChild(i).gameObject.SetActive(true);
                }
            } else if (gm.track2bool == false && gm.track1 != self){
                gm.track2bool = true;
                gm.track2 = self;
                foreach(int i in Enumerable.Range(0,Nucleus.transform.childCount)){
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
                foreach(int i in Enumerable.Range(0,Nucleus.transform.childCount)){
                    Nucleus.transform.GetChild(i).gameObject.SetActive(false);
                }
            } else if (gm.track2 == self){
                gm.track2bool = false;
                gm.track2 = null;
                foreach(int i in Enumerable.Range(0,Nucleus.transform.childCount)){
                    Nucleus.transform.GetChild(i).gameObject.SetActive(false);
                }
            } else{
                //do nothing
            }
            if (self.gameObject.transform.childCount > 1){
                foreach(int childIndex in Enumerable.Range(1,self.gameObject.transform.childCount)){
                    Destroy(self.gameObject.transform.GetChild(childIndex).gameObject);
                }            
            }
        }
        //self.gameObject.transform.localRotation=new Quaternion(0,0,0,0);
    }

}
