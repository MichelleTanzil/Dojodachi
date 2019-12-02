namespace dojodachi.Models
{
    public class Dojodachi
    {
        public int Fullness {get; set;}
        public int Happiness {get; set;}

        public int Energy {get; set;}

        public int Meal {get; set;}

        public Dojodachi()
        {
            Fullness = 20;
            Happiness = 20;
            Energy = 50;
            Meal = 3;
        }

    }
}