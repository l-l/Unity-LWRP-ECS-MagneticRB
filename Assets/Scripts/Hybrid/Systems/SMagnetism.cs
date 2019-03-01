using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
public class SMagnetism : ComponentSystem
{
    // all the components used by the system
  
    private struct ApplyWindGroup
    {
        public Transform transform;
        public CApplyMagnetismRB applyMagnetismRB;
        public Rigidbody rigidBody;
    }
    private struct MagnetGroup
    {
        public Transform transform;
        public CMagnetRB magnet;
        public Rigidbody rigidBody;
    }

    protected override void OnUpdate()
    {
       
        foreach (var EMagnet in GetEntities<MagnetGroup>())
        { 

            foreach (var entity in GetEntities<ApplyWindGroup>())
            {
                var d = Vector3.Distance(entity.transform.position, EMagnet.transform.position);
               // Debug.Log("dist=d" + d);
                if (d<EMagnet.magnet.maxRange)
                {
                    float tDistance = Mathf.InverseLerp(EMagnet.magnet.maxRange, 0f, d);
                    float strength = Mathf.Lerp(0f, EMagnet.magnet.maxStrength, tDistance);
                    Vector3 dir = (EMagnet.transform.position - entity.transform.position).normalized;
                    entity.rigidBody.AddForce(dir * strength, ForceMode.Force);
                }



            }
        }
    }
    
}