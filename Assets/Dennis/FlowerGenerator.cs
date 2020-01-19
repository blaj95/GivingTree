using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGenerator : MonoBehaviour
{

	public DonationManager donationManager;
	public GameObject[] plantPrefabs;
	
	public int numPlants;

	public float minSpawnDistance; //from tree
	public float maxSpawnDistance; //from tree


	private List<GameObject> spawnedPlants;


	public int numBloomTiers;

	private int curBloomTier;

	private float averageNumNewPlantsPerTier;

    public Transform treeSpawnPointsParent;
    public Transform groundPlane, tree;
    public bool onTree; //or on ground?


    // Start is called before the first frame update
    void Start()
    {
		curBloomTier = 0;
		averageNumNewPlantsPerTier = (float)numPlants / numBloomTiers;
		spawnedPlants = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            NextBloom();
        }
    }


    public void NextBloom()
	{
		curBloomTier += 1;

		int numSpawns = (int)(averageNumNewPlantsPerTier + (averageNumNewPlantsPerTier * (Random.value - 0.5f)));
		numSpawns = Mathf.Clamp(numSpawns, (int)averageNumNewPlantsPerTier / 2, (int)averageNumNewPlantsPerTier * 2);

        for(int i = 0; i < numSpawns; i+=1)
		{
            Vector3 spawnPoint;

            if (onTree)
            {
                int spawnTransformIndex = Random.Range(0, donationManager.leafPoints.Count-1);
                spawnPoint = treeSpawnPointsParent.GetChild(spawnTransformIndex).position;
                donationManager.leafPoints.RemoveAt(spawnTransformIndex);
               // Destroy(treeSpawnPointsParent.GetChild(spawnTransformIndex).gameObject);
            }
            else
            {
                Vector2 spawnPoint2D = Random.insideUnitCircle * (maxSpawnDistance - minSpawnDistance);
                spawnPoint = new Vector3(spawnPoint2D.x, groundPlane.position.y, spawnPoint2D.y);
                //Vector3 localSpawn = spawnPoint;
                spawnPoint +=  spawnPoint.normalized * minSpawnDistance;
            }

           
            

            if (!onTree) //make further plants more transparent
            {
	            GameObject newPlant = Instantiate(plantPrefabs[Random.Range(0, plantPrefabs.Length)]);
	            newPlant.transform.position = spawnPoint;
	            
	            spawnedPlants.Add(newPlant);
	            for (int ind = 0; ind < newPlant.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials.Length; ind++)
                {
                    Color color = newPlant.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials[ind].color;
                    color.a = Mathf.SmoothStep(1f, 0f, Vector3.Magnitude(newPlant.transform.localPosition) / 3f);
                    //Material newMat = new Material(newPlant.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials[ind]);
                    //newMat.shader = Shader.Find("Custom/PlantStandard");
                    //newMat.color = color;
                    //newMat.renderQueue = 3000;
                    //newPlant.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials[ind] = newMat;
                    newPlant.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials[ind].color = color;

                }
            }
            else
            {
	            GameObject newPlant = Instantiate(plantPrefabs[Random.Range(0, plantPrefabs.Length)], spawnPoint, Quaternion.identity);
	            spawnedPlants.Add(newPlant);
            }
		}

        GrowAllPlants();
	}

    private void GrowAllPlants()
    {
        for(int i = 0; i < spawnedPlants.Count; i += 1)
        {
            float proportionFromGroundToBranchEnd = Vector3.Distance(Vector3.zero, spawnedPlants[i].transform.position) / 7f;
            float nextScale =  (1f + (proportionFromGroundToBranchEnd * 2f)) * ((float)curBloomTier / (float)numBloomTiers) ;  //scales leaves proportionally depending on how far they are from the base of the tree. Final scale is clamped between range of 1 to 3
            if (!onTree) nextScale = nextScale / 2f;
            spawnedPlants[i].GetComponent<PlantGrowth>().Grow((Mathf.Clamp(nextScale, 0f, 3f)), Random.value * 2f);
        }
    }


    public void FinalBloom()
	{

	}




}
