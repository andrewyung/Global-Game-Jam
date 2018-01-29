using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : ButtonInteractable {

    [SerializeField]
    private GameObject boulderPrefab;

    [SerializeField]
    private int numberOfBoulders;

    private TextMesh text;

    private ArrayList bouldersInstantiated;

	// Use this for initialization
	void Start () {
        bouldersInstantiated = new ArrayList();

		for (int i = 0; i < numberOfBoulders; i++)
        {
            GameObject newBoulder = Instantiate(boulderPrefab);
            newBoulder.GetComponent<Boulder>().spawner = this;
            newBoulder.SetActive(false);

            bouldersInstantiated.Add(newBoulder);
        }
        text = GetComponentInChildren<TextMesh>();
        text.text = numberOfBoulders.ToString();
    }

    public override void doAction()
    {
        spawnBoulder();
    }

    public void returnBoulder(GameObject boulder)
    {
        //each boulder should have reference to their spawner if there are multiple spawners
        boulder.SetActive(false);
        bouldersInstantiated.Add(boulder);
        boulder.transform.SetParent(null);
        
        text.text = bouldersInstantiated.Count.ToString();
    }
    
    public bool spawnBoulder()
    {
        if (bouldersInstantiated.Count == 0)
        {
            return false;
        }
        //spawn at this spawner location
        GameObject boulderSpawned = (GameObject) bouldersInstantiated[0];
        bouldersInstantiated.RemoveAt(0);

        boulderSpawned.transform.position = transform.position;
        boulderSpawned.SetActive(true);
        boulderSpawned.transform.SetParent(LayerManager.getCurrentLayerGameObject().transform);

        text.text = bouldersInstantiated.Count.ToString();
        return true;
    }
}
