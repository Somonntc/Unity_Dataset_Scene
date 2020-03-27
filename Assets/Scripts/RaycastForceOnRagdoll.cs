using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForceOnRagdoll : MonoBehaviour
{
    private LayerMask m_layer;
    [SerializeField] private GameObject Object;
    private Transform t_object;
    
    public static ContactPoint contactPoint;
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
        Debug.DrawRay(transform.position, -transform.forward * 100);
        if(Physics.Raycast (transform.position, -transform.forward, out hit, Mathf.Infinity)){
            RagdollEnable.test = false;
            Debug.Log("test");
            hit.rigidbody.AddForce(-transform.forward * 20, ForceMode.Impulse);
            Destroy(gameObject, 0.1f);
        }
    }

    //private void OnCollisionEnter(Collision other) {
    //    foreach (ContactPoint contact in other.contacts) {
    //        Debug.DrawRay(contact.point, contact.normal, Color.green, 2, false);
    //        contactPoint = contact;
    //    }
    //}
}
