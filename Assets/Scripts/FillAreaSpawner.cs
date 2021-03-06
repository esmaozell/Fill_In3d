using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAreaSpawner : MonoBehaviour
{
    Vector3 fillArea = Vector3.zero;

    public List<Color> colorsList;

    public List<GameObject> createdCubes = new List<GameObject>();
    public List<GameObject> CreateBlockFromImage(LevelInfo levelInfo, Transform transform)
    {
        for (int x = 0; x < levelInfo.sprite.texture.width; x++)
        {
            for (int y = 0; y < levelInfo.sprite.texture.height; y++)
            {
                Color color = levelInfo.sprite.texture.GetPixel(x, y);

                if (color.a == 0)
                {
                    continue;
                }

                fillArea = new Vector3(
                    levelInfo.size * (x - (levelInfo.sprite.texture.width * .5f)),
                    levelInfo.size * .5f,
                    levelInfo.size * (y - (levelInfo.sprite.texture.height * .5f)));

                GameObject cubeObj = Instantiate(levelInfo.baseObj, transform);
                cubeObj.transform.localPosition = fillArea;
                colorsList.Add(color);
                cubeObj.GetComponent<FillAreaController>().targetColor = color;
                cubeObj.GetComponent<Renderer>().material.color = Color.gray;
                // colordan cektik
                cubeObj.transform.localScale = Vector3.one * levelInfo.size;

                createdCubes.Add(cubeObj);
                LevelManager.Instance.endGameCubes.Add(cubeObj);
            }
        }

        return createdCubes;
    }
}
