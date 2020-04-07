using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForceOnRagdoll : MonoBehaviour
{
    //private LayerMask m_layer;
    [SerializeField] private GameObject Object;
    //Variable if you want to move the sphere with the raycast (you have to change the line 25 (currently a comment))
    private Transform t_object;
    //Force added to the model (change directly from unity, default value is 15)
    [SerializeField] private float force = 15;
    //If you want to only make walking video (true if you want to activate the falling system)
    [SerializeField] private bool m_isActive = false;
    
    
    private void Awake() {
        t_object = Object.GetComponent<Transform>();
        //m_layer = LayerMask.NameToLayer("ragdoll");
    }
    void Update()
    {
        if(m_isActive){
            DisplayRaycastHit();
        }
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f, 1.25f, 0.56f);
        //t_object.position = new Vector3(t_object.position.x, t_object.position.y, t_object.position.z - 0.01f);
    }

    void DisplayRaycastHit(){
        RaycastHit hit;
        //Affiche le raycast sur l'éditeur pour savoir ou l'impact aura lieu
        Debug.DrawRay(transform.position, -transform.forward * 100);
        if(Physics.Raycast (transform.position, -transform.forward, out hit, Mathf.Infinity)){
            //On désactive le fait que tout le corps soit Kinematic, ce qui activera sa chute
            RagdollEnable.m_isKinematic = false;
            Debug.Log("Hit.transform.gameObject : " + hit.transform.root);
            if(hit.transform.root.GetComponent<Animator>().enabled){
                hit.transform.root.GetComponent<Animator>().enabled = false;
            }
            //hit.transform.gameObject.GetComponent<Animator>().enabled = false;
            //Application d'une force sur le point d'impact du raycast
            hit.rigidbody.AddForce(-transform.forward * force, ForceMode.Impulse);
            Destroy(gameObject, 0.1f);
        }
    }
}
