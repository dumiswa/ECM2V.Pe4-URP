using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireBehaviour : MonoBehaviour
{

    public int amountOfLogs = 0;

    public GameObject groupOfParticles;

    public ParticleSystem orangePart;
    public ParticleSystem smokePart;
        
    public AudioSource campFireSound;
    public AudioSource placeWoodSoound;

    bool toSpread = false;

    void Start()
    {
        smokePart = groupOfParticles.transform.GetChild(0).GetComponent<ParticleSystem>();
        orangePart = groupOfParticles.transform.GetChild(1).GetComponent<ParticleSystem>();       
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.R))
       {
            toSpread = true;
       }

       if (toSpread)
        {
            ParticleSystem.ShapeModule smokeShape = smokePart.shape;
            smokeShape.radius += 0.02f;

            ParticleSystem.ShapeModule orangeShape = orangePart.shape;
            orangeShape.radius += 0.02f;

            ModifyRateOverTime(smokePart, smokeShape.radius / 0.8f);
            ModifyRateOverTime(orangePart, orangeShape.radius / 0.8f);
        }
    }

    public void AddLogs()
    {
        if (amountOfLogs == 0) {
            groupOfParticles.SetActive(true);
            placeWoodSoound.Play();
        }

        if (amountOfLogs <=0)
        {
            campFireSound.Play();
        }
      

        var smokeMain = smokePart.main;
        var orangeMain = orangePart.main;

        smokeMain.startLifetime = new ParticleSystem.MinMaxCurve(0.3f + amountOfLogs + 2.5F);
        orangeMain.startLifetime = new ParticleSystem.MinMaxCurve(0.3f + amountOfLogs + 2.5F);

        ParticleSystem.MinMaxCurve smokeSpeedCurve = smokeMain.startSpeed;
        //smokeSpeedCurve.constantMin *= 1.5f;
        smokeSpeedCurve.constantMax *= 2f;
        smokeMain.startSpeed = smokeSpeedCurve;

        ParticleSystem.MinMaxCurve orangeSpeedCurve = orangeMain.startSpeed;
        //orangeSpeedCurve.constantMin *= 1.5f;
        orangeSpeedCurve.constantMax *= 2f;
        orangeMain.startSpeed = orangeSpeedCurve;

        ParticleSystem.ShapeModule smokeShape = smokePart.shape;
        smokeShape.radius += 0.2f;
        smokeShape.radius = Mathf.Clamp(smokeShape.radius, 1, 2.2f);

        ParticleSystem.ShapeModule orangeShape = orangePart.shape;
        orangeShape.radius += 0.2f;
        orangeShape.radius = Mathf.Clamp(orangeShape.radius, 1, 2.2f);

        amountOfLogs++;   
    }

    void ModifyRateOverTime(ParticleSystem particleSystem, float rateChange)    
    {
        var emission = particleSystem.emission;

        ParticleSystem.MinMaxCurve rateCurve = emission.rateOverTime;
        rateCurve.constantMax = 26 * rateChange;
        emission.rateOverTime = rateCurve;
    }
}
