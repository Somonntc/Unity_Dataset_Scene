using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject sphere;
    
    private Transform modelHips;
    // Start is called before the first frame update
    private void Awake() {
        //transform.position = new Vector3(0, 0.865f, model.transform.position.z +3);
        //If the model is the m model use this line
        modelHips = model.transform.Find("Armature/mixamorig12:Hips");
        //if the model is the f model use this line
        //modelHips = model.transform.Find("Armature/mixamorig:Hips");

    }

    // Update is called once per frame
    void Update()
    {
        //If only fall 
        //---------------------------------------------
        //transform.position = new Vector3(model.transform.position.x + 0.0f, 0f, model.transform.position.z);
        //---------------------------------------------

        //If Walk and Fall
        //---------------------------------------------
        if(!RagdollEnable.m_isKinematic){
            transform.position = new Vector3(modelHips.position.x + 0.0f, 0f, modelHips.position.z);
        }
        //---------------------------------------------

        //If only Walk do nothing, the model should not have "Apply root motion active (need a confirmation)
    }
}
