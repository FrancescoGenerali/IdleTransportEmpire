using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MG_Player_Movement : MonoBehaviour
{
    public float speed;
    public int numberOfPassengers;
    public int passengersCapacity;
    public TextMeshProUGUI passengersUI;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        passengersUI.text = numberOfPassengers.ToString();
    }

    public void AddForce()
    {
        rb.AddForce(0, 0, speed * Time.deltaTime);
    }

    private void passengersGetInVehicle()
    {
        for (int i = 0; i < passengersCapacity; i++)
        {
            numberOfPassengers--;
            if (numberOfPassengers == 0)
                return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MG_Goal") && numberOfPassengers == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (other.CompareTag("MG_Start"))
        {
            passengersGetInVehicle();
        }
    }
}
