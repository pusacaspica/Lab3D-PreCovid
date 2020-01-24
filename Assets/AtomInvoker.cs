using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomInvoker : MonoBehaviour
{
    private ParticleSystem Pop;
    private AtomElementBuilder Element;
    private Animator AtomAnimator;
    public Camera camera;
    public GameObject atom;
    // Start is called before the first frame update
    void Start() {
        atom = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        Element = atom.GetComponent<AtomElementBuilder>();
        Pop = atom.GetComponent<ParticleSystem>();
        Pop.Stop();
        Pop.GetComponent<ParticleSystemRenderer>().material = Element.gameObject.GetComponent<Renderer>().material;
        Debug.Log(atom.name);
        AtomAnimator = atom.GetComponent<Animator>();
        AtomAnimator.SetBool("AtomInvoked", false);
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)){
           Debug.Log(Input.mousePosition);
           Ray ray = camera.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;
           if(Physics.Raycast(ray, out hit)){
               Debug.Log("eitcha");
                if (!AtomAnimator.GetBool("AtomInvoked")){
                    AtomAnimator.SetBool("AtomInvoked", true);
                } else {
                    Debug.Log("plau");
                    AtomAnimator.SetBool("AtomInvoked", false);
                    Pop.Play();
                }
            }
        }
    }
}
