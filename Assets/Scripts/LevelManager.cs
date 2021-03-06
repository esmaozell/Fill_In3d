using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Animator anim;

    public static LevelManager Instance => instance;
    public static event Action OnFilledCubeAmountChanged;
    public static event Action<int> OnGameFinished;

    public Action LevelCompleted;

    [Space]
    [SerializeField]
    LevelInfoAsset levelInfoAsset;
    [SerializeField]
    StartCube startCube;

    [Space]
    [SerializeField]
    public Transform fillAreaContainer;

    private static LevelManager instance;

    public Material ab;

    int currentLevelIndex = 0;

    [HideInInspector]
    public FillAreaSpawner fillAreaSpawner = new FillAreaSpawner();
    [HideInInspector]
    public List<GameObject> blocksFromImage = new List<GameObject>();
    [HideInInspector]
    public List<StartCube> startedCubes = new List<StartCube>();
    [HideInInspector]
    public List<StartCube> startedCubes2 = new List<StartCube>();
    [HideInInspector]
    public List<GameObject> endGameCubes = new List<GameObject>();

    [Header("Parent")]
    public Transform startCubeTransform;

    public int startCubeShapes;

    [HideInInspector]
    public int filledCubeCount;
    [HideInInspector]
    public int maxStartedCubeAmount = 1;

    public int nextLevelIndex;
    public bool levelFinish;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        fillAreaSpawner = GetComponent<FillAreaSpawner>();

        StartCube.OnCubeDestroyed += OnCubeDestroyed;
    }

    void OnDestroy()
    {
        StartCube.OnCubeDestroyed -= OnCubeDestroyed;
    }

    private bool _one;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) || (!_one && startedCubes.Count <= 0))
        {
            _one = true;
            anim.SetBool("isFinished", true); // todo Renklenen kupler yeni bi gameobject altina child olacak(transform.SetParent()) parent obje animasyon oynayacak. 
            levelFinish = true;
        }

        if (levelFinish && Input.GetMouseButtonDown(0))
        {
            EndGameAnim();
        }
    }

    void OnCubeDestroyed()
    {
        filledCubeCount++;
        OnFilledCubeAmountChanged?.Invoke();
    }

    public float NormalizedStartedFillAmount() => (float)filledCubeCount / maxStartedCubeAmount;

    private float _counter;

    void EndGameAnim()
    {
        for (int i = 0; i < endGameCubes.Count; i++)
        {
            endGameCubes[i].GetComponent<Rigidbody>().isKinematic = false;
            endGameCubes[i].GetComponent<Rigidbody>().useGravity = true;
            endGameCubes[i].GetComponent<BoxCollider>().isTrigger = false;
            endGameCubes[i].GetComponent<BoxCollider>().enabled = true;
            endGameCubes[i].gameObject.transform.parent = null;

            endGameCubes.Remove(endGameCubes[i]); // todo  dagilan objelerin yeni levele baslamadan once destroylanmasi lazim. Maybe Ground OnCollisionEnter.

            if (endGameCubes.Count <= 0)
            {
                Debug.Log("Finished");
                OnGameFinished?.Invoke(nextLevelIndex);
            }
        }
    }

    public bool HandleCreateNextLevel()
    {
        ++currentLevelIndex;

        if (levelInfoAsset.levelInfos.Count >= currentLevelIndex)
        {
            CreateNextLevel();
            return true;
        }

        return false;
    }

    void CreateNextLevel()
    {
        blocksFromImage = fillAreaSpawner.CreateBlockFromImage(levelInfoAsset.levelInfos[currentLevelIndex - 1], fillAreaContainer);
        maxStartedCubeAmount = blocksFromImage.Count;
        CreateBlocks();
        CubeSpawner();
    }

    void CreateBlocks()
    {
        switch (startCubeShapes)
        {
            case 1:
                float xOffset = 1f;
                float yOffset = 0.15f;
                float zOffset = -4f;

                int modIndex = 0;
                int modCounter = 0;

                for (int i = 0; i < blocksFromImage.Count; i++)
                {
                    if (i % 10 == 0)
                        modCounter++;

                    modIndex = i % 10;
                    modCounter = modCounter % 10;

                    StartCube tmpCube = Instantiate(startCube, startCubeTransform);
                    tmpCube.GetComponent<Collider>().enabled = false;
                    tmpCube.transform.position = new Vector3(xOffset - modIndex * 0.35f, yOffset + modCounter * 0.5f, zOffset);
                    startedCubes2.Add(tmpCube);
                }
                break;

            case 2:

                int maxHeight = 9;
                for (int height = 0; height < maxHeight; height++)
                {
                    int length = maxHeight - height;
                    for (int x = -length; x <= length; x++)
                    {
                        for (int z = -length; z <= length; z++)
                        {
                            if (Mathf.Abs(x) == length || Mathf.Abs(z) == length)
                            {
                                StartCube tmpCube2 = Instantiate(startCube, startCubeTransform);
                                tmpCube2.GetComponent<Collider>().enabled = false;
                                tmpCube2.transform.position = new Vector3(x * .305f, height * .305f, -4 - z * .305f);
                                startedCubes2.Add(tmpCube2);
                            }
                        }
                    }
                }

                break;

        }
    }

    private void CubeSpawner()
    {
        float xOffset = 1f;
        float yOffset = 0.15f;
        float zOffset = -4f;

        int modIndex = 0;
        int modCounter = 0;

        for (int i = 0; i < blocksFromImage.Count; i++)
        {
            if (i % 10 == 0)
                modCounter++;

            modIndex = i % 10;
            modCounter = modCounter % 10;

            StartCube tmpCube = Instantiate(startCube, startCubeTransform);

            tmpCube.GetComponent<MeshRenderer>().enabled = false;
            tmpCube.transform.position = new Vector3(xOffset - modIndex * 0.35f, yOffset + modCounter * 0.5f, zOffset);
            startedCubes.Add(tmpCube);
        }
    }

    public void ActivateBlocks()
    {
        for (int i = 0; i < startedCubes2.Count; i++)
        {
            Destroy(startedCubes2[i].gameObject);
        }

        for (int i = 0; i < startedCubes.Count; i++)
        {
            if (startedCubes[i].rb != null)
                startedCubes[i].GetComponent<MeshRenderer>().enabled = true;

            startedCubes[i].rb.isKinematic = false;
            startedCubes[i].GetComponent<Collider>().enabled = true;
        }
    }
}
