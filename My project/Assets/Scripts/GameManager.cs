using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Color;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject black, wine;
    [SerializeField] private Text turnMessage;
    private bool isPlayer, hasGameFinished;

    const string BLACK_MESSAGE = "Black's Turn";
    const string WINE_MESSAGE = "Wine's Turn";
    
    Color BLACK_COLOR = new Color(255, 255, 255, 255);
    Color WINE_COLOR = new Color(255, 255, 255, 255);
    void Awake()
    {
        isPlayer = true;
        hasGameFinished = false;
        turnMessage.text = BLACK_MESSAGE;
        //turnMessage.text = BLACK_COLOR;
    }
    public void GameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hasGameFinished) return;
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (!hit.collider) return;

            if (hit.collider.CompareTag("Press"))
            {
                if (hit.collider.gameObject.GetComponent<Column>().targetLocation.y > 1.5f) return;
                
                Vector3 spawnPos = hit.collider.gameObject.GetComponent<Column>().spawnLocation;
                Vector3 targetPos = hit.collider.gameObject.GetComponent<Column>().targetLocation;
                GameObject circle = Instantiate(isPlayer ? black : wine);
                circle.transform.position = spawnPos;
                circle.GetComponent<Mover>().targetPosition = targetPos;
                
                hit.collider.gameObject.GetComponent<Column>().targetLocation = new Vector3(targetPos.x, targetPos.y + 0.7f, targetPos.z);
                
                turnMessage.text = !isPlayer ?  BLACK_MESSAGE : WINE_MESSAGE;
                //turnMessage.text = !isPlayer ?  BLACK_COLOR : WINE_COLOR;
            }
        }
    }
}
