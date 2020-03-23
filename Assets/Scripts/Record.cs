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
    [SerializeField] private GameObject prefab;
    private GameObject model;
    private Transform cameraTransform;
    private Transform start;
    private float timer = 3f;
    private int rotateCounter_x = 1;
    private int rotateCounter_y = 0;

    //Don't change the Awake() and GetRecordWindow function
    //It basically just go and launch the record window (F10 on unity)
    //It also keeo the parameter, so don't forget to change them before launching the script
    private void Awake() {
        cameraTransform = prefabPosition.GetComponent<Transform>();
    }
    private static RecorderWindow GetRecorderWindow(){
        return (RecorderWindow)EditorWindow.GetWindow(typeof(RecorderWindow));
    }
    //Add what you want in Start()
    private void Start() {
        StartCoroutine(AutomaticRecord());
    }

    //Only rotate for the moment, 90 on the y axis to see if it works correctly
    private IEnumerator AutomaticRecord(){
        //rotateCounter_x != 4 && rotateCounter_y % 4 != 0
        while(true){
            //Get the window for the record
            RecorderWindow recorderWindow = GetRecorderWindow();

            //End the recording
            if(rotateCounter_x == 4 && rotateCounter_y == 4){
                recorderWindow.StopRecording();
                yield break;
            }

            //Instantiate the prefab, fastest and easiest way I found to "reload" the prefab when the record is over (for the next one)
            //Change only the rotation added (here 90 for example) and/or the x rotation for the prefab instance (depending of what type of fall you want)
            model = Instantiate(prefab, new Vector3(0f , 0f, 0f), Quaternion.Euler(5f, 90f, 0f));
            Destroy(model, timer);
            if(rotateCounter_y%4 == 0 && rotateCounter_y != 0){
                rotateCounter_x += 1;
                rotateCounter_y = 0;
                prefabPosition.transform.Rotate(90, 0, 0);
            }
            cameraTransform.Rotate(prefabPosition.transform.rotation.x, prefabPosition.transform.rotation.y + 90, prefabPosition.transform.rotation.z);
            rotateCounter_y += 1;

            //Don't change anything
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
