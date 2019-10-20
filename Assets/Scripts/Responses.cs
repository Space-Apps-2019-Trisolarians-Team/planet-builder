﻿public class HelloWorld
{
    public string Hello;
}

public class RandomStarResponse
{
    public string st_spstr;
    public float st_age;
    public float st_mass;
    public float st_rad;
    public float st_teff;
    public float st_lum;
}

[System.Serializable]
public class Stats
{
    public float count;
    public float mean;
    public float std;
    public float min;
    public float max;
    public float max95;
    public float min95;
}

public class StarStatsResponse
{
    public Stats st_age;
    public Stats st_mass;
    public Stats st_rad;
    public Stats st_teff;
    public Stats st_lum;
}

public class PlanetStatsResponse
{
    public Stats pl_rade;
    public Stats pl_ratdor;
    public Stats pl_masse;
    public Stats pl_orbsmax;
}

public class SimilarExoplanetResponse
{
    public float distance;
    public float pl_orbsmax;
    public string pl_hostname;
    public string pl_name;
    public float pl_masse;
    public float pl_rade;
    public float st_rad;
    public float pl_ratror;
    public string pl_pelink;
    public bool in_hz;
}