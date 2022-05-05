using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] int miniCubesPerAxis = 8;
    [SerializeField] private float explosionForce = 300f;
    [SerializeField] private float explosionRadius = 2f;

    [SerializeField] private GameObject prefab;


    public void TriggerExplosion()
    {
        //nested for loops to replace original cube with 8 minicubes on every axis
        for (int x = 0; x < miniCubesPerAxis; x++)
        {
            for (int y = 0; y < miniCubesPerAxis; y++)
            {
                for (int z = 0; z < miniCubesPerAxis; z++)
                {
                    CreateCubeExplosion(new Vector3(x, y, z));
                }
            }
        }
        Destroy(gameObject);

    }

    private void CreateCubeExplosion(Vector3 coordinates)
    {
        //Create the cube from prefabs
        GameObject miniCube = GameObject.Instantiate(prefab);

        Renderer renderer = miniCube.GetComponentInChildren<Renderer>();
        //set miniCubes material to the original cubes material
        renderer.material = GetComponentInChildren<Renderer>().material;

        //set the scale of the cube to the original size of the cube/ desired amount of mini cubes
        miniCube.transform.localScale = transform.localScale / miniCubesPerAxis;

        //create a reference vector
        //the position of the original cube - half the size of the original cube + half the size of the mini cube.
        //this gives me a position in 
        Vector3 reference = transform.position - transform.localScale / 2 + miniCube.transform.localScale / 2;

        //use the reference vector and multiply it by the coordinates
        miniCube.transform.position = reference + Vector3.Scale(coordinates, miniCube.transform.localScale);

        // print(reference);
        // print(transform.position+"-"+transform.localScale/2+"+"+miniCube.transform.localScale / 2);

        Rigidbody rb = miniCube.AddComponent<Rigidbody>();

        //add explosion
        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
    }
}
