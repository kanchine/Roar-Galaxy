using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour {

    private const string PLANET_DATA_URL = "http://localhost/roargalaxy/load_planet_data.php";

    private string[] planetData;
    private ArrayList myNodes;
    private ArrayList planets;
    private HashSet<Coordinate> coordinates;

    private float boundaryCenterX = 0f;
    private float boundaryCenterY = 0f;
    private float boundaryCenterZ = 0f;

    private float boundaryScale;

    //private Button refreshButton;

    // Use this for initialization
    IEnumerator Start () {

        MeshCollider boundary = GameObject.Find("SphereBoundary").GetComponent<MeshCollider>();
        //refreshButton = GameObject.Find("RefreshButton").GetComponent<Button>();

        Mesh mesh = boundary.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        boundaryScale = Vector3.Distance(boundary.bounds.center, vertices[0]) * boundary.transform.lossyScale[0];

        yield return StartCoroutine(LoadData(value => planetData = value));

        addPlanets();

        LoadPlanets();
    }

    IEnumerator ReloadData()
    {
        yield return StartCoroutine(LoadData(value => planetData = value));

        addPlanets();

        LoadPlanets();
    }

    public void Refresh()
    {
        //ReloadData();
        SceneManager.LoadScene("RoarGalaxy");
    }

    private IEnumerator LoadData(System.Action<string[]> result)
    {
        WWW rawPlanetData = new WWW(PLANET_DATA_URL);
        yield return rawPlanetData;

        string planetDataString = rawPlanetData.text;
        planetData = planetDataString.Split(';');

        result(planetData);
    }

    private void addPlanets()
    {
        System.Random rand = new System.Random();

        coordinates = new HashSet<Coordinate>();
        planets = new ArrayList();

        for (int i = 0; i < planetData.Length - 1; ++i)
        {
            Coordinate coor = new Coordinate(boundaryCenterX, boundaryCenterY, boundaryCenterZ, boundaryScale, rand);
            while (coordinates.Contains(coor))
            {
                coor.GenerateCoordinate();
            }

            coordinates.Add(coor);

            string[] currPlanetData = planetData[i].Split('|');
            string id = GetDataValue(currPlanetData[0], "id:");
            string filesource = GetDataValue(currPlanetData[1], "filesource:");
            string username = GetDataValue(currPlanetData[2], "username:");

            planets.Add(new Planet(id, filesource, username, coor));
        }
    }

    private void LoadPlanets()
    {
        myNodes = new ArrayList();
        foreach (Planet p in planets)
        {
            myNodes.Add(p.GetPlanetForRender());
        }
    }

    private string GetDataValue(string data,string key)
    {
       string value = data.Substring(data.IndexOf(key) + key.Length);
       return value;
    }
}
