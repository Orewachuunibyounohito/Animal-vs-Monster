namespace TD.Info
{
    public class EnemyInfo : SelectableComponenet, IInfoData
    {
        public string   Name { get; set; }
        public string[] Detail { get; set; }

        protected override void Start(){
            Init(GameManager.Instance.Library.GetData<NewEnemyData>(name));
        }

        public void Init(NewEnemyData enemyData){
            Name   = enemyData.dataName;
            Detail = enemyData.ToStringEx();
        }
    }
}