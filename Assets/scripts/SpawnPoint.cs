using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Identificaci�n")]
    public string idSpawn = "SpawnPoint01";

    [Header("Visual (Solo en Editor)")]
    public Color colorGizmo = Color.green;
    public float tama�oGizmo = 0.5f;

    void OnDrawGizmos()
    {
        Gizmos.color = colorGizmo;
        Gizmos.DrawWireSphere(transform.position, tama�oGizmo);

        // Dibujar direcci�n
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.right * tama�oGizmo * 2);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, tama�oGizmo);
    }
}