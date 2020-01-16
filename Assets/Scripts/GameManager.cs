using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour {
    

    public bool track1bool, track2bool;
    public TrackableBehaviour track1, track2;
    public bool swap = false;
    public bool reload = true;

    public double threshold = 1;

    // Start is called before the first frame update
    void Start() {
        track1bool = track2bool = false;
    }
    
    // Update is called once per frame
    void Update() {
        if(track1 != null && track2 != null && DistanceBetweenMarkers(track1, track2) <= threshold && swap == true){
            SwapModel();
            reload = false;
        }
    }

    void SwapModel(){
        GameObject trackableMarker = track1.gameObject;
        GameObject otherTrackableGameObject = track2.gameObject;
        Instantiate(otherTrackableGameObject.gameObject.transform.GetChild(0).gameObject,
                    trackableMarker.gameObject.transform.GetChild(0).gameObject.transform.position,
                    otherTrackableGameObject.gameObject.transform.GetChild(0).gameObject.transform.rotation,
                    trackableMarker.transform);
        otherTrackableGameObject.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        swap = false;
        //clone.transform.localRotation = trackableMarker.gameObject.transform.GetChild(0).gameObject.transform.localRotation;
        //clone.transform.localScale = trackableMarker.gameObject.transform.GetChild(0).gameObject.transform.localScale;
    }

    double DistanceBetweenMarkers(TrackableBehaviour t1, TrackableBehaviour t2){
        double dist = Mathf.Sqrt((t1.gameObject.transform.position.x-t2.gameObject.transform.position.x)*(t1.gameObject.transform.position.x-t2.gameObject.transform.position.x)+
                                 (t1.gameObject.transform.position.y-t2.gameObject.transform.position.y)*(t1.gameObject.transform.position.y-t2.gameObject.transform.position.y)+
                                 (t1.gameObject.transform.position.z-t2.gameObject.transform.position.z)*(t1.gameObject.transform.position.z-t2.gameObject.transform.position.z));
        if (track1bool == track2bool && reload == true){
            swap = true;
        }
        return dist;
    }
}
