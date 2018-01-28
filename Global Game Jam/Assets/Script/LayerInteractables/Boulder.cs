using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Boulder : MovableValidObject {

    //spawner this boulder came from;
    public BoulderSpawner spawner;

    private ParticleSystem particleSys;

    private Material material;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        particleSys = GetComponentInChildren<ParticleSystem>();
        material = GetComponentsInChildren<SpriteRenderer>()[2].material;//third material
	}

    public override void handleDestroy()
    {
        SpriteRenderer[] renders = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renders.Length - 1; i++)//except last one
        {
            Debug.Log("disable");
            renders[i].enabled = false;
        }

        //use shader for effect
        StartCoroutine(shaderDissolveLerp());

        //activate particle sys
        particleSys.Play();
    }

    IEnumerator shaderDissolveLerp()
    {
        rb.isKinematic = true;
        float shaderDissolveThreshold = material.GetFloat("_DissolveThreshold");
        while (shaderDissolveThreshold > 0)
        {
            shaderDissolveThreshold -= Time.deltaTime * 2;
            material.SetFloat("_DissolveThreshold", shaderDissolveThreshold);
            yield return new WaitForEndOfFrame();
        }

        SpriteRenderer[] renders = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renders.Length - 1; i++)//except last one
        {
            renders[i].enabled = true;
        }
        material.SetFloat("_DissolveThreshold", 1.92f);
        rb.isKinematic = false;
        //return back to boulder spawner
        spawner.
            returnBoulder(
            gameObject);
    }
}
