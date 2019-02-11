using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

    public Entity[] entities;

    private void Start()
    {
        if (FindObjectsOfType<Entity>() != null)
        {
            entities = new Entity[FindObjectsOfType<Entity>().Length];
            entities = FindObjectsOfType<Entity>();
        }
    }

}
