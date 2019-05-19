using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.BoardObjects;
using ConsoleGame.Objects.GameBoard;
using System.Runtime.Caching;

namespace ConsoleGame.Extensions
{
    public static class BoardExtensions
    {
        public static bool UpdateObject(this Board board, BoardObject obj, bool clear = false)
        {
            List<Panel> alteredPanelsForThisObject = new List<Panel>();

            for (int y = 0; y < obj.Style.Height; y++)
            {
                for (int x = 0; x < obj.Style.Width; x++)
                {
                    if (obj.Style.Texture[y * obj.Style.Width + x] != '*')
                        continue;

                    else
                    {
                        Panel currentPanel = board[x + obj.Coordinates.Column, y + obj.Coordinates.Row];

                        if (clear)
                            board[x + obj.PreviousCoordinates.Column, y + obj.PreviousCoordinates.Row].OccupationType = OccupationType.Neutral;

                        else
                        {
                            Collision collision = checkCollision(currentPanel.OccupationType, obj.OccupationType);

                            if (collision == Collision.BlockObject)
                            {
                                for (int i = 0; i < alteredPanelsForThisObject.Count; i++)
                                    alteredPanelsForThisObject[i].OccupationType = OccupationType.Neutral;

                                return false;
                            }

                            if (collision != Collision.Nothing)
                            {
                                MemoryCache cache = MemoryCache.Default;

                                CacheItemPolicy cip = new CacheItemPolicy()
                                {
                                    AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                                };

                                cache.Add("collision", collision, cip);

                                return false;
                            }

                            else
                            {
                                alteredPanelsForThisObject.Add(currentPanel);

                                currentPanel.OccupationType = obj.OccupationType;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private static Collision checkCollision(OccupationType collider1, OccupationType collider2)
        {
            if (collider1 == OccupationType.Player)
            {
                if (collider2 == OccupationType.Enemy)
                    return Collision.PlayerEnemy;

                else if (collider2 == OccupationType.Goal)
                    return Collision.PlayerGoal;
            }

            else if (collider1 == OccupationType.Enemy)
            {
                if (collider2 == OccupationType.Player)
                    return Collision.PlayerEnemy;
            }

            else if (collider1 == OccupationType.Block || collider2 == OccupationType.Block)
                return Collision.BlockObject;

            return Collision.Nothing;
        }
    }
}
