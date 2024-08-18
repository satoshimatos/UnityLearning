using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // O objeto que a câmera seguirá, normalmente o personagem
    public float smoothSpeed = 0.125f;  // Velocidade do smooth
    public Vector3 offset;  // Offset da posição da câmera em relação ao personagem

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}