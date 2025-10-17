using UnityEngine;

public class DartboardGenerator : MonoBehaviour
{
    // Dartboard dimensions (in meters, for 0.45m diameter board)
    private const float totalRadius = 0.225f;
    private const float doubleRadius = 0.21f;
    private const float tripleRadius = 0.105f;
    private const float bullseyeRadius = 0.015f;
    private const float boardThickness = 0.01f;

    // Materials
    public Material blackSegmentMat;
    public Material creamSegmentMat;
    public Material redScoringMat;
    public Material greenScoringMat;
    public Material wireDividerMat;
    public Material backingBoardMat;

    [ContextMenu("Generate Dartboard")]
    public void GenerateDartboard()
    {
        // Clear existing children
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // Create simple layered dartboard
        CreateBackingBoard();
        CreateMainBoard();
        CreateDoubleRing();
        CreateTripleRing();
        CreateBullseye();

        Debug.Log("Simple dartboard generated successfully!");
    }

    private void CreateBackingBoard()
    {
        GameObject backing = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        backing.name = "BackingBoard";
        backing.transform.SetParent(transform);
        backing.transform.localPosition = new Vector3(0, 0, -boardThickness * 2);
        backing.transform.localRotation = Quaternion.Euler(90, 0, 0);
        backing.transform.localScale = new Vector3(totalRadius * 2.15f, boardThickness, totalRadius * 2.15f);

        if (backingBoardMat != null)
        {
            backing.GetComponent<Renderer>().material = backingBoardMat;
        }

        DestroyImmediate(backing.GetComponent<Collider>());
    }

    private void CreateMainBoard()
    {
        GameObject mainBoard = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        mainBoard.name = "MainBoard";
        mainBoard.transform.SetParent(transform);
        mainBoard.transform.localPosition = Vector3.zero;
        mainBoard.transform.localRotation = Quaternion.Euler(90, 0, 0);
        mainBoard.transform.localScale = new Vector3(totalRadius * 2f, boardThickness / 2f, totalRadius * 2f);

        if (creamSegmentMat != null)
        {
            mainBoard.GetComponent<Renderer>().material = creamSegmentMat;
        }

        DestroyImmediate(mainBoard.GetComponent<Collider>());
    }

    private void CreateDoubleRing()
    {
        GameObject doubleRing = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        doubleRing.name = "DoubleRing";
        doubleRing.transform.SetParent(transform);
        doubleRing.transform.localPosition = new Vector3(0, 0, boardThickness * 0.5f);
        doubleRing.transform.localRotation = Quaternion.Euler(90, 0, 0);
        doubleRing.transform.localScale = new Vector3(doubleRadius * 2f, boardThickness / 2f, doubleRadius * 2f);

        if (blackSegmentMat != null)
        {
            doubleRing.GetComponent<Renderer>().material = blackSegmentMat;
        }

        DestroyImmediate(doubleRing.GetComponent<Collider>());
    }

    private void CreateTripleRing()
    {
        GameObject tripleRing = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        tripleRing.name = "TripleRing";
        tripleRing.transform.SetParent(transform);
        tripleRing.transform.localPosition = new Vector3(0, 0, boardThickness * 1.0f);
        tripleRing.transform.localRotation = Quaternion.Euler(90, 0, 0);
        tripleRing.transform.localScale = new Vector3(tripleRadius * 2f, boardThickness / 2f, tripleRadius * 2f);

        if (redScoringMat != null)
        {
            tripleRing.GetComponent<Renderer>().material = redScoringMat;
        }

        DestroyImmediate(tripleRing.GetComponent<Collider>());
    }

    private void CreateBullseye()
    {
        GameObject bullseye = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        bullseye.name = "Bullseye";
        bullseye.transform.SetParent(transform);
        bullseye.transform.localPosition = new Vector3(0, 0, boardThickness * 1.5f);
        bullseye.transform.localRotation = Quaternion.Euler(90, 0, 0);
        bullseye.transform.localScale = new Vector3(bullseyeRadius * 2f, boardThickness / 2f, bullseyeRadius * 2f);

        if (greenScoringMat != null)
        {
            bullseye.GetComponent<Renderer>().material = greenScoringMat;
        }

        DestroyImmediate(bullseye.GetComponent<Collider>());
    }
}