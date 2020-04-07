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
    private GameObject sphereInstance;
    private GameObject model;
    private Transform cameraTransform;
    private Transform start;
    private float timer = 4f;
    //Variables qui permettent de "compter" le nombre de rotation, possibilité de changer
    //la façon que j'ai utilisé qui n'est pas vraiment simple à comprendre
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
            model = Instantiate(prefab, new Vector3(0f , 0f, -1f), Quaternion.Euler(0f, 90f, 0f));
            sphereInstance = Instantiate(sphere, new Vector3(3f, 1.25f, 0.56f), Quaternion.Euler(0,0,0));
            Destroy(model, timer);

            //Permet de fixer la limite du nombre d'enregistrement, ici 16 par exemple
            if(rotateCounter_y%4 == 0 && rotateCounter_y != 0){
                rotateCounter_x += 1;
                rotateCounter_y = 0;
                prefabPosition.transform.Rotate(10, 0, 0);
            }
            //Change l'angle de la camera, avec un angle à préciser (ici 10)
            cameraTransform.Rotate(prefabPosition.transform.rotation.x, prefabPosition.transform.rotation.y + 10, prefabPosition.transform.rotation.z);
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
