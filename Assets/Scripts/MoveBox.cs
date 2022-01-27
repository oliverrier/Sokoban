using UnityEngine;

public class MoveBox : MonoBehaviour
{
    private Vector3 normal;
    private bool bIsPlayerColliding;
    private GameManager gameManager;
    private LocationLog locationLog;
    

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        locationLog = GetComponent<LocationLog>();

    }

    private bool CanPushBox() => gameManager.state == GameManager.GameState.Running && bIsPlayerColliding && Input.GetKeyDown(KeyCode.Space) &&
                                 !Physics.Raycast(transform.position, transform.TransformDirection(normal), 0.5f);
    private void Update()
    {
        if (!CanPushBox()) return;
        locationLog.LogBoxLocation();
            
        if (normal == transform.forward)
        {
            transform.Translate(0,0,1);
            return;
        }

        if (normal == transform.right)
        {
            transform.Translate(1,0,0);
            return;
        }

        if (normal == -(transform.forward))
        {
            transform.Translate(0,0,-1);
            return;
        }

        transform.Translate(-1,0,0);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Player") return;
        normal = other.contacts[0].normal;
        bIsPlayerColliding = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            bIsPlayerColliding = false;
        }    
    }
}