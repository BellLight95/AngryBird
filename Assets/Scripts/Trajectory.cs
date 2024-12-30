using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject _Trajectory;
    public List<GameObject> Objects = new List<GameObject>();
    public Slider Slider;
    public float Mass = 1.0f;
    public int maxStep = 20;
    public float timeStep = 0.1f;
    
    List<Vector3> PredictTrajectory(Vector3 force, float mass)
    {
        List<Vector3> trajectory = new List<Vector3>();
        
        Vector3 position = lineRenderer.GetPosition(1);
        Vector3 acceleration = force / mass;

        trajectory.Add(position);

        for (int i = 1; i <= maxStep; i++)
        {
            float timeElapsed = timeStep * i;
            trajectory.Add(position + 
                           acceleration * timeElapsed + 
                           Physics.gravity * (0.5f * timeElapsed * timeElapsed));
        }

        return trajectory;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentForce = lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1);
        Slider.value = (currentForce.magnitude - 0.5f);
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            List<Vector3> trajectories = PredictTrajectory(currentForce * SlingShot.PowerMultiplier, Mass);
            foreach (var o in Objects)
            {
                Destroy(o);
            }
            
            Objects.Clear();
            
            foreach (var trajectory in trajectories)
            {
                var go = Instantiate(_Trajectory, trajectory, Quaternion.identity);
                Objects.Add(go);
            }
        }

        foreach (var o in Objects)
        {
            o.SetActive(false);
        }
        
        List<Vector3> trajectories2 = PredictTrajectory(currentForce * SlingShot.PowerMultiplier, Mass);
        if (Objects.Count == trajectories2.Count)
        {
            for (var index = 0; index < trajectories2.Count; index++)
            {
                var trajectory = trajectories2[index];
                Objects[index].SetActive(true);
                Objects[index].transform.position = trajectory;
            }
        }

    } 
    
}
