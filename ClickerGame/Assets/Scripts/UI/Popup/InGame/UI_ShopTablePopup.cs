using System.Collections.Generic;
using Data;
using Manager;

namespace UI.Popup.InGame
{
    public class UI_ShopTablePopup : UI_TableBase
    {
        private List<SubItem_Shop> _subItems = new List<SubItem_Shop>();
        
        public override bool Initialize()
        {
            if (base.Initialize() == false)
                return false;
            
            _subItems.Clear();

            foreach (ShopData data in Managers.Data.Shop.Values)
            {
                SubItem_Shop shop = Managers.UI.MakeSubItem<SubItem_Shop>(Content);
                
                shop.Initialize();
                shop.SetInfo(data);
                
                _subItems.Add(shop);
            }


            return true;
        }
    }
}