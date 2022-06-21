using System;
using System.Collections;
using Manager;
using UI.Popup;
using UI.Popup.InGame;
using UnityEngine;

namespace UI.Scene
{
    public class UI_PlayScene : UI_Scene
    {
        enum Transforms
        {
            PlayerArea,
            EnemyArea,
        }
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            Managers.Sound.Play(Define.Sound.Bgm, "InGameBgm");
            
            Bind<Transform>(typeof(Transforms));
            
            Managers.Game.SetPlayerArea(Get<Transform>((int) Transforms.PlayerArea));
            Managers.Game.SetEnemyArea(Get<Transform>((int) Transforms.EnemyArea));
            
            Managers.Game.Player.SetView(Managers.UI.MakeSubItem<SubItem_Player>(Managers.Game.PlayerSpawnArea));
            
            Managers.UI.ShowPopupUI<UI_TabMenuPopup>().Initialize();
            Managers.UI.ShowPopupUI<UI_UserInfoPopup>().Initialize();
            
            Managers.UI.RefreshUI();
            Managers.Game.Player.RefreshUIData();
            
            StopAllCoroutines();
            StartCoroutine(CoAutoSave());
            
            return true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Managers.Game.TryExitGame)
                {
                    Managers.Game.TryExitGame = true;
                    Managers.UI.ShowPopupUI<UI_ExitGamePopup>();
                }
            }
        }

        IEnumerator CoAutoSave()
        {
            while (true)
            {
                Managers.Game.SaveGame();
                yield return new WaitForSeconds(3);
            }
        }
    }
}