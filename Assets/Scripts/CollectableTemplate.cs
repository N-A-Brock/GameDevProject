using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTemplate : MonoBehaviour
{
    public ItemObject templateObject;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;


    private void OnEnable()
    {
        meshFilter = this.GetComponent<MeshFilter>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        boxCollider = this.GetComponent<BoxCollider>();

    }

    private void Start()
    {
        meshFilter.sharedMesh = templateObject.itemObject.GetComponent<MeshFilter>().sharedMesh;
        meshRenderer.sharedMaterial = templateObject.itemObject.GetComponent<MeshRenderer>().sharedMaterial; //This doesnt work if there are multiple materials.

        boxCollider.center = templateObject.colliderCenter;
        boxCollider.size = templateObject.colliderSize;
    }
}
