// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputActions/Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""In Game"",
            ""id"": ""a667a2a9-a6f2-491a-ba12-98df645e1640"",
            ""actions"": [
                {
                    ""name"": ""PlayerMove"",
                    ""type"": ""Value"",
                    ""id"": ""52456bde-c085-473c-93b7-bcc561b292c9"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": ""InvertVector2(invertX=false,invertY=false),StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftArm"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8e4833d2-3f62-46d5-b9a5-6ae97b026333"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightArm"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a1d11243-23a7-47f5-a71d-841087c409df"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""b552b129-4a25-4013-ba3c-c0a4586b8739"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button A"",
                    ""type"": ""Button"",
                    ""id"": ""8d9627c1-0043-4568-b5a2-c071fa4658f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button B"",
                    ""type"": ""Button"",
                    ""id"": ""db0a0745-5479-44bd-9c5b-07f8affbcd8f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button X"",
                    ""type"": ""Button"",
                    ""id"": ""7a6b112f-6c5f-41f8-b6c6-b2eb4ea7e560"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button Y"",
                    ""type"": ""Button"",
                    ""id"": ""172ef89d-d448-4dfa-9414-effcc2e9b963"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""5130658a-f329-4cfd-abab-d49e8d61a997"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6a6aa18e-4a9c-491a-92e5-38228f67fc0b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e1aa9d27-6e7a-4f77-a2af-1208f869f3b1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c6a6fac1-7723-4263-8353-75baeb5801f7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""79902e90-82d7-42c9-9c5a-aac8a7b906b9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""30c53ca4-de93-4736-923a-e49e7aeab932"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3fa433bc-da6b-4fab-b5c8-4191a2323a27"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ad8f78c1-52c7-47a1-89d6-7a8f3939a4f9"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81d409c1-ed7f-485a-a54f-dcc1aae2e81c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5178a83b-e56b-483e-b35b-632bffe74d1b"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16c218c1-1b83-4edf-8770-8a5e1c52445d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25385858-fa1e-418d-841e-577881e36c16"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e621522-51c0-4576-be8a-8a9005866931"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ca69b82-f894-428f-8650-7ba321e75a09"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4befa79f-1e53-4715-9716-e50d030cfbc4"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9051819e-5a30-4641-8718-ef4cf1387da7"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc2b76e9-d0cd-48d6-abf7-cc0d03454591"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // In Game
        m_InGame = asset.FindActionMap("In Game", throwIfNotFound: true);
        m_InGame_PlayerMove = m_InGame.FindAction("PlayerMove", throwIfNotFound: true);
        m_InGame_LeftArm = m_InGame.FindAction("LeftArm", throwIfNotFound: true);
        m_InGame_RightArm = m_InGame.FindAction("RightArm", throwIfNotFound: true);
        m_InGame_Reset = m_InGame.FindAction("Reset", throwIfNotFound: true);
        m_InGame_ButtonA = m_InGame.FindAction("Button A", throwIfNotFound: true);
        m_InGame_ButtonB = m_InGame.FindAction("Button B", throwIfNotFound: true);
        m_InGame_ButtonX = m_InGame.FindAction("Button X", throwIfNotFound: true);
        m_InGame_ButtonY = m_InGame.FindAction("Button Y", throwIfNotFound: true);
        m_InGame_Pause = m_InGame.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // In Game
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_PlayerMove;
    private readonly InputAction m_InGame_LeftArm;
    private readonly InputAction m_InGame_RightArm;
    private readonly InputAction m_InGame_Reset;
    private readonly InputAction m_InGame_ButtonA;
    private readonly InputAction m_InGame_ButtonB;
    private readonly InputAction m_InGame_ButtonX;
    private readonly InputAction m_InGame_ButtonY;
    private readonly InputAction m_InGame_Pause;
    public struct InGameActions
    {
        private @Player m_Wrapper;
        public InGameActions(@Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMove => m_Wrapper.m_InGame_PlayerMove;
        public InputAction @LeftArm => m_Wrapper.m_InGame_LeftArm;
        public InputAction @RightArm => m_Wrapper.m_InGame_RightArm;
        public InputAction @Reset => m_Wrapper.m_InGame_Reset;
        public InputAction @ButtonA => m_Wrapper.m_InGame_ButtonA;
        public InputAction @ButtonB => m_Wrapper.m_InGame_ButtonB;
        public InputAction @ButtonX => m_Wrapper.m_InGame_ButtonX;
        public InputAction @ButtonY => m_Wrapper.m_InGame_ButtonY;
        public InputAction @Pause => m_Wrapper.m_InGame_Pause;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @PlayerMove.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnPlayerMove;
                @PlayerMove.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnPlayerMove;
                @PlayerMove.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnPlayerMove;
                @LeftArm.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnLeftArm;
                @LeftArm.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnLeftArm;
                @LeftArm.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnLeftArm;
                @RightArm.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnRightArm;
                @RightArm.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnRightArm;
                @RightArm.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnRightArm;
                @Reset.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnReset;
                @ButtonA.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonA;
                @ButtonA.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonA;
                @ButtonA.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonA;
                @ButtonB.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonB;
                @ButtonB.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonB;
                @ButtonB.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonB;
                @ButtonX.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonX;
                @ButtonX.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonX;
                @ButtonX.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonX;
                @ButtonY.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonY;
                @ButtonY.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonY;
                @ButtonY.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnButtonY;
                @Pause.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerMove.started += instance.OnPlayerMove;
                @PlayerMove.performed += instance.OnPlayerMove;
                @PlayerMove.canceled += instance.OnPlayerMove;
                @LeftArm.started += instance.OnLeftArm;
                @LeftArm.performed += instance.OnLeftArm;
                @LeftArm.canceled += instance.OnLeftArm;
                @RightArm.started += instance.OnRightArm;
                @RightArm.performed += instance.OnRightArm;
                @RightArm.canceled += instance.OnRightArm;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
                @ButtonA.started += instance.OnButtonA;
                @ButtonA.performed += instance.OnButtonA;
                @ButtonA.canceled += instance.OnButtonA;
                @ButtonB.started += instance.OnButtonB;
                @ButtonB.performed += instance.OnButtonB;
                @ButtonB.canceled += instance.OnButtonB;
                @ButtonX.started += instance.OnButtonX;
                @ButtonX.performed += instance.OnButtonX;
                @ButtonX.canceled += instance.OnButtonX;
                @ButtonY.started += instance.OnButtonY;
                @ButtonY.performed += instance.OnButtonY;
                @ButtonY.canceled += instance.OnButtonY;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IInGameActions
    {
        void OnPlayerMove(InputAction.CallbackContext context);
        void OnLeftArm(InputAction.CallbackContext context);
        void OnRightArm(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
        void OnButtonA(InputAction.CallbackContext context);
        void OnButtonB(InputAction.CallbackContext context);
        void OnButtonX(InputAction.CallbackContext context);
        void OnButtonY(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
