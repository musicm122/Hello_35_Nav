using System;
using Godot;

namespace Hello35Nav
{
    public class Player : KinematicBody2D
    {
        [Export]
        public float Speed { get; set; }
        
        public override void _PhysicsProcess(float delta)
        {
            var movementVector = InputUtils.GetTopDownWithDiagMovementInput() * Speed;
            MoveAndSlide(movementVector);
        }
    }
}