using System;

public class Coordinate {

    private float x;
    private float y;
    private float z;

    private float boundaryCenterX;
    private float boundaryCenterY;
    private float boundaryCenterZ;
    private float boundaryScale;

    private float maxX;
    private float maxY;
    private float maxZ;

    private Random rand;

    private float scale = 1f;

    public Coordinate(float boundaryCenterX, float boundaryCenterY, float boundaryCenterZ, float boundaryScale, Random rand)
    {
        maxX = boundaryCenterX + boundaryScale - scale;
        maxY = boundaryCenterY + boundaryScale - scale;
        maxZ = boundaryCenterZ + boundaryScale - scale;

        this.boundaryCenterX = boundaryCenterX;
        this.boundaryCenterY = boundaryCenterY;
        this.boundaryCenterZ = boundaryCenterZ;
        this.boundaryScale = boundaryScale;
        this.rand = rand;

        GenerateCoordinate();
    }

    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }

    public float GetZ()
    {
        return z;
    }

    public float GetScale()
    {
        return scale;
    }

    public bool Equals(Coordinate Other)
    {
        float distance = (float) Math.Sqrt(Math.Pow(GetX() - Other.GetX(),2) +
                                           Math.Pow(GetY() - Other.GetY(), 2) + 
                                           Math.Pow(GetZ() - Other.GetZ(), 2));
        return distance < scale;
    }

    public void GenerateCoordinate()
    {
        x = boundaryCenterX + (maxX - boundaryCenterX) * (float) rand.NextDouble();
        y = boundaryCenterY + (maxX - boundaryCenterY) * (float) rand.NextDouble();
        z = boundaryCenterZ + (maxX - boundaryCenterZ) * (float) rand.NextDouble();
    }
}
