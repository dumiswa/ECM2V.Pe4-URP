using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    private bool hasCollided = false;

    public GameObject firePrefab;

    public ParticleSystem orangePart;
    public ParticleSystem smokePart;

    private void OnParticleCollision(GameObject other)
    {

        if (!hasCollided)
        {
            ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[100];
            ParticleSystem particleSystem = GetComponent<ParticleSystem>();
            int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

            Vector3 collisionPosition = collisionEvents[0].intersection;
            Debug.Log("Collision at position: " + collisionPosition);

            Vector3 spawnPoint = new Vector3(collisionPosition.x + (other.transform.position.x - collisionPosition.x) / 1.5f, collisionPosition.y - 0.2f , collisionPosition.z + (other.transform.position.z - collisionPosition.z) / 1.5f);

            Instantiate(firePrefab, spawnPoint, Quaternion.Euler(-90,0,0));

            smokePart = firePrefab.transform.GetChild(0).GetComponent<ParticleSystem>();
            orangePart = firePrefab.transform.GetChild(1).GetComponent<ParticleSystem>();

            hasCollided = true;

        }
    }

    void Update()
    {
        /*if (hasCollided) 
        {
            ParticleSystem.ShapeModule smokeShape = smokePart.shape;
            smokeShape.radius += 0.01f;
            //smokeShape.radius = Mathf.Clamp(smokeShape.radius, 1, 4f);

            ParticleSystem.ShapeModule orangeShape = orangePart.shape;
            orangeShape.radius += 0.01f;
            //orangeShape.radius = Mathf.Clamp(orangeShape.radius, 1, 4f);
        }*/
    }



}
