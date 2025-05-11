using UnityEngine;

[CreateAssetMenu(fileName = "GenericPoolSO", menuName = "New Pool")]
public class GenericPoolSO : ScriptableObject
{
    public string poolID;
    public GameObject bulletPrefab;
    public int poolSize = 10;
}
