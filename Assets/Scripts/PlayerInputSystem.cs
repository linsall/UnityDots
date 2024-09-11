using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;


[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private GameInput Input;
    private Entity Player;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        Input = new GameInput();
    }

    protected override void OnStartRunning()
    {
        Input.Enable();
        Input.Gameplay.Shoot.performed += OnShoot;
        Player = SystemAPI.GetSingletonEntity<PlayerTag>();

    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if(!SystemAPI.Exists(Player)) { return; }
        SystemAPI.SetComponentEnabled<FireProjectileTag>(Player, true);
    }

    protected override void OnUpdate()
    {
        Vector2 MoveInput = Input.Gameplay.Move.ReadValue<Vector2>();
        SystemAPI.SetSingleton(new PlayerMoveInput { Value = MoveInput });
    }
    protected override void OnStopRunning()
    {
        Input.Disable();
        Player = Entity.Null;
    }
}
