using UnityEngine;

/**
 * An interface used to make something cuttable by a cutting weapon
 */
public interface ICuttable
{
    /**
     * Called when an object is cut by the knife weapon
     * 
     * cutter (GameObject): The player using the knife weapon
     */
    void Cut(GameObject cutter);
}
