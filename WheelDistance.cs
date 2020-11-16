using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class WheelDistance : Deduction
{
    [SerializeField] 
    private DistanceSensor[] _sensors;
    [SerializeField]
    private GameObject _left_debuging_sensor;
    [SerializeField]
    private GameObject _right_debuging_sensor;
    private float _whole_wheel_distance = 1.945687f; //left 1.116196 + right 0.829491
    private float _track_distance = 4.265f;

    private float persent = 0f;
    
    [SerializeField]
    public Text distanceText;
   private void Awake()
    {
        for(int i = 0; i<_sensors.Length; i++)
        {
            _sensors[i].GetComponent<DistanceSensor>();
        }
    }

    private void Start()
    {
        for(int i = 0; i<_sensors.Length; i++)
        {
            _sensors[i].GetComponent<DistanceSensor>().ray_hit_event.AddListener(CalculateAverage);
            _sensors[i].distance_list.Clear();
            _sensors[i].gameObject.SetActive(true);
        }
        distanceText.text = "평균값 : " + persent+ "%";
    }
    public override void DoTest()
    {
        if(persent <5)
            deductionScore = 5;
    }

    private void DebugEachDistance()
    {
        Debug.Log(Vector3.Distance(_left_debuging_sensor.transform.position,_right_debuging_sensor.transform.position));
        int layer_mask = 1 << LayerMask.NameToLayer("Wheel");
        RaycastHit hit;
        if(Physics.Raycast(_left_debuging_sensor.transform.position, _right_debuging_sensor.transform.position-_left_debuging_sensor.transform.position,out hit,100f,layer_mask))
        {
            if (hit.collider.CompareTag("LeftWheel"))
            {
                Vector3 from = hit.collider.gameObject.transform.position;
                from.y = 0;
                Vector3 to = _left_debuging_sensor.transform.position;
                to.y = 0;
                Debug.Log("Left : "+Vector3.Distance(from,to));
            }
        } 
        
        if(Physics.Raycast(_right_debuging_sensor.transform.position, _left_debuging_sensor.transform.position-_right_debuging_sensor.transform.position,out hit,100f,layer_mask))
        {
            if (hit.collider.CompareTag("RightWheel"))
            {
                Vector3 from = hit.collider.gameObject.transform.position;
                from.y = 0;
                Vector3 to = _right_debuging_sensor.transform.position;
                to.y = 0;
                Debug.Log("Right : "+Vector3.Distance(from,to));
            }
        }

    }
    public void CalculateAverage()
    {
        float sum = 0;
        int count = 0;
        foreach (var i in _sensors)
        {
            for (int j = 0; j < i.distance_list.Count; j++)
            {
                float left = i.distance_list[j];
                float right = _whole_wheel_distance - left;
                Debug.Log(left);
                Debug.Log(right);
                float distance = left < right ? left : right;

                Debug.Log(distance);
                sum += distance;
                count++;
            }
        }
        Debug.Log("SUM :" + sum + " Count :" + count + " 평균값 : " + persent+ "%");
        persent = (sum/count)/_track_distance *100;
        distanceText.text = "평균값 : " + persent+ "%";
    }
}
