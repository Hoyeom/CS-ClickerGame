using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Manager;

namespace Data
{
    public class BossDataExcel
    {
        [XmlAttribute]
        public int ID;
        [XmlAttribute]
        public int Health;
        [XmlAttribute]
        public int AttackPower;
        [XmlAttribute]
        public int DefencePower;
    }
    
    public class BossData
    {
        [XmlAttribute]
        public int ID;
        [XmlArray]
        public List<BossDataExcel> Data  = new List<BossDataExcel>();
    }
    
    [Serializable,XmlRoot("ArrayOfBossData")]
    public class BossDataLoader
    {
        [XmlElement("BossData")] 
        public List<BossData> _bossData = new List<BossData>();

        public Dictionary<int, BossData> MakeDictionary()
        {
            Dictionary<int, BossData> dic = new Dictionary<int, BossData>();
            
            foreach (var data in _bossData)
                dic.Add(data.ID, data);
                    
            return dic;
        }
    }
}