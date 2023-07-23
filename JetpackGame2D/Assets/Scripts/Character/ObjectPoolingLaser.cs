using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectpoolingLaser : MonoBehaviour
{
    public GameObject laserPrefab; // Prefab c?a ??i t??ng Laser
    public int poolSize = 5; // S? l??ng Laser s? ???c t?o trong pool ban ??u
    public Transform player; // Transform c?a ng??i ch?i (??i t??ng m?c tiêu c?a Laser)
    private List<GameObject> laserPool; // Danh sách ch?a các ??i t??ng Laser trong pool

    public float fireInterval = 0.5f; // Th?i gian gi?a m?i l?n b?n Laser
    private int currentLaserIndex = 0; // L?u v? trí hi?n t?i c?a Laser ?? b?n

    private void Start()
    {
        laserPool = new List<GameObject>();

        // ?i?n pool Laser ban ??u b?ng vi?c t?o các ??i t??ng Laser và ??t chúng ?n
        for (int i = 0; i < poolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab); // T?o m?t ??i t??ng Laser t? Prefab
            laser.SetActive(false); // ??t ??i t??ng Laser ?n
            laserPool.Add(laser); // Thêm ??i t??ng Laser vào pool
        }

        // G?i hàm FireLaser sau m?t kho?ng th?i gian l?p l?i (fireInterval)
        InvokeRepeating("FireLaser", fireInterval, fireInterval);
    }

    private void FireLaser()
    {
        // Tìm ??i t??ng Laser không ho?t ??ng trong pool
        GameObject laser = GetInactiveLaser();

        if (laser != null)
        {
            // Thi?t l?p v? trí b?n Laser t? v? trí c?a ng??i ch?i
            Vector3 firePosition = player.position;
            firePosition.z = 0;
            laser.transform.position = firePosition;

            // Kích ho?t ??i t??ng Laser ?? b?n
            laser.SetActive(true);
        }
    }

    private GameObject GetInactiveLaser()
    {
        // Tìm ??i t??ng Laser không ho?t ??ng trong pool
        for (int i = 0; i < laserPool.Count; i++)
        {
            int indexToCheck = (currentLaserIndex + i) % laserPool.Count; // L?p l?i t? ??u n?u c?n thi?t
            if (!laserPool[indexToCheck].activeInHierarchy)
            {
                currentLaserIndex = (indexToCheck + 1) % laserPool.Count; // ??t ch? s? ti?p theo ?? b?n Laser ti?p theo
                return laserPool[indexToCheck];
            }
        }

        // N?u không tìm th?y ??i t??ng Laser không ho?t ??ng, tr? v? null
        return null;
    }
}
