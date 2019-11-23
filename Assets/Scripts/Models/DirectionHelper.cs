using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public static class DirectionHelper
    {
        public static bool Has(this Direction direction, Direction value)
        {
            return (direction & value) == value;
        }

        public static List<Vector2> GetDirections(Direction directions)
        {
            var result = new List<Vector2>();
            if (directions.Has(Direction.Up))
            {
                result.Add(Vector2.up);
            }
            if (directions.Has(Direction.Left))
            {
                result.Add(Vector2.left);
            }
            if (directions.Has(Direction.Right))
            {
                result.Add(Vector2.right);
            }
            if (directions.Has(Direction.Down))
            {
                result.Add(Vector2.down);
            }
            if (directions.Has(Direction.UpRight))
            {
                result.Add(Vector2.one.normalized);
            }
            if (directions.Has(Direction.DownRight))
            {
                result.Add(new Vector2(1, -1).normalized);
            }
            if (directions.Has(Direction.DownLeft))
            {
                result.Add((-Vector2.one).normalized);
            }
            if (directions.Has(Direction.UpLeft))
            {
                result.Add(new Vector2(-1, 1).normalized);
            }
            
            return result;
        }
    }
}