using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.state == GameManager.GameState.Running && other.gameObject.CompareTag("Box"))
        {
            gameManager.IncrementScore();
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameManager.state == GameManager.GameState.Running && other.gameObject.CompareTag("Box"))
        {
            gameManager.DecrementScore();
        }    
    }
}
