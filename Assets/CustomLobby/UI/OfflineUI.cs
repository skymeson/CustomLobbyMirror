// UI/OfflineUI.cs

using Modal;
using GameState;
using UnityEngine;
//using UnityEngine.Networking;
using Mirror;
using UnityEngine.UI;

namespace UI {
    public class OfflineUI : MonoBehaviour {


        //public GameObject serverCanvas, clientCanvas;
        //public ClientHUD clientHudScript;
        //public ServerHUD serverHUDScript;

        //public GameObject[] show;
        //public GameObject[] hide;


        public int maxConnections = 4;

        public void Start ()
        {
            State.GetInstance().Subscribe(
                new StateOption().
                    NetworkState(State.NETWORK_CLIENT).
                    PreviousGameState(State.GAME_ONLINE).
                    GameState(State.GAME_OFFLINE),
                () => {
                    ModalManager.GetInstance().Show(
                        "Lost connection to server",
                        "Ok",
                        () => {
                            ModalManager.GetInstance().Hide();
                        }
                    );
                }
            );

            State.GetInstance().Subscribe(
                new StateOption().
                    NetworkState(State.NETWORK_CLIENT).
                    PreviousGameState(State.GAME_CONNECTING).
                    GameState(State.GAME_OFFLINE),
                () => {
                    ModalManager.GetInstance().Show(
                        "Cannot establish connection to server",
                        "Ok",
                        () => {
                            ModalManager.GetInstance().Hide();
                        }
                    );
                }
            );
        }

        public void StartServer ()
        {
            NetworkManager.singleton.networkAddress = "localhost";

            // do this manually so we can alter number of connections
            // this has to do with the transport layer. Lets see if we can get rid of this?
            // QosType - Enumeration of all supported quality of service channel modes.
            // The default channel connection configuration used by the high level components
            // of the networking system is:
            // * channel 0 - Reliable Sequenced channel * channel 1 - Unreliable channel.
            //var config = new ConnectionConfig();
            //config.AddChannel(QosType.ReliableSequenced);
            //config.AddChannel(QosType.Unreliable);
            //if (NetworkManager.singleton.StartHost(config, maxConnections) == null) {
            // FIXME - issue because public virtual NetworkClient StartHost(); has been changed to public virutal void StartHost();
            // Need to check if NetworkClient == null which means it is a server and server is already running on machine. 


            //serverHUDScript.enabled = true;
            //serverCanvas.SetActive(true);
            //for (int i = 0; i < hide.Length; i++)
            //{
            //    hide[i].SetActive(false);
            //}

            // Autostart Host
            // Better way to do this I'm sure. 
            NetworkManager.singleton.StartHost();

            //if (NetworkManager.singleton.StartHost() == null)
            if (NetworkManager.singleton.client == null)
            {
                ModalManager.GetInstance().Show(
                    "You already have a server running on this machine",
                    "Oh, ok",
                    () => { ModalManager.GetInstance().Hide(); }
                );
            }

            //        SceneManager.LoadScene("ServerClientMenu");

        }

        //public void StartClient()
        //{
        //    clientHudScript.enabled = true;
        //    clientCanvas.SetActive(true);
        //    for (int i = 0; i < hide.Length; i++)
        //    {
        //        hide[i].SetActive(false);
        //    }
        //    //        SceneManager.LoadScene("ServerClientMenu");
        //}


        // Move the Join Game to the Client Canvas
        public void JoinGame (Text ipAddressText)
        {
            if (ipAddressText.text == "") {
                ModalManager.GetInstance().Show(
                    "You need to enter an IP address to connect to",
                    "Try again",
                    () => { ModalManager.GetInstance().Hide(); }
                );
                return;
            }

            NetworkManager.singleton.networkAddress = ipAddressText.text;
            // if (NetworkManager.singleton.StartClient() == null) {

            // FIXME
            //if (NetworkManager.singleton.StartClient() == null) {
            NetworkManager.singleton.StartClient();
            if (NetworkManager.singleton.client == null) { 
                ModalManager.GetInstance().Show(
                "Connection not attempted to " + ipAddressText.text,
                "Ok",
                () => {
                    ModalManager.GetInstance().Hide();
                }
            );
                return;
            }

            ModalManager.GetInstance().Show(
                "Attempting to join " + ipAddressText.text,
                "Cancel attempt",
                () => {
                    NetworkManager.singleton.StopClient();
                    ModalManager.GetInstance().Hide();
                }
            );
        }
    }
}
