using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;

public class PieceAgent : Agent
{
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {

    }
    public override void OnEpisodeBegin()
    {
        return;
    }
    // sends the information we collect to the Brain
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Target.localPosition);
    }
    /*public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (true)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        if (false)
        {
            EndEpisode();
        }
    }*/
}
