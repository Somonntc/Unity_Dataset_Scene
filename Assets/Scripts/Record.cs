using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Recorder;
using UnityEditor;

public class Record : MonoBehaviour
{
    private RecorderWindow recorderWindow;
    [SerializeField] private GameObject prefabPosition;
    [SerializeField] private GameObject prefab;
    private GameObject model;
    private Transform cameraTransform;
    private Transform start;

    private void Awake() {
        cameraTransform = prefabPosition.GetComponent<Transform>();
        Debug.Log("cameraTransform : "+ cameraTransform.position);
    }
    private static RecorderWindow GetRecorderWindow(){
        return (RecorderWindow)EditorWindow.GetWindow(typeof(RecorderWindow));
    }

    private void Start() {
        //StartCoroutine(StartStopRecord());
        StartCoroutine(test());
    }

    private IEnumerator test(){
        while(true){
            model = Instantiate(prefab, new Vector3(0f , 0f, 0f), Quaternion.Euler(0f, 90f, 0f));
            Destroy(model, 3f);
            cameraTransform.Rotate(prefabPosition.transform.rotation.x, prefabPosition.transform.rotation.y + 90, prefabPosition.transform.rotation.z);
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
            yield return new WaitForSeconds(3f);
        }
    }
    
}
