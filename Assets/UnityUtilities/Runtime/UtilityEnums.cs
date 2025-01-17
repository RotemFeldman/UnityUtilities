namespace UnityUtilities
{
    namespace UnityUtilities
    {
        public enum Direction
        {
            North,
            South,
            East,
            West
        }
        
        public enum FourDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum GridDirection
        {
            TopLeft,
            TopCenter,
            TopRight,
            CenterLeft,
            Center,
            CenterRight,
            BottomLeft,
            BottomCenter,
            BottomRight
        }
        
        public enum EightCardinalDirection
        {
            North = Direction.North, 
            South = Direction.South, 
            East = Direction.East, 
            West = Direction.West,
            NorthWest,
            NorthEast,
            SouthWest,
            SouthEast,
            
        }
    }
}