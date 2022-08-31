using System;
using Node_based_texture_generator.Editor.Nodes.ValueNodes;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Node_based_texture_generator.Editor.GraphBase
{
    [CustomNodeGraphEditor(typeof(TextureMainGraph))]
    public class TextureMainGraphEditor : NodeGraphEditor
    {
        public override void OnOpen()
        {
            base.OnOpen();
        }

        private Action OnClick;

        public override void OnGUI()
        {
            base.OnGUI();
            var e = Event.current;
            switch (e.type)
            {
                case EventType.MouseDown:
                    OnClick?.Invoke();
                    break;
                case EventType.MouseUp:
                    break;
                case EventType.MouseMove:
                    break;
                case EventType.MouseDrag:
                    break;
                case EventType.KeyDown:
                    switch (e.keyCode)
                    {
                        case KeyCode.None:
                            break;
                        case KeyCode.Backspace:
                            break;
                        case KeyCode.Delete:
                            break;
                        case KeyCode.Tab:
                            break;
                        case KeyCode.Clear:
                            break;
                        case KeyCode.Return:
                            break;
                        case KeyCode.Pause:
                            break;
                        case KeyCode.Escape:
                            break;
                        case KeyCode.Space:
                            var GenericMenu = new GenericMenu();
                            AddContextMenuItems(GenericMenu);
                            GenericMenu.ShowAsContext();
                            break;
                        case KeyCode.UpArrow:
                            break;
                        case KeyCode.DownArrow:
                            break;
                        case KeyCode.RightArrow:
                            break;
                        case KeyCode.LeftArrow:
                            break;
                        case KeyCode.Insert:
                            break;

                        case KeyCode.Alpha0:
                            break;
                        case KeyCode.Alpha1:
                            OnClick = () =>
                            {
                                CreateNode(typeof(FloatValueInput),
                                    NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition));
                            };
                            break;
                        case KeyCode.Alpha2:
                            break;
                        case KeyCode.Alpha3:
                            break;
                        case KeyCode.Alpha4:
                            break;
                        case KeyCode.Alpha5:
                            break;
                        case KeyCode.Alpha6:
                            break;
                        case KeyCode.Alpha7:
                            break;
                        case KeyCode.Alpha8:
                            break;
                        case KeyCode.Alpha9:
                            break;
                        case KeyCode.Exclaim:
                            break;
                        case KeyCode.DoubleQuote:
                            break;
                        case KeyCode.Hash:
                            break;
                        case KeyCode.Dollar:
                            break;
                        case KeyCode.Percent:
                            break;
                        case KeyCode.Ampersand:
                            break;
                        case KeyCode.Quote:
                            break;
                        case KeyCode.LeftParen:
                            break;
                        case KeyCode.RightParen:
                            break;
                        case KeyCode.Asterisk:
                            break;
                        case KeyCode.Plus:
                            break;
                        case KeyCode.Comma:
                            break;
                        case KeyCode.Minus:
                            break;
                        case KeyCode.Period:
                            break;
                        case KeyCode.Slash:
                            break;
                        case KeyCode.Colon:
                            break;
                        case KeyCode.Semicolon:
                            break;
                        case KeyCode.Less:
                            break;
                        case KeyCode.Equals:
                            break;
                        case KeyCode.Greater:
                            break;
                        case KeyCode.Question:
                            break;
                        case KeyCode.At:
                            break;
                        case KeyCode.LeftBracket:
                            break;
                        case KeyCode.Backslash:
                            break;
                        case KeyCode.RightBracket:
                            break;
                        case KeyCode.Caret:
                            break;
                        case KeyCode.Underscore:
                            break;
                        case KeyCode.BackQuote:
                            break;
                        case KeyCode.A:
                            break;
                        case KeyCode.B:
                            break;
                        case KeyCode.C:
                            break;
                        case KeyCode.D:
                            break;
                        case KeyCode.E:
                            break;
                        case KeyCode.F:
                            break;
                        case KeyCode.G:
                            break;
                        case KeyCode.H:
                            break;
                        case KeyCode.I:
                            break;
                        case KeyCode.J:
                            break;
                        case KeyCode.K:
                            break;
                        case KeyCode.L:
                            break;
                        case KeyCode.M:
                            break;
                        case KeyCode.N:
                            break;
                        case KeyCode.O:
                            break;
                        case KeyCode.P:
                            break;
                        case KeyCode.Q:
                            break;
                        case KeyCode.R:
                            break;
                        case KeyCode.S:
                            break;
                        case KeyCode.T:
                            break;
                        case KeyCode.U:
                            break;
                        case KeyCode.V:
                            break;
                        case KeyCode.W:
                            break;
                        case KeyCode.X:
                            break;
                        case KeyCode.Y:
                            break;
                        case KeyCode.Z:
                            break;
                        case KeyCode.LeftCurlyBracket:
                            break;
                        case KeyCode.Pipe:
                            break;
                        case KeyCode.RightCurlyBracket:
                            break;
                        case KeyCode.Tilde:
                            break;
                        case KeyCode.Numlock:
                            break;
                        case KeyCode.CapsLock:
                            break;
                        case KeyCode.ScrollLock:
                            break;
                        case KeyCode.RightShift:
                            break;
                        case KeyCode.LeftShift:
                            break;
                        case KeyCode.RightControl:
                            break;
                        case KeyCode.LeftControl:
                            break;
                        case KeyCode.RightAlt:
                            break;
                        case KeyCode.LeftAlt:
                            break;
                        case KeyCode.LeftMeta:
                            break;
                        case KeyCode.LeftWindows:
                            break;
                        case KeyCode.RightMeta:
                            break;
                        case KeyCode.RightWindows:
                            break;
                        case KeyCode.AltGr:
                            break;
                        case KeyCode.Help:
                            break;
                        case KeyCode.Print:
                            break;
                        case KeyCode.SysReq:
                            break;
                        case KeyCode.Break:
                            break;
                        case KeyCode.Menu:
                            break;
                        case KeyCode.Mouse0:
                            break;
                        case KeyCode.Mouse1:
                            break;
                        case KeyCode.Mouse2:
                            break;
                        case KeyCode.Mouse3:
                            break;
                        case KeyCode.Mouse4:
                            break;
                        case KeyCode.Mouse5:
                            break;
                        case KeyCode.Mouse6:
                            break;
                        case KeyCode.JoystickButton0:
                            break;
                        case KeyCode.JoystickButton1:
                            break;
                        case KeyCode.JoystickButton2:
                            break;
                        case KeyCode.JoystickButton3:
                            break;
                        case KeyCode.JoystickButton4:
                            break;
                        case KeyCode.JoystickButton5:
                            break;
                        case KeyCode.JoystickButton6:
                            break;
                        case KeyCode.JoystickButton7:
                            break;
                        case KeyCode.JoystickButton8:
                            break;
                        case KeyCode.JoystickButton9:
                            break;
                        case KeyCode.JoystickButton10:
                            break;
                        case KeyCode.JoystickButton11:
                            break;
                        case KeyCode.JoystickButton12:
                            break;
                        case KeyCode.JoystickButton13:
                            break;
                        case KeyCode.JoystickButton14:
                            break;
                        case KeyCode.JoystickButton15:
                            break;
                        case KeyCode.JoystickButton16:
                            break;
                        case KeyCode.JoystickButton17:
                            break;
                        case KeyCode.JoystickButton18:
                            break;
                        case KeyCode.JoystickButton19:
                            break;
                        case KeyCode.Joystick1Button0:
                            break;
                        case KeyCode.Joystick1Button1:
                            break;
                        case KeyCode.Joystick1Button2:
                            break;
                        case KeyCode.Joystick1Button3:
                            break;
                        case KeyCode.Joystick1Button4:
                            break;
                        case KeyCode.Joystick1Button5:
                            break;
                        case KeyCode.Joystick1Button6:
                            break;
                        case KeyCode.Joystick1Button7:
                            break;
                        case KeyCode.Joystick1Button8:
                            break;
                        case KeyCode.Joystick1Button9:
                            break;
                        case KeyCode.Joystick1Button10:
                            break;
                        case KeyCode.Joystick1Button11:
                            break;
                        case KeyCode.Joystick1Button12:
                            break;
                        case KeyCode.Joystick1Button13:
                            break;
                        case KeyCode.Joystick1Button14:
                            break;
                        case KeyCode.Joystick1Button15:
                            break;
                        case KeyCode.Joystick1Button16:
                            break;
                        case KeyCode.Joystick1Button17:
                            break;
                        case KeyCode.Joystick1Button18:
                            break;
                        case KeyCode.Joystick1Button19:
                            break;
                        case KeyCode.Joystick2Button0:
                            break;
                        case KeyCode.Joystick2Button1:
                            break;
                        case KeyCode.Joystick2Button2:
                            break;
                        case KeyCode.Joystick2Button3:
                            break;
                        case KeyCode.Joystick2Button4:
                            break;
                        case KeyCode.Joystick2Button5:
                            break;
                        case KeyCode.Joystick2Button6:
                            break;
                        case KeyCode.Joystick2Button7:
                            break;
                        case KeyCode.Joystick2Button8:
                            break;
                        case KeyCode.Joystick2Button9:
                            break;
                        case KeyCode.Joystick2Button10:
                            break;
                        case KeyCode.Joystick2Button11:
                            break;
                        case KeyCode.Joystick2Button12:
                            break;
                        case KeyCode.Joystick2Button13:
                            break;
                        case KeyCode.Joystick2Button14:
                            break;
                        case KeyCode.Joystick2Button15:
                            break;
                        case KeyCode.Joystick2Button16:
                            break;
                        case KeyCode.Joystick2Button17:
                            break;
                        case KeyCode.Joystick2Button18:
                            break;
                        case KeyCode.Joystick2Button19:
                            break;
                        case KeyCode.Joystick3Button0:
                            break;
                        case KeyCode.Joystick3Button1:
                            break;
                        case KeyCode.Joystick3Button2:
                            break;
                        case KeyCode.Joystick3Button3:
                            break;
                        case KeyCode.Joystick3Button4:
                            break;
                        case KeyCode.Joystick3Button5:
                            break;
                        case KeyCode.Joystick3Button6:
                            break;
                        case KeyCode.Joystick3Button7:
                            break;
                        case KeyCode.Joystick3Button8:
                            break;
                        case KeyCode.Joystick3Button9:
                            break;
                        case KeyCode.Joystick3Button10:
                            break;
                        case KeyCode.Joystick3Button11:
                            break;
                        case KeyCode.Joystick3Button12:
                            break;
                        case KeyCode.Joystick3Button13:
                            break;
                        case KeyCode.Joystick3Button14:
                            break;
                        case KeyCode.Joystick3Button15:
                            break;
                        case KeyCode.Joystick3Button16:
                            break;
                        case KeyCode.Joystick3Button17:
                            break;
                        case KeyCode.Joystick3Button18:
                            break;
                        case KeyCode.Joystick3Button19:
                            break;
                        case KeyCode.Joystick4Button0:
                            break;
                        case KeyCode.Joystick4Button1:
                            break;
                        case KeyCode.Joystick4Button2:
                            break;
                        case KeyCode.Joystick4Button3:
                            break;
                        case KeyCode.Joystick4Button4:
                            break;
                        case KeyCode.Joystick4Button5:
                            break;
                        case KeyCode.Joystick4Button6:
                            break;
                        case KeyCode.Joystick4Button7:
                            break;
                        case KeyCode.Joystick4Button8:
                            break;
                        case KeyCode.Joystick4Button9:
                            break;
                        case KeyCode.Joystick4Button10:
                            break;
                        case KeyCode.Joystick4Button11:
                            break;
                        case KeyCode.Joystick4Button12:
                            break;
                        case KeyCode.Joystick4Button13:
                            break;
                        case KeyCode.Joystick4Button14:
                            break;
                        case KeyCode.Joystick4Button15:
                            break;
                        case KeyCode.Joystick4Button16:
                            break;
                        case KeyCode.Joystick4Button17:
                            break;
                        case KeyCode.Joystick4Button18:
                            break;
                        case KeyCode.Joystick4Button19:
                            break;
                        case KeyCode.Joystick5Button0:
                            break;
                        case KeyCode.Joystick5Button1:
                            break;
                        case KeyCode.Joystick5Button2:
                            break;
                        case KeyCode.Joystick5Button3:
                            break;
                        case KeyCode.Joystick5Button4:
                            break;
                        case KeyCode.Joystick5Button5:
                            break;
                        case KeyCode.Joystick5Button6:
                            break;
                        case KeyCode.Joystick5Button7:
                            break;
                        case KeyCode.Joystick5Button8:
                            break;
                        case KeyCode.Joystick5Button9:
                            break;
                        case KeyCode.Joystick5Button10:
                            break;
                        case KeyCode.Joystick5Button11:
                            break;
                        case KeyCode.Joystick5Button12:
                            break;
                        case KeyCode.Joystick5Button13:
                            break;
                        case KeyCode.Joystick5Button14:
                            break;
                        case KeyCode.Joystick5Button15:
                            break;
                        case KeyCode.Joystick5Button16:
                            break;
                        case KeyCode.Joystick5Button17:
                            break;
                        case KeyCode.Joystick5Button18:
                            break;
                        case KeyCode.Joystick5Button19:
                            break;
                        case KeyCode.Joystick6Button0:
                            break;
                        case KeyCode.Joystick6Button1:
                            break;
                        case KeyCode.Joystick6Button2:
                            break;
                        case KeyCode.Joystick6Button3:
                            break;
                        case KeyCode.Joystick6Button4:
                            break;
                        case KeyCode.Joystick6Button5:
                            break;
                        case KeyCode.Joystick6Button6:
                            break;
                        case KeyCode.Joystick6Button7:
                            break;
                        case KeyCode.Joystick6Button8:
                            break;
                        case KeyCode.Joystick6Button9:
                            break;
                        case KeyCode.Joystick6Button10:
                            break;
                        case KeyCode.Joystick6Button11:
                            break;
                        case KeyCode.Joystick6Button12:
                            break;
                        case KeyCode.Joystick6Button13:
                            break;
                        case KeyCode.Joystick6Button14:
                            break;
                        case KeyCode.Joystick6Button15:
                            break;
                        case KeyCode.Joystick6Button16:
                            break;
                        case KeyCode.Joystick6Button17:
                            break;
                        case KeyCode.Joystick6Button18:
                            break;
                        case KeyCode.Joystick6Button19:
                            break;
                        case KeyCode.Joystick7Button0:
                            break;
                        case KeyCode.Joystick7Button1:
                            break;
                        case KeyCode.Joystick7Button2:
                            break;
                        case KeyCode.Joystick7Button3:
                            break;
                        case KeyCode.Joystick7Button4:
                            break;
                        case KeyCode.Joystick7Button5:
                            break;
                        case KeyCode.Joystick7Button6:
                            break;
                        case KeyCode.Joystick7Button7:
                            break;
                        case KeyCode.Joystick7Button8:
                            break;
                        case KeyCode.Joystick7Button9:
                            break;
                        case KeyCode.Joystick7Button10:
                            break;
                        case KeyCode.Joystick7Button11:
                            break;
                        case KeyCode.Joystick7Button12:
                            break;
                        case KeyCode.Joystick7Button13:
                            break;
                        case KeyCode.Joystick7Button14:
                            break;
                        case KeyCode.Joystick7Button15:
                            break;
                        case KeyCode.Joystick7Button16:
                            break;
                        case KeyCode.Joystick7Button17:
                            break;
                        case KeyCode.Joystick7Button18:
                            break;
                        case KeyCode.Joystick7Button19:
                            break;
                        case KeyCode.Joystick8Button0:
                            break;
                        case KeyCode.Joystick8Button1:
                            break;
                        case KeyCode.Joystick8Button2:
                            break;
                        case KeyCode.Joystick8Button3:
                            break;
                        case KeyCode.Joystick8Button4:
                            break;
                        case KeyCode.Joystick8Button5:
                            break;
                        case KeyCode.Joystick8Button6:
                            break;
                        case KeyCode.Joystick8Button7:
                            break;
                        case KeyCode.Joystick8Button8:
                            break;
                        case KeyCode.Joystick8Button9:
                            break;
                        case KeyCode.Joystick8Button10:
                            break;
                        case KeyCode.Joystick8Button11:
                            break;
                        case KeyCode.Joystick8Button12:
                            break;
                        case KeyCode.Joystick8Button13:
                            break;
                        case KeyCode.Joystick8Button14:
                            break;
                        case KeyCode.Joystick8Button15:
                            break;
                        case KeyCode.Joystick8Button16:
                            break;
                        case KeyCode.Joystick8Button17:
                            break;
                        case KeyCode.Joystick8Button18:
                            break;
                        case KeyCode.Joystick8Button19:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case EventType.KeyUp:
                    switch (e.keyCode)
                    {
                        case KeyCode.Alpha1:
                        {
                            OnClick = null;
                            break;
                        }
                    }

                    break;
                case EventType.ScrollWheel:
                    break;
                case EventType.Repaint:
                    break;
                case EventType.Layout:
                    break;
                case EventType.DragUpdated:
                    break;
                case EventType.DragPerform:
                    break;
                case EventType.DragExited:
                    break;
                case EventType.Ignore:
                    break;
                case EventType.Used:
                    break;
                case EventType.ValidateCommand:
                    break;
                case EventType.ExecuteCommand:
                    break;
                case EventType.ContextClick:
                    break;
                case EventType.MouseEnterWindow:
                    break;
                case EventType.MouseLeaveWindow:
                    break;
                case EventType.TouchDown:
                    break;
                case EventType.TouchUp:
                    break;
                case EventType.TouchMove:
                    break;
                case EventType.TouchEnter:
                    break;
                case EventType.TouchLeave:
                    break;
                case EventType.TouchStationary:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}