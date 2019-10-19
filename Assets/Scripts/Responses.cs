public class HelloWorld
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
    public Stats st_rad;
}