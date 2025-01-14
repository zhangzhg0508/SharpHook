namespace SharpHook.Native;

/// <summary>
/// Represents the type of an input event which can be raised from a global hook or posted as a fake event.
/// </summary>
/// <seealso cref="UioHookEvent" />
/// <seealso cref="UioHook.PostEvent" />
public enum EventType
{
    /// <summary>
    /// Raised when the global hook is started. Ignored when posted.
    /// </summary>
    HookEnabled = 1,

    /// <summary>
    /// Raised when the global hook is stopped. Ignored when posted.
    /// </summary>
    HookDisabled,

    /// <summary>
    /// Raised when a character is typed. Ignored when posted.
    /// </summary>
    KeyTyped,

    /// <summary>
    /// Raised when a key is pressed or posted to press a key.
    /// </summary>
    KeyPressed,

    /// <summary>
    /// Raised when a key is released or posted to release a key.
    /// </summary>
    KeyReleased,

    /// <summary>
    /// Raised when a mouse button is clicked. Ignored when posted.
    /// </summary>
    MouseClicked,

    /// <summary>
    /// Raised when a mouse button is pressed or posted to press a mouse button.
    /// </summary>
    MousePressed,

    /// <summary>
    /// Raised when a mouse button is released or posted to release a mouse button.
    /// </summary>
    MouseReleased,

    /// <summary>
    /// Raised when the mouse is moved or posted to move the mouse.
    /// </summary>
    MouseMoved,

    /// <summary>
    /// Raised when the mouse is dragged. Not recommended to post as it will be the same as <see cref="MouseMoved" />.
    /// </summary>
    MouseDragged,

    /// <summary>
    /// Raised when the mouse wheel is scrolled or posted to scroll the mouse wheel.
    /// </summary>
    MouseWheel,

    /// <summary>
    /// Posted to press a mouse button at the current coordinates. Never raised.
    /// </summary>
    MousePressedIgnoreCoordinates,

    /// <summary>
    /// Posted to release a mouse button at the current coordinates. Never raised.
    /// </summary>
    MouseReleasedIgnoreCoordinates,

    /// <summary>
    /// Posted to move the mouse relative to the current cursor position. Never raised.
    /// </summary>
    MouseMovedRelativeToCursor
}
