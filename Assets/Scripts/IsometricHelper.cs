using UnityEngine;

public static class IsometricHelper
{
    public const float XDELTA = .5f;
    public const float YDELTA = .25f;

    /**
     * Maps an xy coordinate to the position a tile shoudl be placed in
     * unity. Method also takes into accoutn elevation of the tile
     * 
     */
    public static Vector2 gridToGamePostion(int x, int y)
    {
        float posX = -(y * XDELTA) + (x * XDELTA);
        float posY = (y * YDELTA) + (x * YDELTA);
        //posY += pos.elevation * YDELTA;
        return new Vector2(posX, posY);
    }

    /**
     * Assigns the sorting order tiles shoudl have so the sprites 
     * are displayed in the correct order
     * 
     */
    public static int getTileSortingOrder(int x, int y)
    {
        int sortingOrder = -x + -y;
        return sortingOrder;
    }

    /**
     * Assigns the sorting order tiles shoudl have so the sprites 
     * are displayed in the correct order
     * 
     */
    /*public static int distanceBetweenGridPositions(GridPosition one, GridPosition two)
    {
        int distanceX = Mathf.Abs(one.x - two.x);
        int distanceY = Mathf.Abs(one.y - two.y);
        return distanceX+distanceY;
    }*/

}

