using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    public GameObject[] waypoints; //meu array denominado wayoints, receberá os pontos nos quais meu cubo percorrerá
    int currentWP = 0; //valor inicial 
    float speed = 1.0f; //velocidade que meu objeto irá mover-se
    float accuracy = 3.0f; //aproximação que meu objeto terá
    float rotSpeed = 0.4f; //movimentação de rotação para uma nova rota
    // Start is called before the first frame update
    void Start() //início do programa
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint"); //acha o meu objeto e traça um ponto para o caminho que ele irá
    }

    // Update is called once per frame
    void LateUpdate() //atualização da movimentação do objeto
    {
        if (waypoints.Length == 0) return; //verifica o tamanho do array que fora montado, precisando retornar com zero para ocorrer o start inicialmente
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z); 
        //Criação de um objeto instanciando ele como new Vector3, dentro do waypoints, recebe o valor do current, variável inicial, e suas posições dentro de X e Z, que utilizam o array.
        //Pela movimentação do objeto estar sendo acima de um plano, não é necessário subir ou descer, mantendo apenas sempre o mesmo valor por ser uma superfície plana, não sendo necessário alterações no eixo Y. 

        Vector3 direction = lookAtGoal - this.transform.position; //Instancia a direção na qual o objeto terá que percorrer, começando sua movimentação a partir do ponto inicial até os demais
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed); // Ao se aproximar do ponto, ele começará a rotacionar em direção ao próximo de maneira suave, 
        //graças ao Quaternion.Slerp, dando uma movimentação mais natural ao objeto, junto com o LookRotation e o rotSpeed

        if(direction.magnitude < accuracy) //se a direção e magnitude forem menores que o ponto de aproximação, accuracy, o objeto então caminhará para o próximo ponto
        {
            currentWP++; //continuação da movimentação para o próximo ponto
            if(currentWP >= waypoints.Length) //caso o valor inicial seja maior ou igual aos pontos que meu objeto percorrerá
            {
                currentWP = 0; //valor inicial igual a zero
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime); //Utilização do Time.deltaTime devido ao LateUpdate, para que possa ser feita a atualização, quadro após quadro a movimentação do objeto
    } 
}
