using Godot;

namespace Hello35Nav
{
    public static class InputAction
    {
        public const string Shoot = "shoot";
        public const string Interact = "interact";
        public const string Left = "ui_left";
        public const string Right = "ui_right";
        public const string Up = "ui_up";
        public const string Down = "ui_down";
        public const string Pause = "pause";
        public const string Run = "run";
        public const string Roll = "roll";
        public const string ToggleFlashlight = "ToggleFlashlight";
        public const string CameraUp = "camera_up";
        public const string CameraDown = "camera_down";
        public const string CameraLeft = "camera_left";
        public const string CameraRight = "camera_right";
        public const string CameraReset = "camera_reset";
    }

    public static class InputUtils
    {
        public static Vector2 GetTopDownWithDiagMovementInput(float speed = 1f)
        {
            var velocity = Vector2.Zero;

            if (Input.IsActionPressed(InputAction.Right) && Input.IsActionPressed(InputAction.Up))
            {
                velocity.x += 0.5f;
                velocity.y -= 0.5f;
            }

            if (Input.IsActionPressed(InputAction.Right) && Input.IsActionPressed(InputAction.Down))
            {
                velocity.x += 0.5f;
                velocity.y += 0.5f;
            }

            if (Input.IsActionPressed(InputAction.Left) && Input.IsActionPressed(InputAction.Up))
            {
                velocity.x -= 0.5f;
                velocity.y -= 0.5f;
            }

            if (Input.IsActionPressed(InputAction.Left) && Input.IsActionPressed(InputAction.Down))
            {
                velocity.x -= 0.5f;
                velocity.y += 0.5f;
            }

            if (Input.IsActionPressed(InputAction.Right))
            {
                velocity.x += 1f;
            }

            if (Input.IsActionPressed(InputAction.Left))
            {
                velocity.x -= 1f;
            }

            if (Input.IsActionPressed(InputAction.Up))
            {
                velocity.y -= 1f;
            }

            if (Input.IsActionPressed(InputAction.Down))
            {
                velocity.y += 1f;
            }

            return velocity.Normalized() * speed;
        }
    }
}