using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Brick[] brickPrefabs;
    public float coordScaleFactor = 0.17f;
    private bool proceduralMode = false;

    public void StartProcedural() {
        LoadLevel("Level_Proc");
        proceduralMode = true;
        GenerateLevel();
    }

    public void LoadLevel(string name) {
		Debug.Log ("New Level load: " + name);
        SceneManager.LoadScene(name);
	}

	public void QuitRequest() {
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadNextLevel() {
        // Win_Screen is the sceen after the last level in the buildindex.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void BrickDestroyed() {
        if (Brick.breakableBrickCount <= 0) {
            if (proceduralMode) {
                Brick brick;
                //remove unbreakable bricks
                while(brick = GameObject.FindObjectOfType<Brick>()){
                    Destroy(brick);
                }
                GenerateLevel();
            } else {
                LoadNextLevel();
            }
        }
    }

    public void GenerateLevel() {
        int gridWidth = 14, gridHeight = 9;
        int[,] PerlinGrid = new int[gridWidth,gridHeight];
        float brickTypes = 1.0f * brickPrefabs.Length;
        float seed = Random.Range(0.0f, 100.0f);
        for (int i = 0; i < gridWidth; i++) {
            for (int j = 0; j < gridHeight; j++) {
                PerlinGrid[i, j] = Mathf.RoundToInt(Mathf.PerlinNoise(seed+coordScaleFactor*i, seed+coordScaleFactor*j)*(brickTypes-1));

                //calculate block coordinates
                float xcoord = 1.5f + i;
                float ycoord = 7.0f + j * 0.4666666f;
                if (Instantiate(brickPrefabs[PerlinGrid[i, j]], new Vector3(xcoord, ycoord, 0.0f), Quaternion.identity)) {
                    Debug.Log("Instantiated brick");
                }
            }
        }
    }

}
