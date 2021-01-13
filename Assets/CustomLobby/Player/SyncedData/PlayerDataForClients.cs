// Player/SyncedData/PlayerDataForClients.cs

using UnityEngine;
//using UnityEngine.Networking;
using Mirror;

namespace Player.SyncedData {
    public class PlayerDataForClients : NetworkBehaviour {

        public const int TEAM_SPECTATOR = 0;
        public const int TEAM_VIP = 1;
        public const int TEAM_INHUMER = 2;


        public const int HMDTYPE_SPECTATOR = 0;
        public const int HMDTYPE_OPENVR = 1;
        public const int HMDTYPE_AR = 2;


        // Player Data should also include 
        // Tracked Device ID for Head, LHand, RHand
        // Tracked Device S/N for Head, LHand, RHand

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="playerName"></param>
        public delegate void NameUpdated(GameObject player, string playerName);
        public event NameUpdated OnNameUpdated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="team"></param>
        public delegate void TeamUpdated (GameObject player, int team);
        public event TeamUpdated OnTeamUpdated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="hmdtype"></param>
        public delegate void HMDTypeUpdated(GameObject player, int hmdtype);
        public event HMDTypeUpdated OnHMDTypeUpdated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="score"></param>
        public delegate void ScoreUpdated (GameObject player, int score);
        public event ScoreUpdated OnScoreUpdated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="isReady"></param>
        public delegate void IsReadyFlagUpdated(GameObject player, bool isReady);
        public event IsReadyFlagUpdated OnIsReadyFlagUpdated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="isServer"></param>
        public delegate void IsServerFlagUpdated (GameObject player, bool isServer);
        public event IsServerFlagUpdated OnIsServerFlagUpdated;

        [SyncVar(hook = "UpdateName")]
        private string playerName;


        [SyncVar(hook = "UpdateTeam")]
        private int team;

        // Added HMDType to PlayerData
        [SyncVar(hook = "UpdateHMDType")]
        private int hmdtype;

        [SyncVar(hook = "UpdateScore")]
        private int score;
        [SyncVar(hook = "UpdateIsReadyFlag")]
        private bool isReadyFlag;
        [SyncVar(hook = "UpdateIsServerFlag")]
        private bool isServerFlag;

        public override void OnStartClient()
        {
            // don't update for local player as handled by LocalPlayerOptionsManager
            // don't update for server as the server will know on Command call from local player
            if (!isLocalPlayer && !isServer) {
                UpdateName(playerName);
                UpdateTeam(team);
                UpdateHMDType(hmdtype);

                UpdateScore(score);
                UpdateIsReadyFlag(isReadyFlag);
                UpdateIsServerFlag(isServerFlag);
            }
        }
        
        public string GetName()
        {
            return playerName;
        }

        [Client]
        public void SetName(string newName)
        {
            CmdSetName(newName);
        }

        [Command]
        public void CmdSetName(string newName)
        {
            playerName = newName;
        }

        [Client]
        public void UpdateName(string newName)
        {
            playerName = newName;
            if (this.OnNameUpdated != null) {
                this.OnNameUpdated(gameObject, newName);
            }
        }

        public int GetTeam()
        {
            return team;
        }

        [Client]
        public void SetTeam(int newTeam)
        {
            CmdSetTeam(newTeam);
        }

        [Command]
        public void CmdSetTeam(int newTeam)
        {
            team = newTeam;
        }

        [Client]
        public void UpdateTeam(int newTeam)
        {
            team = newTeam;
            if (this.OnTeamUpdated != null) {
                this.OnTeamUpdated(gameObject, newTeam);
            }
        }


        // HMD Type
        public int GetHMDType()
        {
            return hmdtype;
        }


        [Client]
        public void SetHMDType(int newhmdtype)
        {
            CmdHMDType(newhmdtype);
        }

        [Command]
        public void CmdHMDType(int newhmdtype)
        {
            hmdtype = newhmdtype;
        }

        [Client]
        public void UpdateHMDType(int newhmdtype)
        {
            hmdtype = newhmdtype;
            if (this.OnHMDTypeUpdated != null)
            {
                this.OnHMDTypeUpdated(gameObject, newhmdtype);
            }
        }



        public int GetScore ()
        {
            return score;
        }

        [Server]
        public void ResetScore ()
        {
            score = 0;
        }

        [Server]
        public void ServerIncrementScore ()
        {
            score ++;
        }

        [Client]
        public void UpdateScore (int newScore)
        {
            score = newScore;
            if (this.OnScoreUpdated != null) {
                this.OnScoreUpdated(gameObject, newScore);
            }
        }

        public bool GetIsReadyFlag()
        {
            return isReadyFlag;
        }

        [Client]
        public void SetIsReadyFlag(bool newIsReady)
        {
            CmdSetIsReadyFlag(newIsReady);
        }

        [Command]
        public void CmdSetIsReadyFlag(bool newIsReady)
        {
            isReadyFlag = newIsReady;
        }

        [Client]
        public void UpdateIsReadyFlag(bool newIsReady)
        {
            isReadyFlag = newIsReady;

            if (this.OnIsReadyFlagUpdated != null) {
                this.OnIsReadyFlagUpdated(gameObject, newIsReady);
            }
        }

        public bool GetIsServerFlag ()
        {
            return isServerFlag;
        }

        [Client]
        public void SetIsServerFlag (bool newIsServer)
        {
            CmdSetIsServerFlag(newIsServer);
        }

        [Command]
        public void CmdSetIsServerFlag (bool newIsServer)
        {
            isServerFlag = newIsServer;
        }

        [Client]
        public void UpdateIsServerFlag (bool newIsServer)
        {
            isServerFlag = newIsServer;

            if (this.OnIsServerFlagUpdated != null) {
                this.OnIsServerFlagUpdated(gameObject, newIsServer);
            }
        }
    }
}