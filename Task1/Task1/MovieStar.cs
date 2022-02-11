using System.Text;

namespace Task1
{
    internal class MovieStar
    {
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }


        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"{this.Name} {this.Surname}");
            result.AppendLine($"{this.Sex}");
            result.AppendLine($"{this.Nationality}");

            var today = DateTime.Today;
            var age = today.Year - this.DateOfBirth.Year;


            if (this.DateOfBirth.AddYears(age) > today)
            {
                age--;
            }


            result.AppendLine($"{age} years old");

            return result.ToString();
        }
    }
}
