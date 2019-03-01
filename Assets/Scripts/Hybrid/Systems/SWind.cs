using System;
using UnityEngine;
using Unity.Entities;

public class SWind : ComponentSystem
{
    // all the components used by the system


    private struct Group
    {
        public Transform transform;
        public CApplyWindRB applyWindRB;
        public Rigidbody rigidBody;
    }
    private struct WindGroup
    {
        public CWind wind;
        public Transform transform;
    }


    protected override void OnUpdate()
    {
        var windcg = GetEntities<WindGroup>();
        var tempStrength = Mathf.PerlinNoise(Time.time, 0);
       // Debug.Log(tempStrength);
        if (windcg.Length>0)
        {
            foreach (var entity in GetEntities<Group>())
            {
                // out of bounds?
                if (entity.applyWindRB.bringBack)
                {
                    if (entity.transform.position.x > (windcg[0].transform.position.x + windcg[0].wind.bounds.x / 2f))
                    {
                        entity.transform.position = new Vector3(windcg[0].transform.position.x - windcg[0].wind.bounds.x / 2f, entity.transform.position.y, entity.transform.position.z);
                    }
                    if (entity.transform.position.x < (windcg[0].transform.position.x - windcg[0].wind.bounds.x / 2f))
                    {
                        entity.transform.position = new Vector3(windcg[0].transform.position.x + windcg[0].wind.bounds.x / 2f, entity.transform.position.y, entity.transform.position.z);
                    }
                    if (entity.transform.position.y > (windcg[0].transform.position.y + windcg[0].wind.bounds.y / 2f))
                    {
                        entity.transform.position = new Vector3(entity.transform.position.x, windcg[0].transform.position.y - windcg[0].wind.bounds.y / 2f, entity.transform.position.z);
                    }
                    if (entity.transform.position.y < (windcg[0].transform.position.y - windcg[0].wind.bounds.y / 2f))
                    {
                        entity.transform.position = new Vector3(entity.transform.position.x, windcg[0].transform.position.y + windcg[0].wind.bounds.y / 2f, entity.transform.position.z);
                    }
                    if (entity.transform.position.z > (windcg[0].transform.position.y + windcg[0].wind.bounds.z / 2f))
                    {
                        entity.transform.position = new Vector3(entity.transform.position.x, entity.transform.position.y, windcg[0].transform.position.z - windcg[0].wind.bounds.z / 2f);
                    }
                    if (entity.transform.position.z < (windcg[0].transform.position.z - windcg[0].wind.bounds.y / 2f))
                    {
                        entity.transform.position = new Vector3(entity.transform.position.x, entity.transform.position.y, windcg[0].transform.position.z + windcg[0].wind.bounds.z / 2f);
                    }

                }   
               

                
                var locStrength = Mathf.PerlinNoise(entity.transform.position.x, entity.transform.position.z);
                
                entity.rigidBody.AddForce(new Vector3(locStrength * tempStrength*windcg[0].wind.strength, 0, 0),ForceMode.Acceleration);
                


            }
        }
    }
}
