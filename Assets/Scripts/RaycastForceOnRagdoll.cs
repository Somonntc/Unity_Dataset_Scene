using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForceOnRagdoll : MonoBehaviour
{
    private LayerMask m_layer;
    [SerializeField] private GameObject Object;
    private Transform t_object;
    
    private void Awake() {
        t_object = Object.GetComponent<Transform>();
        m_layer = LayerMask.NameToLayer("ragdoll");
    }
    void Update()
    {
        DisplayRaycastHit();
        //t_object.position = new Vector3(t_object.position.x, t_object.position.y, t_object.position.z - 0.01f);
    }

    void DisplayRaycastHit(){
        RaycastHit hit;
        //Affiche le raycast sur l'éditeur pour savoir ou l'impact aura lieu
        Debug.DrawRay(transform.position, -transform.forward * 100);
        if(Physics.Raycast (transform.position, -transform.forward, out hit, Mathf.Infinity)){
            //On désactive le fait que tout le corps soit Kinematic, ce qui activera sa chute (il sera plus fix à sa position)
            RagdollEnable.m_isKinematic = false;
            //Application d'une force sur le point d'impact du raycast
            hit.rigidbody.AddForce(-transform.forward * 20, ForceMode.Impulse);
            //On détruit l'object ayany le raycast qui applique la force au ragdoll
            //Il faut pas le détruire instant, il faut laisser un petit délai sinon la force ne s'appliquera pas correctement
            //Et le ragdoll tombera sur place
            //il faut aussi pas aller trop vite si on déplace le raycast sur le ragdoll, on obtient le même pb sinon
            //Autrement ça fonctionne bien, j'ai test sur presque toutes les parties du corps et on obtient un résultat potable
            Destroy(gameObject, 0.1f);
        }
    }

    //Au lieu de détruire la sphère (ça posera problème), juste la tp à sa position de base devrait suffire pour faire les enregistrements

    //private void OnCollisionEnter(Collision other) {
    //    foreach (ContactPoint contact in other.contacts) {
    //        Debug.DrawRay(contact.point, contact.normal, Color.green, 2, false);
    //        contactPoint = contact;
    //    }
    //}
}
