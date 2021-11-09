using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessNPC : MonoBehaviour
{
    SkinnedMeshRenderer meshRenderer;
    public Material normalMaterial;
    public Material possessMaterial;

    void Start()
    {
        meshRenderer = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMaterial(bool possess)
    {
        if (possess)
        {
            meshRenderer.material = possessMaterial;

        }
        else
        {
            meshRenderer.material = normalMaterial;
        }

    }
}
