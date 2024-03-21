using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToTarget : Agent
{
    [SerializeField] private Transform target;

    [SerializeField] private Transform Enemy;
    public SpriteRenderer backgroundSpriteRenderer;
    

    // Declare ray perception sensor
    // public RayPerceptionSensorComponent3D rayPerception;

    public Rigidbody rBody;

    private int stepCount;



    public override void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        backgroundSpriteRenderer.color = Color.white;
        stepCount = 0;
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        target.localPosition = new Vector3(Random.value * 8 - 4, 2.2f, Random.value * 25 - 4);

        this.transform.localPosition = new Vector3(2.0f, 2.14f, 2.0f);
        
    //     Enemy.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 30 - 4);
    //     this.transform.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    // }
    }

    private RayPerceptionSensorComponent3D GetRayPerceptionSensor()
    {
        // Tenta encontrar o componente RayPerceptionSensorComponent3D neste objeto ou nos seus filhos
        RayPerceptionSensorComponent3D rayPerceptionSensor = GetComponent<RayPerceptionSensorComponent3D>();

        // Se o componente não for encontrado, imprime uma mensagem de aviso e retorna null
        if (rayPerceptionSensor == null)
        {
            Debug.LogWarning("Ray Perception Sensor não encontrado no objeto do agente.");
            return null;
        }

        return rayPerceptionSensor;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(target.position);
        sensor.AddObservation(this.transform.position);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);

        sensor.AddObservation((target.position - this.transform.position).normalized);
        // sensor.AddObservation(GetRayPerceptionSensor().RaySensor.Perceive());
        // GetRayPerceptionSensor().WriteRaycast(sensor);
        // CollectRayPerceptionSensor(sensor);
        // sensor.addObservation(GetRayPerceptionSensor());

        // Enemy position
        // sensor.AddObservation(Enemy.position);

        // Enemy velocity
        // sensor.AddObservation(Enemy.GetComponent<Rigidbody>().velocity.x);
        // sensor.AddObservation(Enemy.GetComponent<Rigidbody>().velocity.z);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        stepCount++;
        // Actions
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        rBody.AddForce(new Vector3(moveX, 0, moveZ) * 10);

        if (stepCount > 1500){
            AddReward(-10.0f);
            EndEpisode();
        }

    }

    // Update debugging
    // private void Update()
    // {
    //     Debug.Log(GetRayPerceptionSensor().RaySensor);
    // }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target"){
            AddReward(100.0f);
            backgroundSpriteRenderer.color = Color.green;
            this.transform.localPosition = new Vector3(2.0f, 2.14f, 2.0f);
            // EndEpisode();
        }
        // else if (other.tag == "PickUp"){
        //     SetReward(-1.0f);
        //     backgroundSpriteRenderer.color = Color.red;
        //     EndEpisode();
        // }
        else if (other.tag == "Enemy"){
            AddReward(-10.0f);
            backgroundSpriteRenderer.color = Color.red;
            EndEpisode();
        }
        else if (other.tag == "Wall_0"){
            AddReward(-1.0f);
            backgroundSpriteRenderer.color = Color.red;
            EndEpisode();
        }

        else if (other.tag == "TargetMax"){
            AddReward(100.0f);
            backgroundSpriteRenderer.color = Color.green;
            EndEpisode();
        }
    }

    
}
