using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnable : MonoBehaviour
{
    private LayerMask m_layer;
    private ContactPoint contactpoint;
    public static bool test;
    public void SetKinematic(bool newValue){
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in bodies){
            rb.isKinematic = newValue;
        }
    }

    private void Awake() {
        test = true;
        m_layer = LayerMask.NameToLayer("target");
        Physics.IgnoreLayerCollision (9, 8, true);
    }

    private void Start() {
        SetKinematic(true);
    }

    private void Update() {
        SetKinematic(test);
    }    
}
