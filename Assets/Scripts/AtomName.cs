using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;

public class AtomName : MonoBehaviour
{
    public GameObject self;
    private Vector3 InitialPos;
    private TMP_Text LegendName;
    public GameObject nucleus;
    public TrackableBehaviour trackable;
    public enum TypeOfText{
        Alias, Name, AtomicMass, AtomicNumber
    };
    public TypeOfText typeOfText;
    // Start is called before the first frame update
    void Start() {
        InitialPos = this.transform.position;
        LegendName = GetComponent<TMP_Text>();
        if (typeOfText==TypeOfText.Alias){
            LegendName.text = nucleus.GetComponent<AtomElementBuilder>().element.alias;
        } else if (typeOfText==TypeOfText.Name) {
            LegendName.text = nucleus.GetComponent<AtomElementBuilder>().element.name;
        } else if (typeOfText==TypeOfText.AtomicNumber) {
            LegendName.text = nucleus.GetComponent<AtomElementBuilder>().element.atomicNumber.ToString();
        } else if (typeOfText==TypeOfText.AtomicMass) {
            LegendName.text = nucleus.GetComponent<AtomElementBuilder>().element.atomicMass.ToString();
        }
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = InitialPos;
        this.transform.rotation = Quaternion.Euler(90f,0,0);
        //this.transform.localRotation = Quaternion.Euler(0,-90f,0);
    }
}
