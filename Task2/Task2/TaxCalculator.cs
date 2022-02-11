using System.Reflection;


namespace Task2
{
    //In this class we create a bunch of methods that apply some kind of tax deduction to the gross salary given.
    internal static class TaxCalculator
    {
        private const int taxThreshold = 1000;
        private const int socialContributionsThreshold = 3000;
        private const decimal incomeTaxPercentage = 0.1m;
        private const decimal socialContributionsTaxPercentage = 0.15m;
      
        public static decimal CalculateNetValue(decimal grossAmount)
        {
            decimal taxOwed = 0m;

            Type myType = (typeof(TaxCalculator));

            //Here we get an array of all of the tax deduction methods, that follow a certain set of rules:
            // - Method should be prvate
            // - Method should be static
            // - Method should have a return type of "decimal"
            // - Method should accept one argument of type "decimal"
            // - Method`s name should end with "TaxAmmount"
            MethodInfo[] taxMethodArr = myType.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                .Where(x => x.Name.EndsWith("TaxAmount") &&
                            x.GetParameters().Length == 1 &&
                            x.GetParameters()[0].ParameterType.Name == "Decimal" &&
                            x.ReturnType.Name == "Decimal").ToArray();

            //Here we loop through each tax deduction method in order to get the sum of the tax that is owed;
            foreach (var taxMethod in taxMethodArr)
            {
                taxOwed += (decimal)taxMethod.Invoke(null, new object[] { grossAmount });
            }

            //We return the gross salary amount minus the tax that is owed
            return Math.Round(grossAmount -= taxOwed, 2); 
        }


        private static decimal GetIncomeTaxAmount(decimal grossAmount)
        {
            return IsThresholdExceeded(grossAmount) ? ((grossAmount - taxThreshold) * incomeTaxPercentage) : 0;
        }

        private static decimal GetSocialContributionsTaxAmount(decimal grossAmount)
        {
            var result = IsThresholdExceeded(grossAmount) ? grossAmount : 0;


            if (result == 0)
            {
                return result;
            }

            result = IsSocialContributionsThresholdExceeded(grossAmount) ? (socialContributionsThreshold - taxThreshold) : (result - taxThreshold);

            return result * socialContributionsTaxPercentage;
        }



        private static bool IsSocialContributionsThresholdExceeded(decimal grossAmount)
            => grossAmount > socialContributionsThreshold;
        private static bool IsThresholdExceeded(decimal grossAmount)
            => grossAmount > taxThreshold;
        
    }
}
