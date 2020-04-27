using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder;
using UnityEditor;

public class Record : MonoBehaviour
{
    //Don't change anything here except timer (except if it still works)
    private RecorderWindow recorderWindow;
    [SerializeField] private GameObject prefabPosition;
    [SerializeField] public GameObject prefab;
    [SerializeField] private GameObject sphere;
    [SerializeField] private int NumberOfRecords;
    [SerializeField] private float timer = 1f;
    [SerializeField] private bool ActivateSphere = true;
    private GameObject triggerZone;
    private GameObject sphereInstance;
    private GameObject model;
    private Transform cameraTransform;
    private Transform focusHips;
    
    //Variables qui permettent de "compter" le nombre de rotation, possibilité de changer
    //la façon que j'ai utilisé qui n'est pas vraiment simple à comprendre
    private int rotateCounter_x = 1;
    private int rotateCounter_y = 0;
    private int counter = 0;

    //Don't change the Awake() and GetRecordWindow function
    //It basically just go and launch the record window (F10 on unity)
    //It also keeo the parameter, so don't forget to change them before launching the script
    private void Awake() {
        cameraTransform = prefabPosition.GetComponent<Transform>();
        //transform.position = new Vector3(focusHips.position.x + 0.0f, 0f, focusHips.position.z);
    }
    private static RecorderWindow GetRecorderWindow(){
        return (RecorderWindow)EditorWindow.GetWindow(typeof(RecorderWindow));
    }
    //Add what you want in Start()
    private void Start() {
        StartCoroutine(AutomaticRecord());
    }

    private void Update() {
        //Focus on the model hips to avoid the model leaving the screen
        if(model.scene.IsValid()){
            if(!RagdollEnable.m_isKinematic){
                transform.position = new Vector3(focusHips.position.x + 0.0f, 0f, focusHips.position.z);
            }
        }
        //Change the timer here if you are doing walk and fall record
        //it prevent the model to be on the floor for too long or to not have the time to fall completly
        //If the hit is on the side of the model for example
        if(counter == 90 || counter == 270){
            timer = 2f;
        }
        //if the hit point is on the front/back of the model for example
        else{
            timer = 2f;
        }
    }

    private IEnumerator AutomaticRecord(){
        while(true){
            //Get the window for the record
            RecorderWindow recorderWindow = GetRecorderWindow();

            //End the recording
            if(rotateCounter_x == 4 && rotateCounter_y == 360/NumberOfRecords){
                recorderWindow.StopRecording();
                yield break;
            }

            //Instantiate the prefab, you need to specify the coordinate if you don't want this one
            model = Instantiate(prefab, new Vector3(-4f , 0f, -1f), Quaternion.Euler(0f, counter, 0f));
            //You will want to change the coordinate to record different way of falling, but you don't need to change the rotation
            if(ActivateSphere){
                sphereInstance = Instantiate(sphere, new Vector3(-4f, 1.4f, -1.6f), Quaternion.Euler(0,180f,0));
            }

            //if the model is the female model use this line
            //focusHips = model.transform.Find("Armature/mixamorig12:Hips");
            //if the model is the male model use this line
            focusHips = model.transform.Find("Armature/mixamorig:Hips");

            //Destroy the model and the sphere so we can have a new model and a new shpere at the new coordinate
            Destroy(model, timer);
            Destroy(sphereInstance, timer);

            //
            if(rotateCounter_y%NumberOfRecords == 0 && rotateCounter_y != 0){
                rotateCounter_x += 1;
                rotateCounter_y = 0;
                //prefabPosition.transform.Rotate(10, 90, 0);
                counter += 360/NumberOfRecords;
            }
            Debug.Log("rotate_counter_x : " + rotateCounter_x + "rotate counter y : " + rotateCounter_y);
            //Change the camera angle
            cameraTransform.Rotate(prefabPosition.transform.rotation.x, prefabPosition.transform.rotation.y + 360/NumberOfRecords, prefabPosition.transform.rotation.z);
            rotateCounter_y += 1;

            //Start and stop the recording after each timer
            if(!recorderWindow.IsRecording()){
                recorderWindow.StartRecording();
            }else{
                recorderWindow.StopRecording();
                recorderWindow.StartRecording();
            }
            yield return new WaitForSeconds(timer);
        }
    }
    
}
