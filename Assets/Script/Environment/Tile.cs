using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public enum Type
    {
        Walkable,
        Building,

    }

    [SerializeField] Type tileType;
    public void SetTileType(Type type) { tileType = type; UpdateTileType(); }

    [Header("Reference")]
    [SerializeField] BoxCollider _collider;
    [SerializeField] MeshRenderer _meshRenderer;

    [Header("Walkable")]
    public Material material_Walkable;

    [Header("Building")]
    public Material material_Building;


    void Awake()
    {
       
    }

    void Start()
    {
       

    }

    void Update()
    {
        
    }

    void UpdateTileType()
    {
        switch (tileType)
        {
            case Type.Walkable:
                _meshRenderer.material = material_Walkable;
                _collider.enabled = false;
                break;
            case Type.Building:
                _meshRenderer.material = material_Building;
                _collider.enabled = true;
                break;
        }
    }
}
