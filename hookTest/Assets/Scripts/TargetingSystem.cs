using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public GameObject targetSpritePrefab; // Prefab-ul RedTarget
    private GameObject currentTarget; // Ținta curentă
    private List<GameObject> targetsInRange = new List<GameObject>(); // Lista țintelor în rază
    private GameObject currentTargetSprite; // Instanța sprite-ului RedTarget

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            targetsInRange.Add(collision.gameObject);
            UpdateTarget();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            targetsInRange.Remove(collision.gameObject);
            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        if (targetsInRange.Count > 0)
        {
            currentTarget = targetsInRange[0];

            // Instanțiează și setează poziția sprite-ului target pe pește
            if (currentTargetSprite == null) // Verificăm dacă sprite-ul nu a fost deja creat
            {
                currentTargetSprite = Instantiate(targetSpritePrefab, currentTarget.transform.position, Quaternion.identity);
                currentTargetSprite.transform.SetParent(currentTarget.transform); // Face ca sprite-ul să urmeze peștele
            }
            else
            {
                currentTargetSprite.transform.position = currentTarget.transform.position; // Actualizează poziția
            }

            currentTargetSprite.SetActive(true);
        }
        else
        {
            if (currentTargetSprite != null)
            {
                currentTargetSprite.SetActive(false); // Dezactivează targetul dacă nu sunt ținte
            }
            currentTarget = null;
        }
    }

    public GameObject GetCurrentTarget()
    {
        return currentTarget;
    }
}
