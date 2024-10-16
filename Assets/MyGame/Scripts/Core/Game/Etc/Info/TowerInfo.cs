using Sirenix.OdinInspector;
using UnityEngine;

namespace TD.Info
{
    public class TowerInfo : SelectableComponenet, IInfoData
    {
        public string   Name { get; set; }
        public string[] Detail { get; set; }

        [ShowInInspector, ReadOnly]
        private GameObject attackArea;

        protected override void Start(){
            base.Start();
            Init(GameManager.Instance.Library.GetData<NewTowerData>(name));
            attackArea = transform.Find("AttackArea").gameObject;
        }

        public void Init(NewTowerData towerData){
            Name   = towerData.dataName;
            Detail = towerData.ToStringEx();
        }

        public void SetAttackArea(GameObject attackArea) => this.attackArea = attackArea;

        public override void Deselect(){
            base.Deselect();
            attackArea.SetActive(false);
        }

        public override void Select(){
            base.Select();
            attackArea.SetActive(true);
        }
    }
}
