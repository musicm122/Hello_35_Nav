using System;
using Godot;

namespace Hello35Nav
{
    
    public class Enemy : KinematicBody2D
    {
        [Signal]
        public delegate void TargetReached();
        
        [Signal]
        public delegate void PathChanged(Vector2[] safeVector);
        
        [Export]
        public float Speed { get; set; }

        [Export] 
        public NodePath TargetPath { get; set; } 

        [Export] 
        public NodePath NavAgentPath { get; set; } = "NavAgent";

        private Vector2 Velocity { get; set; }
        
        private NavigationAgent2D NavAgent { get; set; }
        
        public KinematicBody2D Target { get; set; }

        private bool ArrivedAtTarget => this.NavAgent.IsNavigationFinished();

        public void SetTargetLocation(Vector2 target)
        {
            NavAgent.SetTargetLocation(target);
            //GD.Print("Target Location Set");
        }

        private void OnVelocityComputed(Vector2 safeVelocity)
        {
            //GD.Print("OnVelocityComputed with arg ",safeVelocity);
            //_on_NavigationAgent2D_velocity_computed
            if (!ArrivedAtTarget)
            {
                //GD.Print("Enemy Moving");
                Velocity = MoveAndSlide(safeVelocity * Speed);
            }
            else
            {
                EmitSignal(nameof(PathChanged),Array.Empty<Vector2>() );
                EmitSignal(nameof(TargetReached));
            }
        }

        public void OnPathChanged()
        {
            EmitSignal(nameof(PathChanged),NavAgent.GetNavPath());
        }

        public override void _Ready()
        {
            NavAgent = GetNode<NavigationAgent2D>(NavAgentPath);
            NavAgent.Connect("path_changed", this, nameof(OnPathChanged));
            NavAgent.Connect("velocity_computed", this, nameof(OnVelocityComputed));
            
            if (TargetPath != null)
            {
                Target = GetNode<KinematicBody2D>(TargetPath);
                SetTargetLocation(Target.GlobalPosition);
            }
            else
            {
                SetTargetLocation(Position);
            }

        }

        public override void _PhysicsProcess(float delta)
        {
            //	var move_direction = position.direction_to(navigation_agent.get_next_location())
            var moveDirection = Position.DirectionTo(NavAgent.GetNextLocation());
            Velocity = moveDirection * Speed;
            NavAgent.SetVelocity(Velocity);
            SetTargetLocation(Target.GlobalPosition);
        }
    }
}