using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSettings", menuName = "Heist/Entity/Character", order = 0)]
public class CharacterSettings : ScriptableObject
{
    [Range(1f, 100f)][SerializeField] private float _walkSpeed = 1f;
    public float WalkSpeed => _walkSpeed;
    
    [Range(1f, 100f)][SerializeField] private float _runSpeed = 1f;
    public float RunSpeed => _runSpeed;
    
    [Range(1f, 100f)][SerializeField] private float _crouchSpeed = 1f;
    public float CrouchSpeed => _crouchSpeed;
}
