using UnityEngine;
using UnityEditor;

public class DartboardGeneratorExecutor
{
    [MenuItem("Tools/Generate Dartboard")]
    public static void ExecuteGeneration()
    {
        DartboardGenerator generator = GameObject.Find("Dartboard")?.GetComponent<DartboardGenerator>();
        if (generator != null)
        {
            generator.GenerateDartboard();
            Debug.Log("Dartboard generation executed via Tools menu");
        }
        else
        {
            Debug.LogError("Dartboard GameObject with DartboardGenerator component not found!");
        }
    }
}