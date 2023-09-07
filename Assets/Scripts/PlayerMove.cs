using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject Player;
    public Text txt;


    GameObject player; //Физическое тело

    private float speed; //Скорость перемещения
    private float impulse; //Предельный импульс
    private float FinallMass = 0.0002f; //R
    private float StartMass;
    private float ToplivoPerSecond;
    public float _text;
    public float thrustlevel;

    //Задаем переменные при старте игры
    void Start()
    {
        player = GameObject.Find("Ship");
        speed = 0f;
        thrustlevel = 0f;
        ToplivoPerSecond = 0f;
        StartMass = player.GetComponent<Rigidbody>().mass;
    }

    //Теперь опишем, что должен делать персонаж при нажатых клавишах

    private void FixedUpdate()
    {
        
        //Двигаемся вперед по вектору камеры
        if ((Input.GetKey(KeyCode.W)) && (player.GetComponent<Rigidbody>().mass > FinallMass) )
        {
            thrustlevel += 0.00000001f;
            ToplivoPerSecond += 0.0000001f;
        }
        if ((Input.GetKey(KeyCode.S)) && (player.GetComponent<Rigidbody>().mass > FinallMass))
        {
            thrustlevel -= 0.00000001f;
            ToplivoPerSecond -= 0.0000001f;
        }
        if (Input.GetKey(KeyCode.R))
        {
            player.GetComponent<Rigidbody>().mass = 0.001f;
        }
        if (thrustlevel < 0.00000001f) { thrustlevel = 0; }
        if (ToplivoPerSecond < 0) { ToplivoPerSecond = 0; }
        if (thrustlevel == 0) { thrustlevel = thrustlevel; }
        else { _text = thrustlevel * 100000000; }
        txt.text = _text.ToString();
        if (player.GetComponent<Rigidbody>().mass <= FinallMass) { thrustlevel = 0; ToplivoPerSecond = 0; }
        player.GetComponent<Rigidbody>().mass -= ToplivoPerSecond * Time.deltaTime;
        if (ToplivoPerSecond == 0) { impulse = 0; }
        else impulse = thrustlevel / ToplivoPerSecond;
        speed = impulse * Mathf.Log(StartMass / FinallMass);
         
        player.GetComponent<Rigidbody>().AddForce(MainCamera.transform.forward * speed * Time.deltaTime);
    }
}
