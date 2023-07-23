using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectpoolingLaser : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab c?a ??i t??ng Laser
    public int poolSize = 5; // S? l??ng Laser s? ???c t?o trong pool ban ??u
    public Transform player; // Transform c?a ng??i ch?i (??i t??ng m?c ti�u c?a Laser)
    private List<GameObject> laserPool; // Danh s�ch ch?a c�c ??i t??ng Laser trong pool

    public float fireInterval = 0.5f; // Th?i gian gi?a m?i l?n b?n Laser
    private int currentLaserIndex = 0; // L?u v? tr� hi?n t?i c?a Laser ?? b?n

    private void Start()
    {
        laserPool = new List<GameObject>();

        // ?i?n pool Laser ban ??u b?ng vi?c t?o c�c ??i t??ng Laser v� ??t ch�ng ?n
        for (int i = 0; i < poolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab); // T?o m?t ??i t??ng Laser t? Prefab
            laser.SetActive(false); // ??t ??i t??ng Laser ?n
            laserPool.Add(laser); // Th�m ??i t??ng Laser v�o pool
        }

        // G?i h�m FireLaser sau m?t kho?ng th?i gian l?p l?i (fireInterval)
        InvokeRepeating("FireLaser", fireInterval, fireInterval);
    }

    private void FireLaser()
    {
        // T�m ??i t??ng Laser kh�ng ho?t ??ng trong pool
        GameObject laser = GetInactiveLaser();

        if (laser != null)
        {
            // Thi?t l?p v? tr� b?n Laser t? v? tr� c?a ng??i ch?i
            Vector3 firePosition = player.position;
            firePosition.z = 0;
            laser.transform.position = firePosition;

            // K�ch ho?t ??i t??ng Laser ?? b?n
            laser.SetActive(true);
        }
    }

    private GameObject GetInactiveLaser()
    {
        // T�m ??i t??ng Laser kh�ng ho?t ??ng trong pool
        for (int i = 0; i < laserPool.Count; i++)
        {
            int indexToCheck = (currentLaserIndex + i) % laserPool.Count; // L?p l?i t? ??u n?u c?n thi?t
            if (!laserPool[indexToCheck].activeInHierarchy)
            {
                currentLaserIndex = (indexToCheck + 1) % laserPool.Count; // ??t ch? s? ti?p theo ?? b?n Laser ti?p theo
                return laserPool[indexToCheck];
            }
        }

        // N?u kh�ng t�m th?y ??i t??ng Laser kh�ng ho?t ??ng, tr? v? null
        return null;
    }
}
