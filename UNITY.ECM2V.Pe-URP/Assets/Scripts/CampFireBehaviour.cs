using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireBehaviour : MonoBehaviour
{

    public int amountOfLogs = 0;

    public GameObject groupOfParticles;

    public ParticleSystem orangePart;
    public ParticleSystem smokePart;

    void Start()
    {
        smokePart = groupOfParticles.transform.GetChild(0).GetComponent<ParticleSystem>();
        orangePart = groupOfParticles.transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //var smokeMain = smokePart.main;
        //var orangeMain = orangePart.main;

        //smokeMain.startLifetime = 0.3f + amountOfLogs+2;
        //orangeMain.startLifetime = 0.3f + amountOfLogs+2;

        //smokeMain.startSpeed.constantMin *= 1.5f;
        //smokeMain.startSpeed.constantMax *= 1.5f;

        //orangeMain.startSpeed.constantMin *= 1.5f;
        //orangeMain.startSpeed.constantMax *= 1.5f;

       

    }

    public void AddLogs()
    {
        if (amountOfLogs == 0) {
            groupOfParticles.SetActive(true);
        }

        var smokeMain = smokePart.main;
        var orangeMain = orangePart.main;

        smokeMain.startLifetime = new ParticleSystem.MinMaxCurve(0.3f + amountOfLogs + 2);
        orangeMain.startLifetime = new ParticleSystem.MinMaxCurve(0.3f + amountOfLogs + 2);

        ParticleSystem.MinMaxCurve smokeSpeedCurve = smokeMain.startSpeed;
        //smokeSpeedCurve.constantMin *= 1.5f;
        smokeSpeedCurve.constantMax *= 1.5f;
        smokeMain.startSpeed = smokeSpeedCurve;

        ParticleSystem.MinMaxCurve orangeSpeedCurve = orangeMain.startSpeed;
        //orangeSpeedCurve.constantMin *= 1.5f;
        orangeSpeedCurve.constantMax *= 1.5f;
        orangeMain.startSpeed = orangeSpeedCurve;

        ParticleSystem.ShapeModule smokeShape = smokePart.shape;
        smokeShape.radius += 0.2f;
        smokeShape.radius = Mathf.Clamp(smokeShape.radius, 1, 2.2f);

        ParticleSystem.ShapeModule orangeShape = orangePart.shape;
        orangeShape.radius += 0.2f;
        orangeShape.radius = Mathf.Clamp(orangeShape.radius, 1, 2.2f);

        amountOfLogs++;
    }
}
