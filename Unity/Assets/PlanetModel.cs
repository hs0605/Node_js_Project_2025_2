using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class PlanetModel
{
    public int id;
    public string name;
    public int metal;
    public int crystal;
    public int deuterium;

    public PlanetModel(int id, string name)
    {
        this.id = id;
        this.name = name;
        this.metal = 500;
        this.crystal = 300;
        this.deuterium = 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
