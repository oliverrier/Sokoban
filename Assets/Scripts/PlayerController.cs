using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (gameManager.state != GameManager.GameState.Running) return;
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (moveX == 0 && moveY == 0) return;
        transform.rotation = Quaternion.LookRotation(moveY * Vector3.forward + moveX * Vector3.right);

        float movement = Math.Abs(moveY) + Math.Abs(moveX);
        
        transform.Translate((movement > 1 ? 1f : movement) * Vector3.forward * Time.deltaTime * speed);

    }
}
