using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CPU_Behaviour : MonoBehaviour
{
    public float speed;
    public int numberOfPassengers;
    public int passengersCapacity;
    public TextMeshProUGUI passengersUI;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        animator.speed = speed;

        StartCoroutine(waitBeforeStart());
    }

    void Update()
    {
        passengersUI.text = numberOfPassengers.ToString();
    }

    private void passengersGetInVehicle()
    {
        for (int i = 0; i < passengersCapacity; i++)
        {
            numberOfPassengers--;
            if (numberOfPassengers == 0)
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MG_Goal") && numberOfPassengers == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.CompareTag("MG_Start"))
        {
            passengersGetInVehicle();
        }
    }

    IEnumerator waitBeforeStart()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("StartAnim", true);
    }
}
