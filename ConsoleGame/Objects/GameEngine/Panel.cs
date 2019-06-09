
namespace ConsoleGame.Objects.GameEngine
{
    public class Panel
    {
        private OccupationType occupationType;

        public OccupationType OccupationType
        {
            get { return occupationType; }
            
            set
            {
                if (value != occupationType)
                {
                    occupationType = value;
                    Game.AlteredPanels.Add(this);
                }
            }
        }

        public Coordinates Coordinates { get; set; }
        public Game Game{ get; set; }

        public Panel(int column, int row, Game game)
        {
            Coordinates = new Coordinates(column, row);
            Game = game;
            OccupationType = OccupationType.Neutral;
        }
    }
}
