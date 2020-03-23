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

    //Don't change the Awake() and GetRecordWindow function
    //It basically just go and launch the record window (F10 on unity)
    //It also keeo the parameter, so don't forget to change them before launching the script
    private void Awake() {
        cameraTransform = prefabPosition.GetComponent<Transform>();
        Debug.Log("cameraTransform : "+ cameraTransform.position);
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
        while(true){
            //Instantiate the prefab, fastest and easiest way I found to "reload" the prefab when the record is over (for the next one)
            model = Instantiate(prefab, new Vector3(0f , 0f, 0f), Quaternion.Euler(0f, 90f, 0f));
            Destroy(model, timer);
            
            //Change only this line (or add)
            cameraTransform.Rotate(prefabPosition.transform.rotation.x, prefabPosition.transform.rotation.y + 90, prefabPosition.transform.rotation.z);
            
            //Don't change anything after this
            Debug.Log("CameraTransform in coroutine : " + cameraTransform.position);
            RecorderWindow recorderWindow = GetRecorderWindow();
            if(!recorderWindow.IsRecording()){
                Debug.Log("Start recording");
                recorderWindow.StartRecording();
                Debug.Log("Is recording (Start boucle): " + recorderWindow.IsRecording());
            }else{
                Debug.Log("is Recording : (stop and start boucle) " + recorderWindow.IsRecording());
                Debug.Log("Stop and Start recording");
                recorderWindow.StopRecording();
                Debug.Log("is Recording : (after stop) " + recorderWindow.IsRecording());
                Debug.Log("Start again recording");
                recorderWindow.StartRecording();
                Debug.Log("is Recording : (after start) " + recorderWindow.IsRecording());
                Debug.Log("End of boucle");
            }
            yield return new WaitForSeconds(timer);
        }
    }
    
}
