using UnityEngine;
public enum PickUpKind { Score, Heal, Speed, Key, Damage }

[CreateAssetMenu(menuName = "Data/PickUp", fileName = "NewPickUp")]
public class PickUpData : ScriptableObject
{
    [Header("Identidad")]
    public string displayName;
    public Sprite icon;

    [Header("Tipo de efecto")]
    public PickUpKind kind;

    [Header("Parámetros")]
    public int scoreAmount;      
    public int healAmount;   
    public int damageAmount;
    public float speedSeconds;    
    public string doorKey;        
    public AudioClip audioClip;   

    [Header("Inventario")]
    public bool goesToInventory = true; 
}
