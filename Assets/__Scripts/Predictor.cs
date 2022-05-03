using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Predictor : MonoBehaviour {

    private static Predictor _instance;

    public static Predictor Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public int maxIterations;

    Scene currentScene;
    Scene predictionScene;

    PhysicsScene currentPhysicsScene;
    PhysicsScene predictionPhysicsScene;

    LineRenderer lineRenderer;
    GameObject dummy;

    [SerializeField]
    private GameObject arrowPrefab;

    void Start(){
        Physics.autoSimulation = false;

        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Prediction", parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();

        lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate(){
        if (currentPhysicsScene.IsValid()){
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

    public void predict(Transform arrow, float force){
        
        if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid()){
            if(dummy == null){
                dummy = Instantiate(arrowPrefab);
                SceneManager.MoveGameObjectToScene(dummy, predictionScene);
            }

            print("predicting");
            
            dummy.transform.position = arrow.position;
            dummy.transform.rotation= arrow.rotation;

            var power = transform.right*force;

            Rigidbody rb= dummy.GetComponent<Rigidbody>();
            rb.isKinematic=false;
            rb.AddForce(power,ForceMode.Impulse);
            
            // //add rotation
            rb.AddForce(transform.right * 5);
            transform.SetParent(null);

            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = maxIterations;


            for (int i = 0; i < maxIterations; i++){
                predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
                lineRenderer.SetPosition(i, dummy.transform.position);
            }

            Destroy(dummy);
        }
    }

}