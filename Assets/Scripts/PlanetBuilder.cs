using UnityEngine;

public class PlanetBuilder : MonoBehaviour
{
    public Planet planet;

    public void GoToPositionEdition()
    {
        planet.editionMode = Planet.EditionModes.POSTIION;
    }

    public void GoToSizeEdition()
    {
        planet.editionMode = Planet.EditionModes.SIZE;
    }

    public void GoToRotationEdition()
    {
        planet.editionMode = Planet.EditionModes.TILT;
    }
}
