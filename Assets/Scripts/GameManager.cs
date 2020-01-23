using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour {
    

    public bool track1bool, track2bool;
    public TrackableBehaviour track1, track2;
    public bool swap = false;
    public bool reload = true;

    //minimal distance between atoms to molecule to be built is around 7. 
    public double threshold = 1;

    // Start is called before the first frame update
    void Start() {
        track1bool = track2bool = false;
    }
    
    // Update is called once per frame
    void Update() {
        Debug.Log(DistanceBetweenMarkers(track1, track2));
        Debug.Log(CalculateValence(track1.GetComponent<Atom>().element.valence, track2.GetComponent<Atom>().element.valence));
        Debug.Log(track1.GetComponent<Atom>().element.valence.ToString()+", "+track2.GetComponent<Atom>().element.valence.ToString());
        /*
            if:
                there are two trackable objects at a given time;
                the two trackable objects are close enough to justify the chemical reaction;
                the both of them can be bonded without disrupting the valence of the universe
                there can be any bonding
        */
        if(track1 != null && track2 != null && DistanceBetweenMarkers(track1, track2) <= threshold && 0 <= CalculateValence(track1.GetComponent<Atom>().element.valence, track2.GetComponent<Atom>().element.valence) && swap == true && reload == true){
            if(!track1.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal) && track2.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal) ||
               track1.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal) && !track2.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal)){
                BondAtoms("donated");
                reload = false;
            } else if ((track1.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal) && track2.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal))){
                BondAtoms("shared");
                reload = false;
            } else if ((!track1.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal) && !track2.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal))){
                BondAtoms("metallic");
                reload = false;
            } else if ((track1.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Noble_Gas)) || (track2.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Noble_Gas))){
                Debug.Log("No bonding for you!");
            }
        }
    }

    void BondAtoms(string bondingMethod){
        GameObject trackableMarker = track1.gameObject;
        GameObject newTrackableMarker = track2.gameObject;
        GameObject spawnedNewNucleus = Instantiate(newTrackableMarker.gameObject.transform.GetChild(0).gameObject,
                    trackableMarker.gameObject.transform.GetChild(0).gameObject.transform.position + new Vector3(
                        trackableMarker.GetComponent<AtomElementBuilder>().eletrosphereRadius+trackableMarker.GetComponent<AtomElementBuilder>().element.atomicMass*Mathf.Sin(Mathf.PI), 
                        trackableMarker.GetComponent<AtomElementBuilder>().eletrosphereRadius+trackableMarker.GetComponent<AtomElementBuilder>().element.atomicMass*Mathf.Cos(Mathf.PI),
                        trackableMarker.GetComponent<AtomElementBuilder>().eletrosphereRadius+trackableMarker.GetComponent<AtomElementBuilder>().element.atomicMass),
                    newTrackableMarker.gameObject.transform.GetChild(0).gameObject.transform.rotation,
                    trackableMarker.transform);
        foreach(int i in Enumerable.Range(spawnedNewNucleus.GetComponent<AtomElementBuilder>().lastEletronLayer, 2*spawnedNewNucleus.GetComponent<AtomElementBuilder>().lastEletronLayer)){
            Destroy(spawnedNewNucleus.gameObject.transform.GetChild(i));
        }
        Molecule molecule = track1.gameObject.AddComponent<Molecule>().GetComponent<Molecule>();
        molecule.atoms.Add(trackableMarker.gameObject.GetComponent<Atom>());
        molecule.atoms.Add(newTrackableMarker.gameObject.GetComponent<Atom>());
        molecule.elements.Add(trackableMarker.GetComponent<Atom>().element);
        molecule.elements.Add(newTrackableMarker.GetComponent<Atom>().element);
        molecule.valence = CalculateValence(trackableMarker.GetComponent<Atom>().element.valence, newTrackableMarker.GetComponent<Atom>().element.valence);
        newTrackableMarker.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        swap = false;
        if(bondingMethod.Equals("shared")){
            foreach(int i in Enumerable.Range(1, Mathf.Min(spawnedNewNucleus.GetComponent<AtomElementBuilder>().valence, trackableMarker.GetComponent<AtomElementBuilder>().valence))){
                spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere.Add(trackableMarker.GetComponent<AtomElementBuilder>().eletrosphere[0]);
                trackableMarker.GetComponent<AtomElementBuilder>().eletrosphere.Add(spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere[0]);
            }
            Debug.Log("One has shared eletrons with the other!");
        } else if (bondingMethod.Equals("donated")){
            if (trackableMarker.GetComponent<Atom>().element.atomicGroup.Equals(Element.atomicType.Nonmetal)) {
                foreach(int i in Enumerable.Range(1, Mathf.Min(spawnedNewNucleus.GetComponent<AtomElementBuilder>().valence, trackableMarker.GetComponent<AtomElementBuilder>().valence))) {
                    spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere.Remove(spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere[spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere.Count-1]);
                    trackableMarker.GetComponent<AtomElementBuilder>().eletrosphere.Add(trackableMarker.GetComponent<AtomElementBuilder>().eletrosphere[0]);
                }
            } else {
                foreach(int i in Enumerable.Range(1, Mathf.Min(spawnedNewNucleus.GetComponent<AtomElementBuilder>().valence, trackableMarker.GetComponent<AtomElementBuilder>().valence))) {
                    trackableMarker.GetComponent<AtomElementBuilder>().eletrosphere.Remove(spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere[spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere.Count-1]);
                    spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere.Add(trackableMarker.GetComponent<AtomElementBuilder>().eletrosphere[0]);
                }
            }
            Debug.Log("One has donated eletrons to the other!");
        } else if (bondingMethod.Equals("metallic")){
            foreach(int i in Enumerable.Range(1, Mathf.Min(spawnedNewNucleus.GetComponent<AtomElementBuilder>().valence, trackableMarker.GetComponent<AtomElementBuilder>().valence))){
                spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere.Add(trackableMarker.GetComponent<AtomElementBuilder>().eletrosphere[0]);
                trackableMarker.GetComponent<AtomElementBuilder>().eletrosphere.Add(spawnedNewNucleus.GetComponent<AtomElementBuilder>().eletrosphere[0]);
            }
            Debug.Log("One has lost eletrons with the other!");
        }
        //spawnedNewNucleus.transform.localRotation = trackableMarker.gameObject.transform.GetChild(0).gameObject.transform.localRotation;
        //spawnedNewNucleus.transform.localScale = trackableMarker.gameObject.transform.GetChild(0).gameObject.transform.localScale;
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

    int CalculateValence(int track1, int track2){
        if (track2 > track1){
            return track2 - track1;
        } else {
            return track1 - track2;
        }
    }
}
