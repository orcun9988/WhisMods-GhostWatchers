using Donteco.GameConsole;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;
using Whis;
using System.Linq;
using Donteco;
using System.Collections.Generic;

namespace Main
{
    public class Main : MelonMod
    {

        public static bool Crosshair;



        public static Camera camera = null;

        //Menüler
        public static bool openmenu;
        private Rect AnaMenu = new Rect(10, 50, 250, 250);


        public static bool espmenu;
        private Rect espmenuu = new Rect(260f, 10f, 250f, 150f);


        public static bool trollmenu;
        private Rect trollmenuu = new Rect(760f, 10f, 250f, 150f);



        public static bool playersmenu;
        private Rect playersmenuu = new Rect(510f, 260f, 250f, 150f);


        public static bool ghostinfo;
        private Rect ghostinfoo = new Rect(510f, 260f, 250f, 150f);


        public static GhostAI ghost;
        public static List<PlayerSetup> network_player;
        public static PlayerSetup localplayer;
        public static PlayerWallet wallet;
        public static LobbyManager lobbyManager;
        public static Lobby lobby;
        public static List<LobbyPlayer> lobbyPlayers;
        public static FlashlightController flashlight;
        public static List<OpenableObjectController> doors;
        public static List<LightSwitcherController> lights;
        public static List<ChildrenToyController> item;
        public static List<WaterFaucetController> faucets;
        public static List<Footprint> footprints;
        public static AreaNode areaNode;

        public override void OnUpdate()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard.capsLockKey.wasPressedThisFrame)
            {
                if (openmenu == true)
                {
                    openmenu = false;
                }
                else if (openmenu == false)
                {
                    openmenu = true;
                }
            }



            areaNode = UnityEngine.GameObject.FindObjectOfType<AreaNode>();
            ghost = UnityEngine.GameObject.FindObjectOfType<GhostAI>();
            network_player = UnityEngine.GameObject.FindObjectsOfType<PlayerSetup>().ToList();
            flashlight = UnityEngine.GameObject.FindObjectOfType<FlashlightController>();
            wallet = UnityEngine.GameObject.FindObjectOfType<PlayerWallet>();
            doors = UnityEngine.GameObject.FindObjectsOfType<OpenableObjectController>().ToList();
            lights = UnityEngine.GameObject.FindObjectsOfType<LightSwitcherController>().ToList();
            item = UnityEngine.GameObject.FindObjectsOfType<ChildrenToyController>().ToList();
            faucets = UnityEngine.GameObject.FindObjectsOfType<WaterFaucetController>().ToList();
            footprints = UnityEngine.GameObject.FindObjectsOfType<Footprint>().ToList();

            lobbyManager = LobbyManager.Instance;
            lobby = LobbyManager.Current;
            lobbyPlayers = lobby.Players.Values.ToList();

            foreach (PlayerSetup p in network_player)
            {
                if (p.IsLocalPlayer)
                {
                    localplayer = p;
                }
            }
        }




        public static void gwNull()
        {
            ghost = null;
            network_player = null;
            flashlight = null;
            wallet = null;
            doors = null;
            item = null;


            lobbyManager = LobbyManager.Instance;
            lobby = LobbyManager.Current;
            lobbyPlayers = lobby.Players.Values.ToList();

            foreach (PlayerSetup p in network_player)
            {
                if (p.IsLocalPlayer)
                {
                    localplayer = p;
                }
            }
        }






        public static void DrawESP(Vector3 objfootPos, Vector3 objheadPos, Color objColor, System.String name)
        {
            float height = objheadPos.y - objfootPos.y;
            float widthOffset = 2f;
            float width = height / widthOffset;

            Render.DrawBox2(objfootPos.x - (width / 2), (float)Screen.height - objfootPos.y - height, width, height, objColor, 1f);
            Render.DrawString(new Vector2(objfootPos.x - (width / 2), (float)Screen.height - objfootPos.y - height), $"{name}", Color.green);
        }


        public override void OnGUI()
        {
            try
            {


                if (ghostbone || ghostesp)
                {
                    ESP.show_esp();
                }



                if (openmenu == true)
                {
                    GUI.color = Color.cyan;
                    AnaMenu = GUILayout.Window(1, AnaMenu, (GUI.WindowFunction)MainMenu, "Whis Mods Main", new GUILayoutOption[0]);

                    if (espmenu)
                    {
                        espmenuu = GUILayout.Window(2, espmenuu, (GUI.WindowFunction)espmenuuu, "Whis Mods ESP", new GUILayoutOption[0]);
                    }


                    if (trollmenu)
                    {
                        trollmenuu = GUILayout.Window(3, trollmenuu, (GUI.WindowFunction)trollmenuuu, "Whis Mods TrollMenu", new GUILayoutOption[0]);
                    }

                    if (playersmenu)
                    {
                        playersmenuu = GUILayout.Window(4, playersmenuu, (GUI.WindowFunction)playersmenuuu, "Whis Mods Players", new GUILayoutOption[0]);
                    }


                    if (ghostinfo)
                    {
                        ghostinfoo = GUILayout.Window(4, ghostinfoo, (GUI.WindowFunction)ghostinfooo, "Whis Mods Players", new GUILayoutOption[0]);
                    }
                }

                if (Crosshair == true)
                {
                    Render.DrawBox(new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f - 7f), new Vector2(1f, 15f), Color.cyan, true);
                    Render.DrawBox(new Vector2((float)Screen.width / 2f - 7f, (float)Screen.height / 2f), new Vector2(15f, 1f), Color.cyan, true);
                }

            }
            catch { }

        }



        public void ghostinfooo(int id)
        {

            GUILayout.Label("Ghost Information");
            GUILayout.Label($"Ghost Type: {ghost.Data.name.Replace("(Clone)", "")}");
            GUILayout.Label($"Ghost Age: {ghost.Data.Age}");
            GUILayout.Label($"Hunt Distance: [{ghost.Data.DistanceForHunt()}]");
            GUILayout.Label($"Temperature: [{ghost.Data.GetTemperatureValue()}]");
            GUILayout.Label($"Mood Type: [{ghost.Data.Mood}]");

            GUILayout.Label($"");

            GUILayout.Label($"Can Attack: [{ghost.CanStartAttack()}]");
            GUILayout.Label($"Can Capture: [{ghost.CanStartCapture()}]");
            GUILayout.Label($"Can Hunt: [{ghost.CanHunt()}]");
            GUILayout.Label($"Can Range Attack: [{ghost.CanRangeAttack()}]");
            GUILayout.Label($"Can Crit Hit: [{ghost.CanCriticalAttack()}]");
            GUILayout.Label($"Full Weakness: [{ghost.IsFullWeakness.Value}]");

            GUI.DragWindow(new Rect(0, 0, (float)Screen.width, (float)Screen.height));
        }

        public void MainMenu(int id)
        {
            try
            {
                GUI.color = Color.cyan;
                GUILayout.Label("WhiskyHacks BussinesTour Main", new GUILayoutOption[0]);


                if (GUILayout.Toggle(Crosshair, "Crosshair", new GUILayoutOption[0]) != Crosshair)
                {
                    Crosshair = !Crosshair;
                }

                if (GUILayout.Button("Steal Host [InGame]", new GUILayoutOption[0]))
                {
                    GameObject gameObject = CommandUtils.FindLocalPlayer();

                    MelonLogger.Msg("Players count: " + Donteco.NetworkManager.Players.Count + "LocalPlayerID : " + Donteco.NetworkManager.LocalPlayerId + "MasterClientID :" + Donteco.NetworkManager.MasterClientId + "Am I master?: " + Donteco.NetworkManager.IsMasterClient);

                    Donteco.NetworkManager.Send(new MhNetworking.CommonMsg.SetMasterClient
                    {
                        ObjectId = -1,
                        NewMasterId = gameObject.GetComponent<Donteco.NetIdentity>().PlayerId
                    }, Lidgren.Network.NetDeliveryMethod.ReliableOrdered);
                }



                if (GUILayout.Button("Esp Menu", new GUILayoutOption[0]))
                {
                    espmenu = !espmenu;
                }

                if (GUILayout.Button("Troll Menu", new GUILayoutOption[0]))
                {
                    trollmenu = !trollmenu;
                }


                if (GUILayout.Button("All Players Menu", new GUILayoutOption[0]))
                {
                    playersmenu = !playersmenu;
                }

                if (GUILayout.Button("Ghost Info", new GUILayoutOption[0]))
                {
                    ghostinfo = !ghostinfo;
                }

                GUI.DragWindow();
            }
            catch { }
        }



        public static bool ghostesp;
        public static bool ghostbone;
        public static bool playersesp;
        public static bool evidencesesp;
        public void espmenuuu(int id)
        {
            try
            {

                GUI.color = Color.cyan;
                if (GUILayout.Toggle(ghostesp, "Ghost Box ESP", new GUILayoutOption[0]) != ghostesp)
                {
                    ghostesp = !ghostesp;
                }

                if (GUILayout.Toggle(ghostbone, "Ghost Bone ESP", new GUILayoutOption[0]) != ghostbone)
                {
                    ghostbone = !ghostbone;
                }

                GUI.DragWindow();
            }
            catch { }
        }







        public void trollmenuuu(int id)
        {
            try
            {

                GUI.color = Color.cyan;

                if (GUILayout.Button("Appear few seconds to Ghost", new GUILayoutOption[0]))
                {
                    ghost.Data.Visibility.SetVisibleForTime(3f);

                }

                if (GUILayout.Button("Random Ghost Action", new GUILayoutOption[0]))
                {
                    ghost.Actions.Random();
                }


                if (GUILayout.Button("Ghost Attack", new GUILayoutOption[0]))
                {
                    ghost.ResetCooldowns();
                    ghost.DoCooldownFastAttack();
                }


                if (GUILayout.Button("Ghost Damage", new GUILayoutOption[0]))
                {
                    ghost.ResetCooldowns();
                    ghost.DoDamage();
                }


                if (GUILayout.Button("Ghost Noise", new GUILayoutOption[0]))
                {
                    ghost.Audio.PlayInteractBreath();
                    ghost.Audio.PlayWarningRunAttack();
                }



                if (GUILayout.Button("Open All Doors", new GUILayoutOption[0]))
                {
                    foreach (OpenableObjectController door in doors)
                    {
                        if (door != null && !door.IsOpened.Value)
                        {
                            door.Open(localplayer.gameObject);
                        }
                    }
                }

                if (GUILayout.Button("TurnOn All Lights", new GUILayoutOption[0]))
                {
                    foreach (LightSwitcherController light in lights)
                    {
                        if (light != null)
                        {
                            if (!light.IsOn.Value)
                            {
                                light.ActionsBeforeExplosion.Value = 999;
                                light.Action(localplayer.gameObject);
                            }
                        }
                    }
                }


                if (GUILayout.Button("TurnOff All Lights", new GUILayoutOption[0]))
                {
                    foreach (LightSwitcherController light in lights)
                    {
                        if (light != null)
                        {
                            if (light.IsOn.Value)
                            {
                                light.ActionsBeforeExplosion.Value = 999;
                                light.Action(localplayer.gameObject);
                            }
                        }
                    }
                }


                if (GUILayout.Button("Open All Faucets", new GUILayoutOption[0]))
                {
                    foreach (WaterFaucetController faucet in faucets)
                    {
                        if (!faucet.IsOn.Value)
                        {
                            faucet.Interact(localplayer.gameObject);
                        }
                    }
                }

                if (GUILayout.Button("Close All Faucets", new GUILayoutOption[0]))
                {
                    foreach (WaterFaucetController faucet in faucets)
                    {
                        if (faucet.IsOn.Value)
                        {
                            faucet.Interact(localplayer.gameObject);
                        }
                    }
                }

                GUI.DragWindow();
            }
            catch { }
        }



        public void playersmenuuu(int id)
        {
            try
            {

                GUI.color = Color.cyan;
                if (GUILayout.Button("test", new GUILayoutOption[0]))
                {


                }

                GUI.DragWindow();
            }
            catch { }
        }


    }

}
