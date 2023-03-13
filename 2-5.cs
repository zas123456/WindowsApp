public abstract class Shape
{

    public abstract int Area();

    public abstract bool IsRight();
}

public class Rectangle : Shape
{
    int height;
    int width;
    public Rectangle()
    {
        this.height = Convert.ToInt32(Console.ReadLine());
        this.width = Convert.ToInt32(Console.ReadLine());
    }

    public override int Area()
    {
        return height * width;
    }

    public override bool IsRight()
    {
        if (height > 0 && width > 0)
            return true;
        else
            return false;
    }
}

public class Square : Shape
{
    int width;
    public Square()
    {
        this.width = Convert.ToInt32(Console.ReadLine());
    }

    public override int Area()
    {
        return width * width;
    }

    public override bool IsRight()
    {
        if (width > 0)
            return true;
        else
            return false;
    }
}

public class Triangle : Shape
{
    int bottom, height;
    int side1, side2, side3;
    public Triangle()
    {
        this.bottom = Convert.ToInt32(Console.ReadLine());
        this.height = Convert.ToInt32(Console.ReadLine()); 
        this.side1 =  Convert.ToInt32(Console.ReadLine());
        this.side2 = Convert.ToInt32(Console.ReadLine());
        this.side3 = Convert.ToInt32(Console.ReadLine());
    }

    public override int Area()
    {
        return bottom * height / 2;
    }

    public override bool IsRight()
    {
        if (height > 0 && bottom > 0 && side1 > 0 && side2 > 0 && side3 > 0)
        {
            if (side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side2)
            {
                if (side1 - side2 < side3 && side1 - side3 < side2 && side2 - side3 < side2)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        else
            return false;
    }
}

public class Factory
{
    public static Shape Creatshape(string n)
    {
        switch(n)
        {
            case"1":return new Rectangle();
            case"2":return new Square();
            case"3":return new Triangle();
            default:
                return null;
        }
    }
}
public class Program
{
    static void Main(string[] args)
    {
        Shape shape1 = Factory.Creatshape("1");Console.WriteLine(shape1.Area()+"  "+shape1.IsRight());
        shape1 = Factory.Creatshape("2"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("3"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("1"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("2"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("2"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("2"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("3"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("3"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("1"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());
        shape1 = Factory.Creatshape("1"); Console.WriteLine(shape1.Area() + "  " + shape1.IsRight());

    }
}
