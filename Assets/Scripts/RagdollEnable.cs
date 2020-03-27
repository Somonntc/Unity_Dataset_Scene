using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEnable : MonoBehaviour
{
    //private LayerMask m_layer;
    public static bool m_isKinematic;

    //On fait en sorte que toutes les parties du corps du ragdoll soient Kinematic
    //Kinematic : RigidBody et Box collider, mais le ragdoll sera fix (il tombera pas comme on avait avant)
    public void SetKinematic(bool newValue){
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in bodies){
            rb.isKinematic = newValue;
        }
    }

    private void Awake() {
        m_isKinematic = true;
        //m_layer = LayerMask.NameToLayer("target");
        //Physics.IgnoreLayerCollision (9, 8, true);
    }
 
    private void Start() {
        SetKinematic(m_isKinematic);
    }

    private void Update() {
        SetKinematic(m_isKinematic);
    }    
}
