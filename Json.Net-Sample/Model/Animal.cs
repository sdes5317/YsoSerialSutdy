namespace Json.Net_Sample
{
    public abstract class Animal
    {
        public string Name { get; set; }
    }

    public class Dog : Animal
    {
        public string Breed { get; set; }
    }

    public class Cat : Animal
    {
        public bool IsIndoor { get; set; }
    }
}
