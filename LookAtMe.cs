using BepInEx;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace COM3D2.LookAtMe
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class LookAtMe : BaseUnityPlugin
    {
        private List<Maid> MaidList = new List<Maid>();

        private int framecounter = 0;
        private bool isEditMode = false;


        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            isEditMode = scene.buildIndex == 5;
        }
           

        public void Update()
        {
            if (!isEditMode)
            {
                if (this.framecounter++ > 200)
                {
                    this.framecounter = 0;
                    this.MaidList.Clear();
                    for (int i = 0; i < GameMain.Instance.CharacterMgr.GetMaidCount(); i++)
                    {
                        Maid maid = GameMain.Instance.CharacterMgr.GetMaid(i);
                        if (maid != null && maid.Visible && maid.AudioMan != null)
                        {
                            this.MaidList.Add(maid);
                        }
                    }
                    if (this.MaidList.Count() > 0)
                    {
                        for (int i = 0; i < this.MaidList.Count(); i++)
                        {
                            if (!this.MaidList[i].body0.boHeadToCam || !this.MaidList[i].body0.boEyeToCam)
                            {
                                Logger.LogMessage($"Forced {this.MaidList[i].status.callName} to look at you.");

                                this.MaidList[i].EyeToCamera(Maid.EyeMoveType.目と顔を向ける, 3f);
                            }
                        }
                    }
                }
            }           
        }
    }
}
