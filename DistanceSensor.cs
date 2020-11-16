using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceSensor : MonoBehaviour
{
    [SerializeField] 
    private GameObject _gizmo; 
    [SerializeField] 
    private GameObject _start;
    [SerializeField] 
    private GameObject _end;
    private RaycastHit _hitInfo;

    private bool _is_active = true;
    private int _layer_mask;
    
    public UnityEvent ray_hit_event;
    public List<float> distance_list;

    private void Awake()
    {
        ray_hit_event = new UnityEvent();
        distance_list = new List<float>();
    }

    void Start()
    {
        _gizmo = Instantiate(_gizmo);
        _gizmo.SetActive(false);
    }

    private void OnEnable()
    {
        _is_active = true;
        _layer_mask = 1 << LayerMask.NameToLayer("Wheel");
    }

    void Update()
    {
        if(_is_active)
        {   
            if (Physics.Raycast(_start.transform.position, _end.transform.position - _start.transform.position
                ,out _hitInfo,100f,_layer_mask))
            {
                if (_hitInfo.collider.CompareTag("LeftWheel"))
                {
                    _is_active = false;
                    distance_list.Add(GetDistanceToWheelByRaycast(_hitInfo));
                    ActivateGizmo();
                    ray_hit_event.Invoke();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnDisable()
    {
        _is_active = false;
    }

    private void ActivateGizmo()
    {
        _gizmo.SetActive(true);
        _gizmo.transform.position = _hitInfo.collider.transform.position;
    }

    private float GetDistanceToWheelByRaycast(RaycastHit hit)
    {
        Vector3 from = _start.transform.position;
        from.y = 0f;
        Vector3 to = hit.collider.transform.position;
        to.y = 0f;
        return Vector3.Distance(from, to);
    }
}
